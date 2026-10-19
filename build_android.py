#!/usr/bin/env python3
"""Run the Android player build and show Unity's current build phase."""

import os
from pathlib import Path
import re
import subprocess
import sys
import time


PROJECT_DIR = Path(__file__).resolve().parent
UNITY = os.environ.get(
    "UNITY_BIN",
    "/home/user/Unity/Hub/Editor/2019.4.41f2/Editor/Unity",
)
GCONF_LIB_DIR = os.environ.get(
    "GCONF_LIB_DIR",
    "/tmp/opencode/gconf_extract/usr/lib/x86_64-linux-gnu",
)
GRADLE_HOME = os.environ.get("GRADLE_USER_HOME", "/tmp/dokdo-gradle-home")
ANDROID_CONFIG_HOME = os.environ.get("DOKDO_ANDROID_HOME", "/tmp/dokdo-android-home")

PHASES = [
    ("Starting Unity", 0),
    ("Compiling project scripts", 10),
    ("Preparing player and shaders", 25),
    ("Generating ARM64 code (IL2CPP)", 45),
    ("Packaging APK with Gradle", 80),
    ("Build complete", 100),
]


def phase_for(line, current):
    if "BuildPlayer: start building target" in line:
        return 1
    if "Finished compile Library/PlayerScriptAssemblies/Assembly-CSharp.dll" in line:
        return 2
    if "DisplayProgressbar: Compiling shader variants" in line:
        return max(current, 2)
    if "DisplayProgressbar: IL2CPP" in line:
        return 3
    if 'DisplayProgressbar: Building Gradle project' in line:
        return 4
    if "[BUILD] result=Succeeded" in line:
        return 5
    return current


def show_progress(phase, spinner):
    name, percent = PHASES[phase]
    width = 24
    filled = round(width * percent / 100)
    bar = "#" * filled + "-" * (width - filled)
    sys.stdout.write(f"\r[{bar}] {name}  {spinner}")
    sys.stdout.flush()


def main():
    unity_path = Path(UNITY)
    gconf_dir = Path(GCONF_LIB_DIR)
    if not unity_path.is_file():
        print(f"Unity executable not found: {unity_path}", file=sys.stderr)
        return 2
    if not (gconf_dir / "libgconf-2.so.4").exists():
        print(f"libgconf-2.so.4 not found under: {gconf_dir}", file=sys.stderr)
        return 2

    if len(sys.argv) > 1:
        log_path = Path(sys.argv[1])
        if not log_path.is_absolute():
            log_path = PROJECT_DIR / log_path
    else:
        timestamp = time.strftime("%Y%m%d-%H%M%S")
        log_path = PROJECT_DIR / "Logs" / f"build-{timestamp}.log"
    log_path.parent.mkdir(parents=True, exist_ok=True)
    log_path.write_text("", encoding="utf-8")

    env = os.environ.copy()
    env["LD_LIBRARY_PATH"] = str(gconf_dir) + os.pathsep + env.get("LD_LIBRARY_PATH", "")
    env["GRADLE_USER_HOME"] = GRADLE_HOME
    env["ANDROID_SDK_HOME"] = ANDROID_CONFIG_HOME
    env["ANDROID_USER_HOME"] = ANDROID_CONFIG_HOME
    Path(GRADLE_HOME).mkdir(parents=True, exist_ok=True)
    Path(ANDROID_CONFIG_HOME).mkdir(parents=True, exist_ok=True)
    (Path(ANDROID_CONFIG_HOME) / ".android").mkdir(exist_ok=True)

    command = [
        str(unity_path),
        "-batchmode",
        "-nographics",
        "-projectPath",
        str(PROJECT_DIR),
        "-executeMethod",
        "BuildAndroidScript.Build",
        "-quit",
        "-logFile",
        str(log_path),
    ]
    print(f"Unity log: {log_path}")
    process = subprocess.Popen(command, cwd=PROJECT_DIR, env=env)
    phase = 0
    spinner_chars = "|/-\\"
    spinner = 0
    log_pos = 0
    last_render = 0.0
    failure_line = None

    try:
        while process.poll() is None:
            try:
                with log_path.open("r", encoding="utf-8", errors="replace") as log:
                    log.seek(log_pos)
                    while True:
                        line = log.readline()
                        if not line:
                            break
                        log_pos = log.tell()
                        phase = phase_for(line, phase)
                        if "[BUILD] result=Failed" in line or "[BUILD] EXCEPTION:" in line:
                            failure_line = line.strip()
            except (FileNotFoundError, OSError):
                pass

            now = time.monotonic()
            if now - last_render >= 1.5:
                show_progress(phase, spinner_chars[spinner % len(spinner_chars)])
                spinner += 1
                last_render = now
            time.sleep(0.2)

        # Read any final log output written as Unity exits.
        with log_path.open("r", encoding="utf-8", errors="replace") as log:
            log.seek(log_pos)
            while True:
                line = log.readline()
                if not line:
                    break
                phase = phase_for(line, phase)
                if "[BUILD] result=Failed" in line or "[BUILD] EXCEPTION:" in line:
                    failure_line = line.strip()
    except KeyboardInterrupt:
        process.terminate()
        process.wait()
        print("\nBuild interrupted.", file=sys.stderr)
        return 130

    show_progress(phase, " ")
    print()
    if process.returncode != 0 or phase != 5:
        if failure_line:
            print(failure_line, file=sys.stderr)
        print(f"Build failed; see {log_path}", file=sys.stderr)
        return process.returncode or 1

    print(f"APK: {PROJECT_DIR / 'Builds' / 'DOKDO_arm64.apk'}")
    print(f"Build log: {log_path}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
