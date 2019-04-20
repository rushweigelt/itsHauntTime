using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class TriggerStoryListenerNode : ActionNode
{
    [StoryGraphField] public StoryListener StoryListener;

    public override string MenuName {get{return "Trigger Story Listener";}}

    public override void Execute()
    {
        StoryListener.StoryListenerAction.Invoke();
        GoToNextNode();
    }

}
