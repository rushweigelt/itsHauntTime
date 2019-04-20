using UnityEngine;

namespace StoryGraph
{
    public abstract class ListenerNode : StoryNode
    {

    #if UNITY_EDITOR    
        public override string MenuNamePrefix {get{return "Listener/";}}
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeEventStyle();
        }
    #endif
    }
}