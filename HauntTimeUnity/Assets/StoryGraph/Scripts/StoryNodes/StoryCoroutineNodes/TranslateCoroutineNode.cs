using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class TranslateCoroutineNode : CoroutineNode
    {

        [StoryGraphField] public GameObject Origin;
        [StoryGraphField] public GameObject Target;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "Game Object/Translate"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(TranslateTarget());
        }
        public IEnumerator TranslateTarget()
        {

            float journey = 0f;

            Vector3 startingPosition = Origin.transform.position;
            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                Origin.transform.position = Vector3.LerpUnclamped(startingPosition, Target.transform.position, curvePercent);
                yield return null;
            }
            GoToNextNode();
        }
    }
}

