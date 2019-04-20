using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public class CustomActionNode : ActionNode
    {
        public override string MenuName {get{return "Custom Action Node";}}

        [StoryGraphField(StoryDrawer.UnityEvent)] public UnityEvent onEvent;

        public override void Execute()
        {
            onEvent.Invoke();
            GoToNextNode();
        }
    }
}
