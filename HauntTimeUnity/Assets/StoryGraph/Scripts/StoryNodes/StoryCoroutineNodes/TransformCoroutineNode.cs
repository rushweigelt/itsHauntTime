using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class TransformCoroutineNode : CoroutineNode
    {

        [StoryGraphField] public GameObject Origin;
        [StoryGraphField] public GameObject Target;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "Game Object/Transform"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(TranslateTarget());
        }
        public IEnumerator TranslateTarget()
        {

            float journey = 0f;

            Vector3 startingPosition = Origin.transform.position;
            Vector3 startingScale = Origin.transform.localScale;
            Quaternion startingRotation = Origin.transform.rotation;

            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);

                float curvePercent = AnimCurve.Evaluate(percent);
                Origin.transform.position = Vector3.LerpUnclamped(startingPosition, Target.transform.position, curvePercent);
                Origin.transform.localScale = Vector3.Lerp(startingScale, Target.transform.localScale, curvePercent);
                Origin.transform.rotation = Quaternion.LerpUnclamped(startingRotation, Target.transform.rotation, curvePercent);
                yield return null;
            }
            GoToNextNode();
        }
    }
}

