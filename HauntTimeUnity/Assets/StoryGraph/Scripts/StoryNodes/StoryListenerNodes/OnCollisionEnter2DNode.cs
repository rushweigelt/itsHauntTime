using UnityEngine;

namespace StoryGraph
{
    public class OnCollisionEnter2DNode : ListenerNode
    {

        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public OnCollisionEnter2DListener Listener;


        public override string MenuName {get{return "Collider/On Collision Enter 2D";}}

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
