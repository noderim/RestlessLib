using RestlessLib.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RestlessLib.TimeR
{
    [System.Serializable]
    public class SequenceElement
    {
        public float Delay;
        public float Duration;
        public UnityEvent OnElementStarts;
        public UnityEvent OnElementEnds;
        [HorizontalLine]
        [Header("Fade Animation")]
        public bool UseFadeAnimation;
        public CanvasGroup CanvasGroup;
        public float FadeInTime;
        [Space(10)]
        public float FadeOutTime;
    }
}