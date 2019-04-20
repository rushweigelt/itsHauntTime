using UnityEngine;
using UnityEngine.Playables;

namespace StoryGraph
{
    public class OnPlayableDirectorStopped : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public PlayableDirector playableDirector;


        public override string MenuName {get{return "Timeline/On Playable Director Stopped";}}

        public override void Execute()
        {
            Debug.Log(Id + " is Initialized");
            if(playableDirector != null)
            {
                playableDirector.stopped += OnListener;
            }
        }

        public void OnListener(PlayableDirector _playableDirector)
        {

            if(TurnOffOnExecute)
            {
                playableDirector.stopped -= OnListener;
            }
            GoToNextNode();
        }
    }
}
