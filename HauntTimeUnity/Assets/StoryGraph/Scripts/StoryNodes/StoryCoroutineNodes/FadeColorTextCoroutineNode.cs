using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class FadeColorTextCoroutineNode : CoroutineNode
    {

        [StoryGraphField] public Text Text;
        [StoryGraphField] public Color Color;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "UI/Fade Color Text"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(FadeInTarget());
        }
        public IEnumerator FadeInTarget()
        {

            float journey = 0f;


            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                Text.color = Color.Lerp(Text.color, Color, curvePercent);

                yield return null;
            }
            GoToNextNode();
        }
    }
}