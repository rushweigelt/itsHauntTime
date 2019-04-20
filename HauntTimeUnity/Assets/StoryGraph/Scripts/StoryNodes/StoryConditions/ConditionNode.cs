using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StoryGraph
{
    public abstract class ConditionNode : StoryNode
    {
        public ConnectionPoint elseOutPoint;

#if UNITY_EDITOR
        public override string MenuNamePrefix {get{return "Condition/";}}

        public override void SetStyles()
        {
            base.SetStyles();
            nodeHeaderStyle = StoryGraphStyles.NodeConditionStyle();
        }


        public override void Initialize(string _title, Vector2 position, float width, float height, StoryGraph _storyGraph)
        {
            base.Initialize(_title, position, width, height, _storyGraph);

            ExtraPointSpacing = (EditorGUIUtility.singleLineHeight + 10) *2;
            elseOutPoint = new GameObject().AddComponent<ConnectionPoint>();
            elseOutPoint.gameObject.hideFlags = HideFlags.HideInHierarchy;
            elseOutPoint.gameObject.transform.parent = transform;
            elseOutPoint.Initialize(Id, ConnectionPointType.Out, StoryGraphStyles.OutPointStyle(), storyGraph, false, 3);
            outPoint.setNodeIndex(2);

            SerializedPropertyYOffset = 132;
        }

        public override void Draw(bool isVisible)
        {
            elseOutPoint.UpdateLocation(rect.x, rect.y, rect.width, rect.height);
            elseOutPoint.Draw();

            if (serializedObject == null)
            {
                serializedObject = new SerializedObject(this);
            }

            elseOutPoint.setNodeIndex(3);
            outPoint.setNodeIndex(2);

            DrawConnectionPoints();

            GUI.BeginGroup(rect, style);

            GUI.Label(new Rect(0, 22 + (2*28), rect.width - 20, 20), "True", whiteRightTextStyle);
            GUI.Label(new Rect(0, 22 + (3*28), rect.width - 20, 20), "False", whiteRightTextStyle);

            DrawNode(isVisible);


            GUI.EndGroup();
        }

        public override void DestroySelf()
        {
            storyGraph.RemoveNode(this);
        }

        public override void SelectNode(bool isSelected)
        {
            storyGraph.SetConnectionsSelected(this, isSelected);
        }
#endif

        public void GoToTrueNode()
        {
            GoToNextNode();
        }

        public void GoToFalseNode()
        {
            storyNodeState = StoryNodeState.IsDone;
            timesVisited++;
            elseOutPoint.GoToNextNode(LoopId);
        }
    }
}