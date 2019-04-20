using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph {
	public class OrLogicGate : LogicGateNode {
        
    public enum OrState{IsDone, IsDoneMoreThanOnce, IsDoneMoreThanOrEqualToOnce, IsAwake, IsAsleep}

    public OrState orState;

    #if UNITY_EDITOR    

        public override string MenuName {get{return "Logic/Or";}}
        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("orState", "Check If Nodes Are");
        }
    #endif
        public override void Execute()
        {
			if(storyGraph.PassesOrGate(this))
			{
                GoToNextNode();
			}
        }
	}
}