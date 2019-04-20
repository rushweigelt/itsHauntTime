#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StoryGraph
{
    public static class StoryGraphStyles
    {
        public static GUIStyle NodeStyle()
        {
            GUIStyle nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeStyle;
        }

        public static GUIStyle DisableNodeStyle()
        {
            GUIStyle nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeStyle;
        }

        public static GUIStyle NodeHeaderStyle()
        {
            GUIStyle nodeHeaderStyle = new GUIStyle();
            nodeHeaderStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0.png") as Texture2D;
            nodeHeaderStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeHeaderStyle;
        }
        public static GUIStyle SelectedNodeStyle()
        {
            GUIStyle selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
            return selectedNodeStyle;
        }
        public static GUIStyle NodeActionStyle()
        {
            GUIStyle nodeActionStyle = new GUIStyle();
            nodeActionStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node6.png") as Texture2D;
            nodeActionStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeActionStyle;
        }
        public static GUIStyle NodeEventStyle()
        {
            GUIStyle nodeEventStyle = new GUIStyle();
            nodeEventStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1.png") as Texture2D;
            nodeEventStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeEventStyle;
        }
        public static GUIStyle NodeLogicStyle()
        {
            GUIStyle nodeLogicStyle = new GUIStyle();
            nodeLogicStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
            nodeLogicStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeLogicStyle;
        }
        public static GUIStyle NodeConditionStyle()
        {
            GUIStyle nodeConditionStyle = new GUIStyle();
            nodeConditionStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2.png") as Texture2D;
            nodeConditionStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeConditionStyle;
        }
        public static GUIStyle NodeCoroutineStyle()
        {
            GUIStyle nodeCoroutineStyle = new GUIStyle();
            nodeCoroutineStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
            nodeCoroutineStyle.border = new RectOffset(12, 12, 12, 12);
            return nodeCoroutineStyle;
        }
        public static GUIStyle WhiteTextHeaderStyle()
        {
            GUIStyle whiteTextHeaderStyle = new GUIStyle();
            whiteTextHeaderStyle.normal.textColor = Color.white;
            whiteTextHeaderStyle.alignment = TextAnchor.MiddleCenter;
            whiteTextHeaderStyle.fontSize = 14;
            whiteTextHeaderStyle.fontStyle = FontStyle.Bold;
            return whiteTextHeaderStyle;
        }
        public static GUIStyle WhiteTextStyle()
        {
            GUIStyle whiteTextStyle = new GUIStyle();
            whiteTextStyle.normal.textColor = Color.white;
            whiteTextStyle.alignment = TextAnchor.UpperLeft;
            return whiteTextStyle;
        }
        public static GUIStyle WhiteRightTextStyle()
        {
            GUIStyle WhiteRightTextStyle = new GUIStyle();
            WhiteRightTextStyle.normal.textColor = Color.white;
            WhiteRightTextStyle.fontStyle = FontStyle.Bold;
            WhiteRightTextStyle.alignment = TextAnchor.UpperRight;
            WhiteRightTextStyle.fontSize = 13;
            return WhiteRightTextStyle;
        }
        public static GUIStyle WhiteBoldTextStyle()
        {
            GUIStyle whiteBoldTextStyle = new GUIStyle();
            whiteBoldTextStyle.normal.textColor = Color.white;
            whiteBoldTextStyle.fontStyle = FontStyle.Bold;
            return whiteBoldTextStyle;
        }
        public static GUIStyle SelectedNodeActionStyle()
        {
            GUIStyle selectedNodeActionStyle = new GUIStyle();
            selectedNodeActionStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node6 on.png") as Texture2D;
            selectedNodeActionStyle.border = new RectOffset(12, 12, 12, 12);
            selectedNodeActionStyle.normal.textColor = Color.white;
            return selectedNodeActionStyle;
        }
        public static GUIStyle InPointStyle()
        {
            GUIStyle inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn right.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn right on.png") as Texture2D;
            inPointStyle.border = new RectOffset(2, 2, 15, 15);
            return inPointStyle;
        }
        public static GUIStyle OutPointStyle()
        {
            GUIStyle outPointStyle = new GUIStyle();
            outPointStyle.alignment = TextAnchor.MiddleCenter;
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/lightskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);
            return outPointStyle;
        }
        public static GUIStyle CloseButtonStyle()
        {
            GUIStyle closeButtonStyle = new GUIStyle();
            closeButtonStyle.normal.background = EditorGUIUtility.Load("icons/d_winbtn_mac_close_a.png") as Texture2D;
            return closeButtonStyle;
        }
        public static GUIStyle IsAsleepStyle()
        {
            GUIStyle isAsleepStyle = new GUIStyle();
            isAsleepStyle.normal.textColor = new Color(1f, 0.5f, 0.5f, 1);
            isAsleepStyle.alignment = TextAnchor.UpperRight;
            return isAsleepStyle;
        }
        public static GUIStyle IsAwakeStyle()
        {
            GUIStyle isAwakeStyle = new GUIStyle();
            isAwakeStyle.normal.textColor = new Color(1f, 1f, 0, 1);
            isAwakeStyle.alignment = TextAnchor.UpperRight;
            return isAwakeStyle;
        }
        public static GUIStyle IsDoneStyle()
        {
            GUIStyle isDoneStyle = new GUIStyle();
            isDoneStyle.normal.textColor = new Color(0.25f, 1f, 0.25f, 1);
            isDoneStyle.alignment = TextAnchor.UpperRight;
            return isDoneStyle;
        }
        public static GUIStyle WhiteVarNodeStyle()
        {
            GUIStyle whiteVarNodeStyle = new GUIStyle();
            whiteVarNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/varnode0 on.png") as Texture2D;
            whiteVarNodeStyle.border = new RectOffset(20, 20, 20, 20);
            return whiteVarNodeStyle;
        }
    }
}
#endif