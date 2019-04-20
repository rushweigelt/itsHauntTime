using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class ScaleCoroutineNode : CoroutineNode
    {

        [StoryGraphField] public GameObject Origin;
        [StoryGraphField] public GameObject Target;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "Game Object/Scale"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(ScaleTarget());
        }
        public IEnumerator ScaleTarget()
        {

            float journey = 0f;
            Vector3 startingScale = Origin.transform.localScale;
            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                Origin.transform.localScale = Vector3.Lerp(startingScale, Target.transform.localScale, curvePercent);

                yield return null;
            }
            GoToNextNode();
        }
    }
}