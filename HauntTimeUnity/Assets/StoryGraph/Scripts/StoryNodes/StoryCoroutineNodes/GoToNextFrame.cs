using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class GoToNextFrame : CoroutineNode
    {

        public override string MenuName { get { return "Go To Next Frame"; } }

        public override void Execute()
        {
            storyGraph.StartCoroutine(WaitForSeconds());
        }
        public IEnumerator WaitForSeconds()
        {
            yield return null;
            GoToNextNode();
        }
    }
}




