using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class RotateCoroutineNode : CoroutineNode
    {

        [StoryGraphField] public GameObject Origin;
        [StoryGraphField] public GameObject Target;
        [StoryGraphField] public float Duration;
        [StoryGraphField] public AnimationCurve AnimCurve;

        public override string MenuName { get { return "Game Object/Rotate"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(RotateTarget());
        }
        public IEnumerator RotateTarget()
        {

            float journey = 0f;
            Quaternion startingRotation = Origin.transform.rotation;
            while (journey <= Duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / Duration);
                float curvePercent = AnimCurve.Evaluate(percent);
                Origin.transform.rotation = Quaternion.Lerp(startingRotation, Target.transform.rotation, curvePercent);

                yield return null;
            }
            GoToNextNode();
        }
    }
}