using UnityEngine;
using UnityEditor;
using RestlessLib.Attributes;


[CustomPropertyDrawer(typeof(InfoBoxAttribute))]
public class InfoBoxAttributeDrawer : DecoratorDrawer
{
    private float _height;
    public override float GetHeight()
    {
        return _height + 10;
    }
    public override void OnGUI(Rect rect)
    {
        InfoBoxAttribute infoBoxAttribute = (InfoBoxAttribute)attribute;
        Rect indentRect = EditorGUI.IndentedRect(rect);
        float indentLength = indentRect.x - rect.x;
        Rect infoBoxRect = new Rect(
            rect.x + indentLength + 15,
            rect.y + 5,
            rect.width - indentLength,
            GetHelpBoxHeight());
        DrawInfoBox(infoBoxRect, infoBoxAttribute.Text, infoBoxAttribute.Type);
    }
    private float GetHelpBoxHeight()
    {
        InfoBoxAttribute infoBoxAttribute = (InfoBoxAttribute)attribute;
        float minHeight = EditorGUIUtility.singleLineHeight * 2.0f;
        float desiredHeight = GUI.skin.box.CalcHeight(new GUIContent(infoBoxAttribute.Text), EditorGUIUtility.currentViewWidth);
        float height = Mathf.Max(minHeight, desiredHeight);
        _height = height;
        return height;
    }
    private void DrawInfoBox(Rect rect, string infoText, InfoBoxType infoBoxType)
    {
        MessageType messageType = MessageType.None;
        switch (infoBoxType)
        {
            case InfoBoxType.Info:
                messageType = MessageType.Info;
                break;
            case InfoBoxType.Warning:
                messageType = MessageType.Warning;
                break;
            case InfoBoxType.Error:
                messageType = MessageType.Error;
                break;
        }
        EditorGUI.HelpBox(rect, infoText, messageType);
    }
}