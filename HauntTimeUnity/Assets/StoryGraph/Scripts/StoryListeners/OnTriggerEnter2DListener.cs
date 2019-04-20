using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class OnTriggerEnter2DListener : StoryListener
    {

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnTriggerEnter2D()
        {
            if (StoryListenerAction != null)
            {
                StoryListenerAction.Invoke();
            }
        }

    }
}
