using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph {
	public class AndLogicGate : LogicGateNode {
        
    public enum AndState{IsDone, IsDoneInSameLoop, IsDoneSameNumberOfTimes, IsAwake, IsAsleep}

    public AndState andState;
    
    #if UNITY_EDITOR    
        public override string MenuName {get{return "Logic/And";}}
        public override void SetSerializedProperties()
        {    
            AddSerializedProperty("andState", "Check If Nodes Are");
        }
    #endif

        public override void WakeUpNode(string _loopId){
            
            
            LoopId = _loopId;
            

            if(storyNodeState != StoryNodeState.IsDisabled){
                storyNodeState = StoryNodeState.IsAwake;
                Execute();
            }
        }

        public override void Execute()
        {
			if(storyGraph.PassesAndGate(this))
			{
                GoToNextNode();
			}
        }
	}
}