using System.Collections;
using UnityEngine;

namespace StoryGraph
{
    public class OnStartNode : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public OnStartListener Listener;


        public override string MenuName {get{return "Game Object/On Start";}}

        public override void Execute()
        {
            Debug.Log(Id + " is Initialized");
            if(Listener != null)
            {
                Listener.StoryListenerAction += OnListener;
                Listener.IsListenerSet = true;
            }
        }

        public void OnListener()
        {
            if(TurnOffOnExecute)
            {
                Listener.StoryListenerAction -= OnListener;
            }
            GoToNextNode();
        }
    }
}
