using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class OnStartListener : StoryListener
    {
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void Start()
        {
            if (IsListenerSet)
            {
                Debug.Log("Yay it works!!");
                StoryListenerAction.Invoke();
            }
        }
    }
}

