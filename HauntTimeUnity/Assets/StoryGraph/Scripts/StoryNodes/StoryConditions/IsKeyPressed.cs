using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class IsKeyPressed : ConditionNode
    {
        [StoryGraphField] public KeyCode Key;


        public override string MenuName { get { return "Keyboard/Is Key Pressed"; } }

        public override void Execute()
        {
            if (Input.GetKey(Key))
            {
                GoToTrueNode();
            } else {
                GoToFalseNode();
            }
        }

    }
}