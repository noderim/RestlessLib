using System;
using UnityEngine;

namespace RestlessLib.Attributes
{
    // Custom attribute for Read Only Inspector Element (Not editable)
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }

    // Custom attribute for Read Only Inspector Element (Not editable)
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ToggleAttribute : PropertyAttribute { }

    // Custom attribute to expand refrence object variables inside anothers object inspector
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ExpandableAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]

    /// Draws a horizontal line in the Inspector.
    public class HorizontalLineAttribute : PropertyAttribute
    {
        public Color color;
        public float thickness;
        public float padding;

        public HorizontalLineAttribute(float r = 0.7f, float g = 0.7f, float b = 0.7f, float thickness = 2f, float padding = 5f)
        {
            this.color = new Color(r, g, b);
            this.thickness = thickness;
            this.padding = padding;
        }
    }

    /// Displays an int or string field as a scene list in the Inspector.
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SceneAttribute : PropertyAttribute { }

    /// Displays an info box with a custom message in the Inspector.
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class InfoBoxAttribute : PropertyAttribute
    {
        public string Text;
        public InfoBoxType Type;

        public InfoBoxAttribute(string Text, InfoBoxType type = InfoBoxType.Info)
        {
            this.Text = Text;
            this.Type = type;
        }
    }
    public enum InfoBoxType
    {
        Info,
        Warning,
        Error
    }

    // Custom attribute to specify horizontal space
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class HorizontalSpaceAttribute : PropertyAttribute
    {
        public float spaceWidth;

        public HorizontalSpaceAttribute(float spaceWidth)
        {
            this.spaceWidth = spaceWidth;
        }
    }

}
