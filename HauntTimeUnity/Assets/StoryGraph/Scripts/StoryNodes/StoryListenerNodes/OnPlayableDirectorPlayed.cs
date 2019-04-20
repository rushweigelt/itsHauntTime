using UnityEngine;
using UnityEngine.Playables;

namespace StoryGraph
{
    public class OnPlayableDirectorPlayed : ListenerNode
    {
        [StoryGraphField(StoryDrawer.RadioButton)] public bool TurnOffOnExecute = true;
        [StoryGraphField] public PlayableDirector playableDirector;


        public override string MenuName {get{return "Timeline/On Playable Director Played";}}

        public override void Execute()
        {
            Debug.Log(Id + " is Initialized");
            if(playableDirector != null)
            {
                playableDirector.played += OnListener;
            }
        }

        public void OnListener(PlayableDirector _playableDirector)
        {

            if(TurnOffOnExecute)
            {
                playableDirector.played -= OnListener;
            }
            GoToNextNode();
        }
    }
}
