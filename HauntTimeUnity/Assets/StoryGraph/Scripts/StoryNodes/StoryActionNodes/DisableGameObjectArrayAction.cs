using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class DisableGameObjectArrayAction : ActionNode
    {

        [StoryGraphField(StoryDrawer.Array)] public GameObject[] gos;

        public override string MenuName { get { return "Game Object/Disable Game Object Array"; } }

        public override void Execute()
        {
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].SetActive(false);
            }
            GoToNextNode();
        }

    }
}
