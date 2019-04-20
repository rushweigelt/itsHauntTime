using System.Collections;
using UnityEngine;

namespace StoryGraph
{
    public abstract class ActionNode : StoryNode
    {

    #if UNITY_EDITOR    
        public override string MenuNamePrefix {get{return "Action/";}}
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeActionStyle();
        }
    #endif
    }
}
