using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph {
	public abstract class LogicGateNode : StoryNode {

    #if UNITY_EDITOR    
        public override void SetStyles()
        {
            base.SetStyles();    
            nodeHeaderStyle = StoryGraphStyles.NodeLogicStyle();
        }
        
    #endif
	}
}