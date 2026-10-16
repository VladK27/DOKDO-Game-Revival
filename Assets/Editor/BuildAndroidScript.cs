using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildAndroidScript
{
	public static void Build()
	{
		try
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

			PlayerSettings.companyName = "Zzoo";
			PlayerSettings.productName = "DOKDO";
			PlayerSettings.bundleVersion = "1.16.6";
			PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.zzoo.dokdo");
			PlayerSettings.Android.bundleVersionCode = 43;

			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
			PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
			PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Android, ManagedStrippingLevel.Disabled);
			PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);

			PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
			PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
			PlayerSettings.Android.forceInternetPermission = true;
			PlayerSettings.Android.forceSDCardPermission = true;

			EditorUserBuildSettings.development = true;
			EditorUserBuildSettings.allowDebugging = true;
			EditorUserBuildSettings.buildAppBundle = false;

			List<string> scenes = new List<string>();
			foreach (EditorBuildSettingsScene s in EditorBuildSettings.scenes)
			{
				if (s.enabled)
				{
					scenes.Add(s.path);
				}
			}
			Debug.Log("[BUILD] scenes: " + string.Join(", ", scenes.ToArray()));

			Directory.CreateDirectory("Builds");
			string outPath = "Builds/DOKDO_arm64.apk";

			BuildPlayerOptions opts = new BuildPlayerOptions
			{
				scenes = scenes.ToArray(),
				locationPathName = outPath,
				target = BuildTarget.Android,
				options = BuildOptions.Development
			};

			BuildReport report = BuildPipeline.BuildPlayer(opts);
			BuildSummary summary = report.summary;
			Debug.Log("[BUILD] result=" + summary.result + " size=" + summary.totalSize + " errors=" + summary.totalErrors + " warnings=" + summary.totalWarnings);

			if (summary.result != BuildResult.Succeeded)
			{
				Debug.LogError("[BUILD] FAILED");
				EditorApplication.Exit(1);
			}
			else
			{
				Debug.Log("[BUILD] SUCCEEDED -> " + Path.GetFullPath(outPath));
				EditorApplication.Exit(0);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[BUILD] EXCEPTION: " + ex);
			EditorApplication.Exit(2);
		}
	}
}
