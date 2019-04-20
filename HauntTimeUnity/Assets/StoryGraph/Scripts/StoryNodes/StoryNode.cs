using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace StoryGraph
{

    public enum StoryDrawer { PropertyField, UnityEvent, Array, NoLabelPropertyField, RadioButton, NoLabelReadOnly, IntegerField }
    public enum StoryNodeState { IsAsleep, IsAwake, IsDone, IsDisabled }
    public class StoryNode : MonoBehaviour, IComparable<StoryNode>
    {
        public virtual string MenuName { get { return "StoryNode"; } }
        public virtual string MenuNamePrefix { get { return ""; } }

        public string Id;
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;
        public StoryNodeState storyNodeState = StoryNodeState.IsAsleep;
        public int timesVisited = 0;
        public StoryGraph storyGraph;
        public string LoopId;

#if UNITY_EDITOR
        public Rect originalRect;
        public Rect rect;
        public string title;
        public float SerializedPropertyYOffset = 75;
        public float SerializedSpacing;
        public float ExtraPointSpacing = 0;
        public bool isDragged;
        public bool isSelected;
        public bool enableNode = true;

        public GUIStyle style;
        public GUIStyle disabledStyle;
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;
        public GUIStyle closeButtonStyle;
        public GUIStyle nodeHeaderStyle;
        public GUIStyle whiteTextStyle;
        public GUIStyle whiteRightTextStyle;
        public GUIStyle whiteTextHeaderStyle;
        public GUIStyle isAsleepStyle;
        public GUIStyle isAwakeStyle;
        public GUIStyle isDoneStyle;
        public GUIStyle whiteBoldTextStyle;
        public GUIStyle whiteVarNodeStyle;

        public SerializedObject serializedObject;

        public List<SerializedProperty> SerializedProperties;
        public List<string> Labels;
        public List<StoryDrawer> StoryDrawers;



        public StoryNode() { }
        public virtual void Initialize(string _title, Vector2 position, float width, float height, StoryGraph _storyGraph)
        {
            storyGraph = _storyGraph;

            Id = "Node_" + System.Guid.NewGuid().ToString();
            inPoint = new GameObject().AddComponent<ConnectionPoint>();
            inPoint.gameObject.hideFlags = HideFlags.HideInHierarchy;
            inPoint.gameObject.transform.parent = transform;
            inPoint.Initialize(Id, ConnectionPointType.In, StoryGraphStyles.InPointStyle(), storyGraph);

            outPoint = new GameObject().AddComponent<ConnectionPoint>();
            outPoint.gameObject.hideFlags = HideFlags.HideInHierarchy;
            outPoint.gameObject.transform.parent = transform;
            outPoint.Initialize(Id, ConnectionPointType.Out, StoryGraphStyles.OutPointStyle(), storyGraph);

            title = _title;
            rect = new Rect(position.x, position.y, width, height);
            originalRect = new Rect(position.x, position.y, width, height);
            SetStyles();

            InitializeSerializedProperties();
        }


        public virtual void SetStyles()
        {
            style = StoryGraphStyles.NodeStyle();
            disabledStyle = StoryGraphStyles.DisableNodeStyle();
            defaultNodeStyle = StoryGraphStyles.NodeStyle();
            selectedNodeStyle = StoryGraphStyles.SelectedNodeStyle();
            closeButtonStyle = StoryGraphStyles.CloseButtonStyle();
            nodeHeaderStyle = StoryGraphStyles.NodeHeaderStyle();
            whiteTextHeaderStyle = StoryGraphStyles.WhiteTextHeaderStyle();
            whiteTextStyle = StoryGraphStyles.WhiteTextStyle();
            whiteRightTextStyle = StoryGraphStyles.WhiteRightTextStyle();
            isAsleepStyle = StoryGraphStyles.IsAsleepStyle();
            isAwakeStyle = StoryGraphStyles.IsAwakeStyle();
            isDoneStyle = StoryGraphStyles.IsDoneStyle();
            whiteBoldTextStyle = StoryGraphStyles.WhiteBoldTextStyle();
            whiteVarNodeStyle = StoryGraphStyles.WhiteVarNodeStyle();
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public virtual void SetSerializedProperties()
        {
            FieldInfo[] objectFields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < objectFields.Length; i++)
            {
                StoryGraphField attribute = Attribute.GetCustomAttribute(objectFields[i], typeof(StoryGraphField)) as StoryGraphField;
                if (attribute != null)
                {
                    AddSerializedProperty(objectFields[i].Name, attribute.GetPropertyType());
                }
            }
        }

        public virtual void Draw(bool isVisible)
        {
            if (serializedObject == null)
            {
                serializedObject = new SerializedObject(this);
            }

            DrawConnectionPoints();

            GUI.BeginGroup(rect, style);

            DrawNode(isVisible);

            GUI.EndGroup();
        }

        protected void DrawConnectionPoints()
        {
            inPoint.UpdateLocation(rect.x, rect.y, rect.width, rect.height);
            inPoint.Draw();
            outPoint.UpdateLocation(rect.x, rect.y, rect.width, rect.height);
            outPoint.Draw();
        }

        protected void DrawNode(bool isVisible)
        {
            GUI.BeginGroup(new Rect(0, 0, rect.width, 50), nodeHeaderStyle);
            GUI.Label(new Rect(0, 0, rect.width, 50), title, whiteTextHeaderStyle);
            if (GUI.Button(new Rect(rect.width - 25, 5, 15, 15), "", closeButtonStyle))
            {
                DestroySelf();
                return;
            }



            if (GUI.Toggle(new Rect(8, 5, 15, 15), enableNode, "") != enableNode)
            {
                enableNode = !enableNode;

                if (enableNode == false)
                {
                    storyNodeState = StoryNodeState.IsDisabled;
                }

                if (enableNode == true)
                {
                    storyNodeState = StoryNodeState.IsAsleep;
                }
            }

            GUI.EndGroup();


            if (isVisible)
            {
                if (storyNodeState == StoryNodeState.IsDisabled)
                {
                    GUI.BeginGroup(new Rect(0, 42, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Is Disabled", isAsleepStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsAsleep)
                {
                    GUI.BeginGroup(new Rect(0, 42, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Asleep", isAsleepStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsDone && timesVisited == 1)
                {
                    GUI.BeginGroup(new Rect(0, 40, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Done", isDoneStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsDone && timesVisited > 1)
                {
                    GUI.BeginGroup(new Rect(0, 40, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Done(" + timesVisited + ")", isDoneStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsAwake && timesVisited > 1)
                {
                    GUI.BeginGroup(new Rect(0, 40, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Awake(" + timesVisited + ")", isAwakeStyle);
                    GUI.EndGroup();
                }
                else if (storyNodeState == StoryNodeState.IsAwake)
                {
                    GUI.BeginGroup(new Rect(0, 40, rect.width, 40));
                    GUI.Label(new Rect(15, 8, rect.width, 20), "Status:", whiteBoldTextStyle);
                    GUI.Label(new Rect(0, 8, rect.width - 20, 20), "Awake", isAwakeStyle);
                    GUI.EndGroup();
                }

                SetSerializedSpacing();
                DrawSerializedProperties();
            }
            else
            {
                if (SerializedProperties != null && SerializedProperties.Count > 0)
                {
                    SerializedProperties = null;
                    SerializedSpacing = 0;
                    SetRectSize();
                }
            }
        }

        public virtual void DestroySelf()
        {
            storyGraph.RemoveNode(this);
            DestroyImmediate(inPoint.gameObject, true);
            DestroyImmediate(outPoint.gameObject, true);
            DestroyImmediate(this.gameObject, true);
        }

        public void SetSerializedSpacing()
        {
            SerializedSpacing = GetSerializedSpacing();
            SetRectSize();
        }

        public float GetSerializedSpacing()
        {
            float spacing = 0;
            if (SerializedProperties != null && SerializedProperties.Count > 0)
            {
                for (int i = 0; i < SerializedProperties.Count; i++)
                {
                    spacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                }
                spacing += EditorGUIUtility.singleLineHeight * (SerializedProperties.Count - 1);
                spacing += (SerializedProperties.Count) * 2;
            }
            return spacing;
        }

        public void SetRectSize()
        {
            rect = new Rect(rect.x, rect.y, originalRect.width, originalRect.height + SerializedSpacing + ExtraPointSpacing);
        }

        public void InitializeSerializedProperties()
        {
            if (SerializedProperties == null || serializedObject == null)
            {
                SerializedProperties = new List<SerializedProperty>();
                Labels = new List<string>();
                StoryDrawers = new List<StoryDrawer>();
                serializedObject = new SerializedObject(this);

                SetSerializedProperties();
                SetSerializedSpacing();
            }
        }

        public virtual void DrawSerializedProperties()
        {
            InitializeSerializedProperties();
            if (SerializedProperties.Count > 0)
            {
                GUI.BeginGroup(new Rect(10, SerializedPropertyYOffset, rect.width, rect.height));

                serializedObject.Update();
                float currentSpacing = 0;
                for (int i = 0; i < SerializedProperties.Count; i++)
                {
                    if (StoryDrawers[i] == StoryDrawer.NoLabelPropertyField)
                    {
                        GUI.BeginGroup(new Rect(0, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height));
                        EditorGUI.PropertyField(new Rect(0, currentSpacing, rect.width - 20, rect.height), SerializedProperties[i], GUIContent.none, true);
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                    else if (StoryDrawers[i] == StoryDrawer.PropertyField)
                    {
                        GUI.Label(new Rect(5, (currentSpacing) + (i * 2) + EditorGUIUtility.singleLineHeight * i, rect.width, 50), Labels[i], whiteTextStyle);
                        GUI.BeginGroup(new Rect(rect.width / 2, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height), whiteTextStyle);
                        EditorGUI.PropertyField(new Rect(0, currentSpacing, (rect.width / 2) - 20, rect.height), SerializedProperties[i], GUIContent.none, true);
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }

                    else if (StoryDrawers[i] == StoryDrawer.UnityEvent || StoryDrawers[i] == StoryDrawer.Array || StoryDrawers[i] == StoryDrawer.IntegerField)
                    {
                        GUI.BeginGroup(new Rect(0, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height));
                        EditorGUI.PropertyField(new Rect(0, currentSpacing, rect.width - 20, rect.height), SerializedProperties[i], GUIContent.none, true);
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                    else if (StoryDrawers[i] == StoryDrawer.RadioButton)
                    {
                        GUI.Label(new Rect(5, (currentSpacing) + (i * 2) + EditorGUIUtility.singleLineHeight * i, rect.width, 50), Labels[i], whiteTextStyle);
                        GUI.BeginGroup(new Rect(rect.width - 45, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height), whiteTextStyle);
                        EditorGUI.PropertyField(new Rect(0, currentSpacing, rect.width - 20, rect.height), SerializedProperties[i], GUIContent.none, true);
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                    else if (StoryDrawers[i] == StoryDrawer.NoLabelReadOnly)
                    {
                        GUI.BeginGroup(new Rect(0, (EditorGUIUtility.singleLineHeight * i), rect.width, rect.height));
                        GUI.enabled = false;
                        EditorGUI.PropertyField(new Rect(0, currentSpacing, rect.width - 20, rect.height), SerializedProperties[i], GUIContent.none, true);
                        GUI.enabled = true;
                        GUI.EndGroup();
                        currentSpacing += EditorGUI.GetPropertyHeight(SerializedProperties[i], true);
                    }
                }
                serializedObject.ApplyModifiedProperties();
                GUI.EndGroup();
            }

        }

        public virtual void AddSerializedProperty(string propertyName)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(propertyName);
            StoryDrawers.Add(StoryDrawer.PropertyField);
        }

        public virtual void AddSerializedProperty(string propertyName, string label)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(label);
            StoryDrawers.Add(StoryDrawer.PropertyField);
        }

        public virtual void AddSerializedProperty(string propertyName, StoryDrawer StoryDrawer)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(propertyName);
            StoryDrawers.Add(StoryDrawer);
        }

        public virtual void AddSerializedProperty(string propertyName, string label, StoryDrawer StoryDrawer)
        {
            SerializedProperties.Add(serializedObject.FindProperty(propertyName));
            Labels.Add(label);
            StoryDrawers.Add(StoryDrawer);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.KeyDown:

                    if (isSelected && e.Equals(Event.KeyboardEvent(KeyCode.Delete.ToString())))
                    {
                        OnClickRemoveNode();
                    }
                    return true;
                    break;
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            isSelected = true;
                            storyGraph.SelectedNode = this;
                            SelectNode(true);
                            style = selectedNodeStyle;
                            return true;
                        }
                        else
                        {
                            isSelected = false;
                            SelectNode(false);
                            style = defaultNodeStyle;
                            return true;
                        }
                    }

                    if (e.button == 1 && rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        isSelected = true;
                        storyGraph.SelectedNode = this;
                        SelectNode(true);
                        style = selectedNodeStyle;

                        return true;

                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;

                    if (e.button == 1 && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                        return true;

                    }

                    break;

                case EventType.MouseDrag:
                    if (isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }

        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }
        private void OnClickRemoveNode()
        {
            DestroySelf();
        }
        public virtual void SelectNode(bool isSelected)
        {
            storyGraph.SetConnectionsSelected(this, isSelected);
        }
#endif

        /// <summary>
        /// Needed for IComparable. Sets order of MenuItems. Currently set to 0, meaning order of MenuItems are random
        /// </summary>
        public int CompareTo(StoryNode storyNode)
        {
            return 0;
        }

        /// <summary>
        /// Sets node state to IsAwake and Executes the node's logic
        /// </summary>
        public virtual void WakeUpNode(string _loopId)
        {

            if (_loopId == "start" || LoopId == _loopId)
            {
                LoopId = "Loop_" + Time.time + System.Guid.NewGuid().ToString();
            }
            else
            {
                LoopId = _loopId;
            }

            if (storyNodeState != StoryNodeState.IsDisabled)
            {
                storyNodeState = StoryNodeState.IsAwake;
                Execute();
            }
        }

        /// <summary>
        /// Sets state to IsDone, iterates times visited, then goes to the next node via the outpoint.
        /// </summary>
        public void GoToNextNode()
        {
            storyNodeState = StoryNodeState.IsDone;
            timesVisited++;
            outPoint.GoToNextNode(LoopId);
        }
        /// <summary>
        /// Execute is called either on Start, or when a Connection traverses to this StoryNode.   
        /// </summary>
        public virtual void Execute()
        {

        }

        /// <summary>
        /// Sets node state to IsAsleep 
        /// </summary>
        virtual public void DisableNode()
        {
            storyNodeState = StoryNodeState.IsAsleep;
        }
    }
}
