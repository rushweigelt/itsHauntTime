using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class EnableGameObjectArrayAction : ActionNode
    {

        [StoryGraphField(StoryDrawer.Array)] public GameObject[] gos;

        public override string MenuName { get { return "Game Object/Enable Game Object Array"; } }

        public override void Execute()
        {
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].SetActive(true);
            }
            GoToNextNode();
        }

    }
}
