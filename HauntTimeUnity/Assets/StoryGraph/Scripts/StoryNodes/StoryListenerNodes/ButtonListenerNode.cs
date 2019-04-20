using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class ButtonListenerNode : ListenerNode
    {

        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField]public Button Button;


        public override string MenuName { get { return "UI/On Button Click Listener"; } }

        public override void Execute()
        {
            if (Button != null)
            {
                Button.onClick.AddListener(OnListener);
            }
        }

        public void OnListener()
        {
            if (TurnOffOnExecute)
            {
                Button.onClick.RemoveListener(OnListener);
            }
            GoToNextNode();
        }

        public override void DisableNode()
        {
            base.DisableNode();
            Button.onClick.RemoveListener(OnListener);
        }
    }
}
