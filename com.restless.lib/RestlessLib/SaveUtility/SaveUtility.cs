using System;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace RestlessLib.SaveUtility
{
    public static class SaveUtility
    {
        private const string SaveDirectory = "Assets/Saved/";
        public static void Save(ScriptableObject data)
        {
            if (!Directory.Exists(SaveDirectory))
            {
                AssetDatabase.CreateFolder("Assets", "Saved");
            }

            try
            {
                string json = JsonUtility.ToJson(data, true);
#if UNITY_EDITOR
                string path = Path.Combine(SaveDirectory, data.name);
#else
                string path = Path.Combine(Application.persistentDataPath, data.name);
#endif
                File.WriteAllText(path, json);
                Debug.Log("Data saved successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save data: {e.Message}");
            }
        }

        public static bool Load(ScriptableObject data)
        {
            try
            {

#if UNITY_EDITOR
                string path = Path.Combine(SaveDirectory, data.name);
#else
                string path = Path.Combine(Application.persistentDataPath, data.name);
#endif

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    JsonUtility.FromJsonOverwrite(json, data);
                    Debug.Log("Data loaded successfully.");
                    return true;
                }
                else
                {
                    Debug.LogWarning("Save file not found.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data: {e.Message}");
                return false;
            }
        }
        public static T GetObject<T>() where T : ScriptableObject
        {
            T obj = AssetDatabase.LoadAssetAtPath<T>(Path.Combine(SaveDirectory, typeof(T).Name));
            Load(obj);
            return obj;
        }
        public static T GetOrCreate<T>() where T : ScriptableObject
        {
            T obj = AssetDatabase.LoadAssetAtPath<T>(Path.Combine(SaveDirectory, $"{typeof(T).Name}.asset"));
            if (obj == null)
            {
                obj = ScriptableObject.CreateInstance<T>();
                if (!Directory.Exists(SaveDirectory))
                {
                    AssetDatabase.CreateFolder("Assets", "Saved");
                }
                AssetDatabase.CreateAsset(obj, Path.Combine(SaveDirectory, $"{typeof(T).Name}.asset"));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            Load(obj);
            return obj;
        }

        public static void Save(object data, string filename, bool verbose = true)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
#if UNITY_EDITOR
                string path = Path.Combine(SaveDirectory, filename);
#else
                string path = Path.Combine(Application.persistentDataPath, filename);
#endif
                string directory = Path.GetDirectoryName(path);

                if (verbose)
                {
                    Debug.Log($"Saving data to: {path}");
                }

                if (!Directory.Exists(directory))
                {
                    AssetDatabase.CreateFolder("Assets", "Saved");
                }

                File.WriteAllText(path, json);
                if (verbose)
                {
                    Debug.Log("Data saved successfully.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save data: {e.Message}");
            }
        }

        public static bool Load(object obj, string filename, bool verbose = true)
        {
            try
            {
#if UNITY_EDITOR
                string path = Path.Combine(SaveDirectory, filename);
#else
                string path = Path.Combine(Application.persistentDataPath, filename);
#endif

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    JsonUtility.FromJsonOverwrite(json, obj);
                    if (verbose)
                    {
                        Debug.Log("Data loaded successfully.");
                    }
                    return true;
                }
                else
                {
                    Debug.LogWarning("Save file not found.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data: {e.Message}");
                return false;
            }
        }

        public static bool CreateOrLoad(object obj, string filename, bool verbose = true)
        {
            if (!Load(obj, filename, verbose))
            {
                Save(obj, filename, verbose);
                return false;
            }
            return true;
        }
    }

}
