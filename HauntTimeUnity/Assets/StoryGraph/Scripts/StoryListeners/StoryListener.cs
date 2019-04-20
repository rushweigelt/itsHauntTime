using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryGraph
{
    public class StoryListener : MonoBehaviour
    {

        public UnityAction StoryListenerAction;

        [HideInInspector]
        public bool IsListenerSet = false;

    }
}