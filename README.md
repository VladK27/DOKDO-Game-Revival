# DOKDO Android Revival

A community project dedicated to restoring **DOKDO** and making it compatible with modern Android devices, including ARM64.

The original game was developed by **Zzoo**. This project focuses solely on compatibility improvements and preserving the original gameplay experience.

## Disclaimer

All credits, rights, and intellectual property belong to the original developers at **Zzoo**.

I am not affiliated with the original developers. I only adapted the game to work on newer Android versions.

This is an unofficial, non-commercial fan project created for preservation purposes.

## How I made this

The original APK was built with **Unity 5.6.5f1** (Mono scripting backend) and shipped only a **32-bit ARM** native library, which modern Android no longer supports. There was no source code, so the project had to be reconstructed and then ported.

The workflow, step by step:

1. **Inspected the APK** — checked the manifest, native libraries and the Unity data to identify the engine version, scripting backend and architecture.
2. **Reconstructed the project** — decompiled the managed assemblies (`Assembly-CSharp.dll`) with **ILSpy** and rebuilt a full Unity project (scenes, prefabs, meshes, textures, materials, animation) from the APK data with **AssetRipper**.
3. **Upgraded the engine** — opened the reconstructed project in **Unity 2019.4 LTS**. The game code no longer compiled, so the removed / changed APIs had to be fixed (legacy networking, `GUIText` / `GUITexture`, moved texture APIs, and a few generated attributes).
4. **Added ARM64** — switched the build to the **IL2CPP** backend and enabled the **arm64-v8a** target so the game runs on 64-bit-only devices.
5. **Restored the graphics** — the APK stores only *compiled* shaders, which cannot be decompiled back to source. They were recreated from their original sources (the water shader used Unity's legacy Water4; the particle effects used the Cartoon FX shaders).
6. **Fixed port artefacts** — repaired rendering and UI problems introduced by the engine upgrade, such as a HUD widget whose animated children collapsed due to a scale issue, and worked through every compile error until the project built cleanly.
7. **Built and tested** — configured the Android player (arm64-v8a, min SDK 24), produced the release APK and verified the game on a **Google Pixel 9 running Android 17**: it launches, renders the original graphics, loads the scenes and reacts to input.

The gameplay, assets and design are untouched — only the compatibility layer was rebuilt.
