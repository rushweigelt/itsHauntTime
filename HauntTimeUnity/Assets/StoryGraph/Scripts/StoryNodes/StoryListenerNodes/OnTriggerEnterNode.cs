using UnityEngine;

namespace StoryGraph
{
    public class OnTriggerEnterNode : ListenerNode
    {

        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public OnTriggerEnterListener Listener;


        public override string MenuName {get{return "Collider/On Trigger Enter";}}

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
