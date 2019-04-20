using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StoryGraph
{
    public class StoryGraph : MonoBehaviour
    {
        public List<StoryNode> Nodes;

        public int NodesCount
        {
            get { return Nodes.Count; }
        }

        public List<Connection> Connections;


        public Dictionary<string, GUIStyle> Styles;

        private ConnectionPoint selectedInPoint;
        public ConnectionPoint SelectedInPoint
        {
            get { return selectedInPoint; }
            set { selectedInPoint = value; }
        }
        private ConnectionPoint selectedOutPoint;
        public ConnectionPoint SelectedOutPoint
        {
            get { return selectedOutPoint; }
            set { selectedOutPoint = value; }
        }


        private StoryNode selectedNode;
        public StoryNode SelectedNode
        {
            get { return selectedNode; }
            set { selectedNode = value; }
        }

        void OnEnable()
        {
            StartNodeGraph();
        }

        void OnDisable()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].DisableNode();
            }
        }

        /// <summary>
        /// Starts on nodes with no inPoint
        /// </summary>
        void StartNodeGraph()
        {

            List<StoryNode> storyNodes = new List<StoryNode>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                storyNodes.Add(Nodes[i]);
                for (int j = 0; j < Connections.Count; j++)
                {
                    if (Nodes[i].inPoint.Id == Connections[j].inPoint.Id)
                    {
                        storyNodes.Remove(Nodes[i]);
                    }
                }
            }

            for (int i = 0; i < storyNodes.Count; i++)
            {
                storyNodes[i].WakeUpNode("start");
            }
        }


#if UNITY_EDITOR

        public float Zoom = 1.0f;

        /// <summary>
        /// Highlights connections when selecting a StoryNode
        /// </summary>
        public void SetConnectionsSelected(StoryNode storyNode, bool _isSelected)
        {

            string inPointId = storyNode.inPoint.Id;
            string outPointId = storyNode.outPoint.Id;



            if (Connections != null && Connections.Count > 0)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    if (selectedNode == null)
                    {
                        break;
                    }
                    //If you are picking the node then all its connections are selected
                    if (storyNode.Id == selectedNode.Id && _isSelected == true)
                    {
                        Connections[i].isSelected = false;

                        if (Connections[i].inPoint.Id == inPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                        if (Connections[i].outPoint.Id == outPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                    }
                    //If you are picking the node, and this one is set to be not selected (should never happen)
                    else if (storyNode.Id == selectedNode.Id && _isSelected == false)
                    {
                        Connections[i].isSelected = false;
                    }
                }
            }
        }

        /// <summary>
        /// Highlights connections when selecting a ConditionNode
        /// </summary>
        public void SetConnectionsSelected(ConditionNode conditionNode, bool _isSelected)
        {

            string inPointId = conditionNode.inPoint.Id;
            string outPointId = conditionNode.outPoint.Id;
            string elsePointId = conditionNode.elseOutPoint.Id;

            if (Connections != null && Connections.Count > 0)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    if (selectedNode == null)
                    {
                        break;
                    }
                    //If you are picking the node then all its connections are selected
                    if (conditionNode.Id == selectedNode.Id && _isSelected == true)
                    {
                        Connections[i].isSelected = false;

                        if (Connections[i].inPoint.Id == inPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                        if (Connections[i].outPoint.Id == outPointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                        if (Connections[i].outPoint.Id == elsePointId)
                        {
                            Connections[i].isSelected = _isSelected;
                        }
                    }
                    //If you are picking the node, and this one is set to be not selected
                    else if (conditionNode.Id == selectedNode.Id && _isSelected == false)
                    {
                        Connections[i].isSelected = false;
                    }
                }
            }
        }

        /// <summary>
        /// Removes StoryNode and all connections attached to that StoryNode
        /// </summary>
        public void RemoveNode(StoryNode node)
        {
            if (node != null && Connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < Connections.Count; i++)
                {
                    if (Connections[i].inPoint == node.inPoint || Connections[i].outPoint == node.outPoint)
                    {
                        connectionsToRemove.Add(Connections[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    RemoveConnection(connectionsToRemove[i]);
                }

                connectionsToRemove = null;
            }
            Nodes.Remove(node);
        }

        /// <summary>
        /// Removes ConditionNode and all connections attached to that ConditionNode
        /// </summary>
        public void RemoveNode(ConditionNode node)
        {
            if (node != null && Connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < Connections.Count; i++)
                {
                    if (Connections[i].inPoint == node.inPoint || Connections[i].outPoint == node.outPoint || Connections[i].outPoint == node.elseOutPoint)
                    {
                        connectionsToRemove.Add(Connections[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    RemoveConnection(connectionsToRemove[i]);
                }

                connectionsToRemove = null;
            }
            Nodes.Remove(node);
        }

        /// <summary>
        /// Sets all nodes state to sleep
        /// </summary>
        public void setAllNodesAsleep()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].DisableNode();
            }
        }

        /// <summary>
        /// Removes Connection from list and gameObject from scene
        /// </summary>
        public void RemoveConnection(Connection connection)
        {
            Connections.Remove(connection);
            DestroyImmediate(connection.gameObject);
        }

        /// <summary>
        /// Creates Bezier Curve from clicking InPoint Button
        /// </summary>
        public void ClickConnectionInPoint(ConnectionPoint inPoint)
        {

            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.NodeId != selectedInPoint.NodeId)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        /// <summary>
        /// Creates Bezier Curve from clicking OutPoint Button
        /// </summary>
        public void ClickConnectionOutPoint(ConnectionPoint outPoint)
        {

            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint.NodeId != selectedInPoint.NodeId)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        /// <summary>
        /// Connects InPoint and OutPoint with a Bezier Curve
        /// </summary>
        public void CreateConnection()
        {
            Connection connection = new GameObject().AddComponent<Connection>();
            connection.gameObject.hideFlags = HideFlags.HideInHierarchy;
            connection.gameObject.transform.parent = transform;

            connection.Initialize(selectedInPoint, selectedOutPoint, this);

            if (Connections == null)
            {
                Connections = new List<Connection>();
            }

            Connections.Add(connection);

        }

        /// <summary>
        /// Detaches currently selected curve from the mouse
        /// </summary>
        public void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }

