using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class BuildTools 
{
    [MenuItem("Build/Build Android APK")]
    public static void BuildApk()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        List<string> scenes = new List<string>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if(scene.enabled) scenes.Add(scene.path);
        }
        buildPlayerOptions.scenes = scenes.ToArray();

        string repositoryPath = ".";
        string commitHash = GitHelper.GetCommitHash(repositoryPath);

        // string stage = "alpha";
        string platform = "android";
        
        string apkName = string.Format("{0}_v{1}_{2}_{3}_{4}.apk", 
                                        PlayerSettings.productName, 
                                        PlayerSettings.bundleVersion, 
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        commitHash,
                                        platform);

        string outputPath = "./Build/Android";

        if(Directory.Exists(outputPath))
        {
            Directory.Delete(outputPath, true);
        }

        buildPlayerOptions.locationPathName = Path.Combine(outputPath, apkName);
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [MenuItem("Build/Build Windows package")]
    public static void BuildExe()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        List<string> scenes = new List<string>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if(scene.enabled) scenes.Add(scene.path);
        }
        buildPlayerOptions.scenes = scenes.ToArray();

        string repositoryPath = ".";
        string commitHash = GitHelper.GetCommitHash(repositoryPath);

        // string stage = "alpha";
        string platform = "win64";

        string zipFileName = string.Format("{0}_v{1}_{2}_{3}_{4}.zip", 
                                        PlayerSettings.productName, 
                                        PlayerSettings.bundleVersion, 
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        commitHash,
                                        platform);

        string outputPath = "./Build/Windows";

        if(Directory.Exists(outputPath))
        {
            Directory.Delete(outputPath, true);
        }

        buildPlayerOptions.locationPathName = Path.Combine(outputPath, PlayerSettings.productName + ".exe");
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }

        ZipHelper.ZipFiles(Path.Combine(outputPath, zipFileName), outputPath);

        Debug.Log("Pack complete");
    }

    [MenuItem("Build/Export Unity package")]
    public static void ExportUnityPackage()
    {
        string repositoryPath = ".";
        string commitHash = GitHelper.GetCommitHash(repositoryPath);

        string unitypackageName = string.Format("{0}_v{1}_{2}_{3}.unitypackage", 
                                        "OrbbecSDK", 
                                        PlayerSettings.bundleVersion, 
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        commitHash);

        string outputPath = "./Build/UnityPackage";

        if(Directory.Exists(outputPath))
        {
            Directory.Delete(outputPath, true);
        }

        AssetDatabase.ExportPackage("Assets/Orbbec", Path.Combine(outputPath, unitypackageName),
            ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
    }
}