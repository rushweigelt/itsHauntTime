using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryGraph
{
    public class SetTextAction : ActionNode
    {

        [StoryGraphField(StoryDrawer.PropertyField)] public Text Text;
        [StoryGraphField(StoryDrawer.NoLabelPropertyField)] public string TextField;

        public override string MenuName { get { return "UI/Set Text"; } }

        public override void Execute()
        {
            Text.text = TextField;
            GoToNextNode();
        }

    }
}