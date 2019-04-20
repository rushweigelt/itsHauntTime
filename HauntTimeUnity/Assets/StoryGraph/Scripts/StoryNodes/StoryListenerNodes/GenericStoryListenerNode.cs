using UnityEngine;

namespace StoryGraph
{
    public class GenericStoryListenerNode : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public StoryListener Listener;

        public override string MenuName {get{return "Generic Listener";}}

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

        public override void DisableNode(){
            base.DisableNode();
            Listener.StoryListenerAction -= OnListener;
        }
    }
}
