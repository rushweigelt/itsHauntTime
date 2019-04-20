using UnityEngine;

namespace StoryGraph
{
    public class OnTriggerEnter2DNode : ListenerNode
    {

        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public OnTriggerEnter2DListener Listener;


        public override string MenuName {get{return "Collider/On Trigger Enter 2D";}}

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
