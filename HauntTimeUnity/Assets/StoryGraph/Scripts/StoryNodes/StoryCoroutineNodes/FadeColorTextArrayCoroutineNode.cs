using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class FadeColorTextArrayCoroutineNode : CoroutineNode
    {

        [StoryGraphField(StoryDrawer.Array)] public Text[] Text;
        [StoryGraphField] public Color Color;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public float Offset;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "UI/Fade Color Text Array"; } }

        public override void Execute()
        {

            for (int i = 0; i < Text.Length; i++)
            {
                storyGraph.StartCoroutine(FadeInTarget(Text[i], (float)i * Offset));
            }
        }
        public IEnumerator FadeInTarget(Text text, float Offset)
        {

            yield return new WaitForSeconds(Offset);

            float journey = 0f;
            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                text.color = Color.Lerp(text.color, Color, curvePercent);

                yield return null;
            }
            GoToNextNode();
        }
    }
}