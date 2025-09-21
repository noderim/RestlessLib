using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace RestlessLib.Editor
{
    /// <remarks>
    /// In package.json u need to add field "pathToAssets": "relative/path/to/your.unitypackage"
    /// </remarks>
    public static class PackageAssetsImporter
    {
        public static void ImportAssetsFromPackageJson(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
            {
                Debug.LogError("Package name is null or empty.");
                return;
            }

            string unitypackagePath = GetAssetsPathFromPackageJson(packageName);

            if (!File.Exists(unitypackagePath))
            {
                Debug.LogError($"Assets file not found at: {unitypackagePath}");
                return;
            }

            // Wywołanie normalnego okna importu
            AssetDatabase.ImportPackage(unitypackagePath, true);
        }
        public static string GetAssetsPathFromPackageJson(string packageName, bool silent = false)
        {
            // Znajdujemy folder paczki
            string packagePath = Path.Combine("Packages", packageName);
            string packageJsonPath = Path.Combine(packagePath, "package.json");

            if (!File.Exists(packageJsonPath))
            {
                if (!silent)
                    Debug.LogError($"Package {packageName} not found at path: {packagePath}");
                return string.Empty;
            }

            // Ładujemy package.json jako tekst
            string json = File.ReadAllText(packageJsonPath);

            // Parsowanie tylko tego pola (można zrobić JsonUtility, SimpleJSON itp.)
            var packageData = JsonUtility.FromJson<PackageJsonWrapper>(json);

            if (string.IsNullOrEmpty(packageData.pathToAssets))
            {
                if (!silent)
                    Debug.LogWarning($"Package {packageName} does not specify 'pathToAssets' in package.json.");
                return string.Empty;
            }

            string unitypackagePath = Path.Combine(packagePath, packageData.pathToAssets);

            return unitypackagePath;
        }

        [System.Serializable]
        private class PackageJsonWrapper
        {
            public string name;
            public string version;
            public string pathToAssets;
        }
    }
}
