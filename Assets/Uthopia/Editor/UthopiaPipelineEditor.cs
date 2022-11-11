using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class UthopiaPipelineEditor
{
    static string gamePackagesFolder = Path.Combine("Assets", "Game Packages");
    static string gamesFolder = Path.Combine("Assets", "Games");

    [MenuItem("Uthopia/Build Pipeline")]
    static void BuildPipeline()
    {
        // Delete "Assets/Game Packages" folder 
        //AssetDatabase.DeleteAsset(gamesFolder);
        // Create a new "Assets/Games" folder to hold games
        AssetDatabase.CreateFolder("Assets", "Games");

        // Load game packages stored on Assets/Games
        var gamePackageGuids = AssetDatabase.FindAssets("", new[] { gamePackagesFolder });

        foreach (var guid in gamePackageGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            AssetDatabase.ImportPackage(path, false);
        }

        //TODO: Load games into pipeline
    }



    void AddSceneToBuildSettings(string scenePath)
    {
        var newScenes = EditorBuildSettings.scenes.ToList();

        if (newScenes.Any(s => s.path == scenePath))
            return;

        newScenes.Add(new EditorBuildSettingsScene(scenePath, true));

        EditorBuildSettings.scenes = newScenes.ToArray();
    }


}
