using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class IsGameObjectEnabled : ConditionNode
    {
        [StoryGraphField(StoryDrawer.NoLabelPropertyField)] public GameObject IsEnabledObject;


        public override string MenuName { get { return "Is GameObject Enabled"; } }

        public override void Execute()
        {
            if (IsEnabledObject.activeSelf)
            {
                GoToTrueNode();
            } else {
                GoToFalseNode();
            }
        }

    }
}