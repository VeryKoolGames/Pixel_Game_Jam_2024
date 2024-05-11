using System.IO;
using UnityEditor;
using UnityEditor.Search;
using Application = UnityEngine.Application;

public static class Setup
{
    // Script to automatically create default folders in the Assets folder
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        Folders.CreateDefault("", "Scenes", "Scripts", "Assets", "Materials", "Sprites", "Prefabs",
            "ScriptableObjects", "Settings");
        AssetDatabase.Refresh();
    }

    static class Folders
    {
        public static void CreateDefault(string root, params string[] folders)
        {
            var fullpath = Path.Combine(Application.dataPath, root);
            foreach (var folder in folders)
            {
                var path = Path.Combine(fullpath, folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}