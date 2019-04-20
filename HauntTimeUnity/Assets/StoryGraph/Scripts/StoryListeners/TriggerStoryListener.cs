using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryGraph
{
	public class TriggerStoryListener : StoryListener {

		public void Trigger(){
			if(StoryListenerAction != null)
			{
				StoryListenerAction.Invoke();
			}
		}
	}
}

