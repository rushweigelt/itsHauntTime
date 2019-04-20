using System.Collections;
using UnityEngine;

namespace StoryGraph
{
    public class WaitForSecondsCoroutineNode : CoroutineNode
    {
        [StoryGraphField] public float Duration;

        public override string MenuName { get { return "Wait For Seconds"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(WaitForSeconds());
        }
        public IEnumerator WaitForSeconds()
        {

            yield return new WaitForSeconds(Duration);
            GoToNextNode();
        }
    }
}




