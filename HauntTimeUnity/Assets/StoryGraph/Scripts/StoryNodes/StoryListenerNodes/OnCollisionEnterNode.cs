using UnityEngine;

namespace StoryGraph
{
    public class OnCollisionEnterNode : ListenerNode
    {

        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public OnCollisionEnterListener Listener;


        public override string MenuName {get{return "Collider/On Collision Enter";}}

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
