using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace StoryGraph
{
    public class NodeBasedStoryEditor : EditorWindow
    {

        private GUIStyle LockStyle = new GUIStyle();

        private bool lockedInspector = false;



        #region MODEL
        Texture2D backgroundTexture;

        private Vector2 offset;
        private Vector2 drag;

        private Color backgroundColor;
        private Color smallLineColor;
        private Color thickLineColor;
        private Rect clippedArea;
        

        #endregion


        #region OPEN WINDOW
        [MenuItem("Tools/StoryGraph Editor")]
        private static void OpenWindow()
        {
            NodeBasedStoryEditor window = GetWindow<NodeBasedStoryEditor>();
            window.titleContent = new GUIContent("StoryGraph Editor");
        }
        #endregion


        private StoryGraph SelectedStoryGraph;


        private void OnEnable()
        {
            drawLockedStyle();

            backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            smallLineColor = new Color(0.25f, 0.25f, 0.25f);
            thickLineColor = new Color(0.35f, 0.35f, 0.35f);
        }

        private void setHideFlags()
        {

            if (SelectedStoryGraph.Nodes != null)
            {
                for(int i = 0; i < SelectedStoryGraph.Nodes.Count; i++)
                {
                    SelectedStoryGraph.Nodes[i].gameObject.hideFlags = HideFlags.HideInHierarchy;
                }

                StoryGraph prefab = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(SelectedStoryGraph) as StoryGraph;

                if (prefab != null)
                {
                    for(int i = 0; i < SelectedStoryGraph.Nodes.Count; i++)
                    {
                        prefab.Nodes[i].gameObject.hideFlags = HideFlags.HideInHierarchy;
                    }
                }
            }
            if (SelectedStoryGraph.Connections != null)
            {
                for(int i = 0; i < SelectedStoryGraph.Connections.Count; i++)
                {
                    SelectedStoryGraph.Connections[i].gameObject.hideFlags = HideFlags.HideInHierarchy;
                }

                StoryGraph prefab = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(SelectedStoryGraph) as StoryGraph;

                if (prefab != null)
                {
                    for(int i = 0; i < SelectedStoryGraph.Connections.Count; i++)
                    {
                        prefab.Connections[i].gameObject.hideFlags = HideFlags.HideInHierarchy;
                    }
                }
            }
        }

        private void ShowButton(Rect rect)
        {

            drawLockedStyle();
            Rect lockRect = new Rect(rect.x + 2, rect.y + 2, rect.width - 4, rect.height - 4);
            if (GUI.Button(lockRect, GUIContent.none, LockStyle))
            {
                Debug.Log("Locked");
                lockedInspector = !lockedInspector;
            }
        }

        private void drawLockedStyle()
        {
            if (lockedInspector)
            {
                Texture2D icon = EditorGUIUtility.Load("builtin skins/lightskin/images/in lockbutton on.png") as Texture2D;
                LockStyle.normal.background = icon;
            }
            else
            {
                Texture2D icon = EditorGUIUtility.Load("builtin skins/lightskin/images/in lockbutton.png") as Texture2D;
                LockStyle.normal.background = icon;
            }
        }

        #region DRAW LOOP
        private void OnGUI()
        {

            

            DrawBackground();

            if (IsStoryGraphSelected() == true)
            {
                SelectedStoryGraph = Selection.gameObjects[0].GetComponent<StoryGraph>();
                drawGraph();
            }
            else if (IsStoryGraphSelected() == false && lockedInspector == true && SelectedStoryGraph != null)
            {
                drawGraph();
            }
            else
            {
                GUI.Label(new Rect(0, 0, position.width, position.height), "Select a GameObject in the Hierarchy with a StoryGraph Component attached to it.", StoryGraphStyles.WhiteTextHeaderStyle());
                SelectedStoryGraph = null;
            }

            Repaint();
        }

        private void drawGraph()
        {
            setHideFlags();

            clippedArea = Begin(SelectedStoryGraph.Zoom, new Rect(0.0f, 0.0f, position.width, position.height));

            DrawGrid(20, 0.2f, smallLineColor);
            DrawGrid(100, 0.4f, thickLineColor);

            DrawNodes();
            DrawConnections();

            DrawConnectionLine(Event.current);

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);
            
            End();
        }
        #endregion

        #region DRAWING FUNCTIONS

        private void DrawBackground()
        {
            if (backgroundTexture == null)
            {
                backgroundTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                backgroundTexture.SetPixel(0, 0, backgroundColor);
                backgroundTexture.Apply();
            }

            GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), backgroundTexture, ScaleMode.StretchToFill);
        }

        private bool IsStoryGraphSelected()
        {
            return Selection.gameObjects.Length > 0 && Selection.gameObjects[0].GetComponent<StoryGraph>() != null;
        }


        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(clippedArea.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(clippedArea.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, 0, 0) + newOffset, new Vector3(gridSpacing * i, clippedArea.height + 100, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(0, gridSpacing * j, 0) + newOffset, new Vector3(clippedArea.width + 100, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawNodes()
        {
            if (SelectedStoryGraph.Nodes != null)
            {
                for (int i = 0; i < SelectedStoryGraph.Nodes.Count; i++)
                {

                    if (ClipNode(SelectedStoryGraph.Nodes[i].rect))
                    {
                        SelectedStoryGraph.Nodes[i].Draw(true);
                    }
                    else
                    {
                        SelectedStoryGraph.Nodes[i].Draw(false);
                    }
                }
            }
        }

        private bool ClipNode(Rect rect)
        {
            float x1 = rect.x;
            float x2 = rect.x + rect.width;
            float y1 = rect.y;
            float y2 = rect.y + rect.height;
            return ((x1 > 0 && x1 < clippedArea.width) || (x2 > 0 && x2 < clippedArea.width)) && ((y1 > 0 && y1 < clippedArea.height) || (y2 > 0 && y2 < clippedArea.height));
        }

        private void DrawConnections()
        {
            if (SelectedStoryGraph.Connections != null)
            {
                for (int i = 0; i < SelectedStoryGraph.Connections.Count; i++)
                {
                    
                    SelectedStoryGraph.Connections[i].Draw();
                }
            }
        }

        private bool ClipConnection(ConnectionPoint inPoint, ConnectionPoint outPoint)
        {
            float x1 = inPoint.rect.center.x;
            float x2 = (outPoint.rect.center - Vector2.left * 50f).x;
            float y1 = inPoint.rect.center.y;
            float y2 = (outPoint.rect.center - Vector2.left * 50f).y;
            return (x1 > 0 && x1 < position.width) || (x2 > 0 && x2 < position.width) || (y1 > 0 && y1 < position.height) || (y2 > 0 && y2 < position.height);
        }

        private void DrawConnectionLine(Event e)
        {
            ConnectionPoint selectedOutPoint = SelectedStoryGraph.SelectedOutPoint;
            ConnectionPoint selectedInPoint = SelectedStoryGraph.SelectedInPoint;
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

        #endregion

        #region EVENT HANDLING

        private void ProcessEvents(Event e)
        {

            if (!ProcessNodeEvents(Event.current))
            {
                ProcessWindowEvents(Event.current);
            }


        }

        private void ProcessWindowEvents(Event e)
        {
            drag = Vector2.zero;

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        SelectedStoryGraph.ClearConnectionSelection();
                    }

                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        OnDrag(e.delta);
                    }
                    break;
                case EventType.ScrollWheel:
                    float offset = (e.delta.y*0.01f);
                    if(SelectedStoryGraph.Zoom - offset > 0.5f && SelectedStoryGraph.Zoom - offset < 1.5f )
                    SelectedStoryGraph.Zoom -= (e.delta.y*0.01f);
                    break;
            }
        }

        private bool ProcessNodeEvents(Event e)
        {
            bool eventFired = false;
            if (SelectedStoryGraph.Nodes != null)
            {
                for (int i = SelectedStoryGraph.Nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged;
                    guiChanged = SelectedStoryGraph.Nodes[i].ProcessEvents(e);

                    if (guiChanged)
                    {
                        GUI.changed = true;
                        eventFired = true;
                    }
                }
            }
            return eventFired;
        }


        private void OnDrag(Vector2 delta)
        {
            drag = delta;

            if (SelectedStoryGraph.Nodes != null)
            {
                for (int i = 0; i < SelectedStoryGraph.Nodes.Count; i++)
                {
                    SelectedStoryGraph.Nodes[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }


        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();

            IEnumerable<StoryNode> StoryNodeEnumberable = ReflectiveEnumeratorUtil.GetEnumerableOfType<StoryNode>();
            List<StoryNode> StoryNodes = StoryNodeEnumberable.ToList();

            for (int i = 0; i < StoryNodes.Count; i++)
            {
                genericMenu.AddItem(new GUIContent(StoryNodes[i].MenuNamePrefix+StoryNodes[i].MenuName), false, (storyNode) => OnClickAddStoryNode(storyNode, mousePosition), StoryNodes[i]);
            }

            genericMenu.ShowAsContext();
        }

        private void OnClickAddStoryNode(System.Object _storyObject, Vector2 mousePosition)
        {
            StoryNode _storyNode = (StoryNode)_storyObject;
            string menuName = _storyNode.MenuNamePrefix + _storyNode.MenuName;
            string _storyNodeName = menuName.Contains('/') ? menuName.Substring(menuName.LastIndexOf("/") + 1) : menuName;

            InitializeNodes();
            var storyNode = new GameObject().AddComponent(_storyNode.GetType()) as StoryNode;
            storyNode.gameObject.hideFlags = HideFlags.HideInHierarchy;
            storyNode.gameObject.transform.parent = SelectedStoryGraph.transform;

            storyNode.Initialize(_storyNodeName, mousePosition, 250, 90, SelectedStoryGraph);
            SelectedStoryGraph.Nodes.Add(storyNode);
        }

        private void InitializeNodes()
        {
            if (SelectedStoryGraph.Nodes == null)
            {
                SelectedStoryGraph.Nodes = new List<StoryNode>();
            }
        }

        private const float kEditorWindowTabHeight = 21.0f;
        private static Matrix4x4 _prevGuiMatrix;
    
        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup();
    
            Rect clippedArea = RectExtensions.ScaleSizeBy(screenCoordsArea, 1.0f / zoomScale, RectExtensions.TopLeft(screenCoordsArea));
            
            clippedArea.y += kEditorWindowTabHeight;
            GUI.BeginGroup(clippedArea);
    
            _prevGuiMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(RectExtensions.TopLeft(clippedArea), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));


            GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

            return clippedArea;
        }
    
        public static void End()
        {
            GUI.matrix = _prevGuiMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
        }


        #endregion
    }
}