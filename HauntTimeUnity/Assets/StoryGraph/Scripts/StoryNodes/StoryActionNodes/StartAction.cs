using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class StartAction : ActionNode
    {
        public override string MenuName { get { return "Start"; } }

        public override void Execute()
        {
            GoToNextNode();
        }

    }
}
