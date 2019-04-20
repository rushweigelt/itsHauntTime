using System.Collections;
using UnityEngine;

namespace StoryGraph
{
    public abstract class CoroutineNode : StoryNode
    {

    #if UNITY_EDITOR    
        public override string MenuNamePrefix {get{return "Coroutine/";}}
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeCoroutineStyle();
        }
    #endif
    }
}