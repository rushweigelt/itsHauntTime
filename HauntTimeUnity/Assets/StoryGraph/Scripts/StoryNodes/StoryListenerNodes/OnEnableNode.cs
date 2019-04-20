using UnityEngine;

namespace StoryGraph
{
    public class OnEnableNode : ListenerNode
    {

        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public OnEnableListener Listener;


        public override string MenuName {get{return "Game Object/On Enable";}}

        public override void Execute()
        {
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
