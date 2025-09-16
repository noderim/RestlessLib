using UnityEngine;
using UnityEditor;
using System.Linq;
using RestlessLib.Attributes;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneAttribute))]
public class SceneAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Ensure the property is either a string or an integer
        if (property.propertyType != SerializedPropertyType.String && property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.HelpBox(position, "SceneAttribute can only be used with string or int fields", MessageType.Error);
            return;
        }

        // Get all enabled scenes from Build Settings
        var enabledScenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select((scene, index) => new
            {
                scene.path,
                name = System.IO.Path.GetFileNameWithoutExtension(scene.path),
                index
            })
            .ToList();

        // If no scenes are found, display a warning
        if (enabledScenes.Count == 0)
        {
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.HelpBox(position, "No scenes found in Build Settings", MessageType.Warning);
            return;
        }

        // Determine the currently selected scene index
        int currentIndex = 0;
        if (property.propertyType == SerializedPropertyType.String)
        {
            currentIndex = enabledScenes.FindIndex(scene => scene.name == property.stringValue);
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            currentIndex = property.intValue;
        }

        // Clamp the index to ensure it's within valid bounds
        currentIndex = Mathf.Clamp(currentIndex, 0, enabledScenes.Count - 1);

        // Create a list of scene names for the dropdown
        var sceneNames = enabledScenes.Select(scene => scene.name).ToArray();

        // Display the dropdown and get the newly selected index
        int newIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneNames);

        // Update the property value if the selection has changed
        if (newIndex != currentIndex)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                property.stringValue = enabledScenes[newIndex].name;
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = newIndex;
            }
        }
    }
}
#endif
