using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace StoryGraph
{
    public class OnPlayableDirectorFinished : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public PlayableDirector playableDirector;


        public override string MenuName {get{return "Timeline/On Playable Director Finished";}}

        public override void Execute()
        {
            storyGraph.StartCoroutine(AudioFinished());
        }


        public IEnumerator AudioFinished()
        {
            if(TurnOffOnExecute){
                while (playableDirector.time != playableDirector.duration)
                {
                    yield return null;
                }
                GoToNextNode();
            } 
            yield return null;
        }
    }
}
