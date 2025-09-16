using System.Collections.Generic;
using RestlessLib.Attributes;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace RestlessLib.TimeR
{

    [System.Serializable]
    public class TimedSequence : MonoBehaviour
    {
        [ReadOnly]
        [Header("Info")]
        public bool isPlaying;
        [Range(0, 1)]
        public float progress;
        private float acttime;
        [ReadOnly]
        public float sequenceDuration;

        [HorizontalLine]
        [Header("Sequance Setup")]
        public float SequenceStartDelay;
        public UnityEvent OnSequenceStart;
        [Space(10)]
        public float SequanceEndDelay;
        public UnityEvent OnSequenceEnd;

        [HorizontalLine]
        [Header("Sequance")]
        [SerializeField]
        public List<SequenceElement> Sequence;

        private CancellationTokenSource cancellationTokenSource;

        void Awake()
        {
            sequenceDuration = SequenceStartDelay;
            foreach (SequenceElement element in Sequence)
            {
                sequenceDuration += element.Delay + element.Duration + element.FadeInTime + element.FadeOutTime;
            }
            sequenceDuration += SequanceEndDelay;
        }

        public virtual void Update()
        {
            if (isPlaying)
            {
                acttime += Time.deltaTime;
                progress = acttime / sequenceDuration;
                if (progress >= 1f)
                {
                    progress = 1f;
                    isPlaying = false;
                }
            }
        }


        public async virtual Task PlaySequence(CancellationToken token)
        {
            isPlaying = true;
            acttime = 0f;
            progress = 0f;
            try
            {
                await Awaitable.WaitForSecondsAsync(SequenceStartDelay, token);
                OnSequenceStart.Invoke();
                foreach (SequenceElement element in Sequence)
                {
                    await Awaitable.WaitForSecondsAsync(element.Delay, token);
                    element.OnElementStarts.Invoke();
                    if (element.UseFadeAnimation)
                    {
                        await FadeCanvasGroup(element.CanvasGroup, 0f, 1f, element.FadeInTime, token);
                        await Awaitable.WaitForSecondsAsync(element.Duration, token);
                        await FadeCanvasGroup(element.CanvasGroup, 1f, 0f, element.FadeOutTime, token);
                    }
                    else
                    {
                        await Awaitable.WaitForSecondsAsync(element.Duration, token);
                    }
                    element.OnElementEnds.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Catch");
                return;
            }
            finally
            {
                Debug.Log("Finally");
                cancellationTokenSource.Dispose();
            }
            EndSequence();
        }
        [ContextMenu("Start Sequance")]
        public async Task StartSequence()
        {
            cancellationTokenSource = new CancellationTokenSource();
            await PlaySequence(cancellationTokenSource.Token);
        }

        public virtual void SkipSequence()
        {
            // Immediately invoke the end event
            cancellationTokenSource.Cancel();
            Debug.Log("Sequence Skipped");
            EndSequence();
        }

        public virtual void EndSequence()
        {
            acttime = sequenceDuration;
            progress = 1f;
            isPlaying = false;
            OnSequenceEnd.Invoke();
        }

        private async Task FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration, CancellationToken token)
        {
            float elapsedTime = 0f;
            while (elapsedTime <= duration)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                await Awaitable.NextFrameAsync(token); // Wait for next frame
            }
            canvasGroup.alpha = endAlpha;
        }
    }

}