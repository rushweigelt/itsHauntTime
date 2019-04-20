using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public enum ConnectionPointType { In, Out }

    public class ConnectionPoint : MonoBehaviour
    {
        public string Id;
        public string NodeId;

        public ConnectionPointType type;
        public StoryGraph storyGraph;


        #if UNITY_EDITOR      
        float nodeCount = 1;  
        float nodeIndex = 1;  
        string label = "";
        public Rect rect;
        public GUIStyle style;
        bool appendBottom = false;

        /// <summary>
        /// Constructor used instead of normal class constructor to integrate easier with GameObject Instantiate function 
        /// </summary>
        public void Initialize(string NodeId, ConnectionPointType type, GUIStyle style, StoryGraph _storyGraph, bool _appendBottom = false, float _nodeIndex = 1f, float _nodeCount = 1f)
        {
            this.Id = "ConnectionPoint_" + System.Guid.NewGuid().ToString();;
            this.NodeId = NodeId;
            this.type = type;
            this.style = style;
            rect = new Rect(0, 0, 10f, 20f);
            storyGraph = _storyGraph;
            appendBottom = _appendBottom;
            nodeIndex = _nodeIndex;
        }

        public void setAppendBottom(bool _appendBottom){appendBottom = _appendBottom;}
        public void setNodeIndex(float _nodeIndex){nodeIndex = _nodeIndex;}

        /// <summary>
        /// Positions location of ConnectionPoint based on Node's location
        /// </summary>
        public void UpdateLocation(float nodeRectX, float nodeRectY, float nodeRectWidth, float nodeRectHeight)
        {
            if(appendBottom){
                rect.y = nodeRectY + nodeRectHeight - rect.height - 12;
            }else{
                rect.y = nodeRectY + 22 + (nodeIndex*28);
            }

            switch (type)
            {
                case ConnectionPointType.In:
                    rect.x = nodeRectX - rect.width + 8f;
                    break;

                case ConnectionPointType.Out:
                    rect.x = nodeRectX + nodeRectWidth - 8f;
                    break;
            }
        }

        /// <summary>
        /// On Click will activate either InPoint or OutPoint button functionality
        /// </summary>
        public void Draw()
        {
            if (GUI.Button(rect, "", style))
            {
                if (type == ConnectionPointType.In)
                {
                    storyGraph.ClickConnectionInPoint(this);
                }
                else if (type == ConnectionPointType.Out)
                {
                    storyGraph.ClickConnectionOutPoint(this);
                }
            }
        }

        

        #endif

        /// <summary>
        /// Travels from OutPoint to Connection
        /// </summary>
        public void GoToNextNode(string loopId)
        {
            if(type == ConnectionPointType.Out)
            {
                storyGraph.TraverseConnection(Id, loopId);
            }
        }
    }
}