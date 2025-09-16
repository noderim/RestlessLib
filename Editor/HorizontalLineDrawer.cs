using UnityEngine;
using UnityEditor;
using RestlessLib.Attributes;

/// <summary>
/// Custom Editor for HorizontalLineAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(HorizontalLineAttribute))]
public class HorizontalLineDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        HorizontalLineAttribute lineAttribute = (HorizontalLineAttribute)attribute;
        position.x += 15;
        position.width -= 15;
        position.y += lineAttribute.padding;
        position.height = lineAttribute.thickness;

        EditorGUI.DrawRect(position, lineAttribute.color);
    }

    public override float GetHeight()
    {
        HorizontalLineAttribute lineAttribute = (HorizontalLineAttribute)attribute;
        return lineAttribute.thickness + (lineAttribute.padding * 2);
    }
}
