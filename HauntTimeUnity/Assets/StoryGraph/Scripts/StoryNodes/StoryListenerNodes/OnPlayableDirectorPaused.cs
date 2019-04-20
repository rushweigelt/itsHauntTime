using UnityEngine;
using UnityEngine.Playables;

namespace StoryGraph
{
    public class OnPlayableDirectorPaused : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public PlayableDirector playableDirector;


        public override string MenuName {get{return "Timeline/On Playable Director Paused";}}

        public override void Execute()
        {
            Debug.Log(Id + " is Initialized");
            if(playableDirector != null)
            {
                playableDirector.paused += OnListener;
            }
        }

        public void OnListener(PlayableDirector _playableDirector)
        {

            if(TurnOffOnExecute)
            {
                playableDirector.paused -= OnListener;
            }
            GoToNextNode();
        }
    }
}
