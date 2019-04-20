using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
    public class OnTriggerEnterListener : StoryListener
    {

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnTriggerEnter()
        {
            if (StoryListenerAction != null)
            {
                StoryListenerAction.Invoke();
            }
        }

    }
}