#endif

        /// <summary>
        /// Filters for StoryNode linearly by id
        /// </summary>
        public StoryNode GetNodeById(string ConnectionId)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Id == ConnectionId)
                {
                    return Nodes[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Filters for Connection linearly by id
        /// </summary>
        public Connection GetConnectionById(string ConnectionId)
        {
            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i].Id == ConnectionId)
                {
                    return Connections[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Traverses connection via OutPoint from previous node, and executes the node it's attached to
        /// </summary>
        public void TraverseConnection(string outPointId, string loopId)
        {
            for (int i = 0; i < Connections.Count; i++) 
            {
                if (Connections[i].outPoint != null && Connections[i].outPoint.Id == outPointId)
                {
                    Connections[i].IsDone = true;

                    ExecuteNode(Connections[i].inPoint.NodeId, loopId);
                }
            }
        }

        /// <summary>
        /// Wakes up Node thereby getting to the node's Execute function
        /// </summary>
        public void ExecuteNode(string NodeId, string loopId)
        {
            StoryNode storyNode = GetNodeById(NodeId);
            if (storyNode != null)
            {
                storyNode.WakeUpNode(loopId);
            }
        }

        /// <summary>
        /// Based on the logic gate's state, this will return true if it meets that state's requirements (boolean function)
        /// </summary>
        public bool PassesOrGate(OrLogicGate logicNode)
        {
            string inPointId = logicNode.inPoint.Id;

            int sameTimesVisited = -1;

            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i].inPoint.Id == inPointId)
                {
                    StoryNode node = GetNodeById(Connections[i].outPoint.NodeId);

                    switch (logicNode.orState)
                    {
                        case OrLogicGate.OrState.IsDone:
                            if (node.storyNodeState == StoryNodeState.IsDone) return true;
                            break;
                        case OrLogicGate.OrState.IsDoneMoreThanOnce:
                            if (node.storyNodeState == StoryNodeState.IsDone && node.timesVisited > 1) return true;
                            break;
                        case OrLogicGate.OrState.IsDoneMoreThanOrEqualToOnce:
                            if (node.storyNodeState == StoryNodeState.IsDone && node.timesVisited >= 1) return true;
                            break;
                        case OrLogicGate.OrState.IsAwake:
                            if (node.storyNodeState == StoryNodeState.IsAwake) return true;
                            break;
                        case OrLogicGate.OrState.IsAsleep:
                            if (node.storyNodeState == StoryNodeState.IsAsleep) return true;
                            break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Based on the logic gate's state, this will return true if it meets that state's requirements (boolean function)
        /// </summary>
        public bool PassesAndGate(AndLogicGate logicNode)
        {
            string inPointId = logicNode.inPoint.Id;

            bool isAnd = true;

            int sameTimesVisited = -1;
            string loopId;

            //for each connection that leads to the And gate
            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i].inPoint.Id == inPointId)
                {
                    StoryNode node = GetNodeById(Connections[i].outPoint.NodeId);

                    switch (logicNode.andState)
                    {
                        case AndLogicGate.AndState.IsDone:
                            if (node.storyNodeState != StoryNodeState.IsDone) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsDoneSameNumberOfTimes:
                            if (sameTimesVisited == node.timesVisited) isAnd = true;

                            if (sameTimesVisited == -1)
                            {
                                sameTimesVisited = node.timesVisited;
                                isAnd = false;
                                break;
                            }
                            if (node.storyNodeState != StoryNodeState.IsDone) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsDoneInSameLoop:
                            if (logicNode.LoopId != node.LoopId) isAnd = false;

                            if (node.storyNodeState != StoryNodeState.IsDone) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsAwake:
                            if (node.storyNodeState != StoryNodeState.IsAwake) isAnd = false;
                            break;
                        case AndLogicGate.AndState.IsAsleep:
                            if (node.storyNodeState != StoryNodeState.IsAsleep) isAnd = false;
                            break;
                    }
                }
            }
            return isAnd;
        }




    }
}