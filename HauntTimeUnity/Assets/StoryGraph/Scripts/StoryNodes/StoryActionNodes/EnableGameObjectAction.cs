using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class EnableGameObjectAction : ActionNode
    {
        [StoryGraphField(StoryDrawer.NoLabelPropertyField)] public GameObject EnableObject;

        public override string MenuName { get { return "Game Object/Enable Game Object"; } }

        public override void Execute()
        {
            EnableObject.SetActive(true);
            GoToNextNode();
        }

    }
}
