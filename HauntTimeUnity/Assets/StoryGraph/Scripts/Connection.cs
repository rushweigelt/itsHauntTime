using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace StoryGraph
{
    public class Connection : MonoBehaviour
    {
        public string Id;
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public bool IsDone = false;


        public StoryGraph storyGraph;



#if UNITY_EDITOR
        public bool isSelected = false;
        private float AnimateDuration = 0.5f;
        private float AnimateJourney = -1f;
        public AnimationCurve animationCurve;

        public Action<Connection> OnClickRemoveConnection;
        public Connection() { }

        /// <summary>
        /// Constructor used instead of normal class constructor to integrate easier with GameObject Instantiate function 
        /// </summary>
        public void Initialize(ConnectionPoint inPoint, ConnectionPoint outPoint, StoryGraph _storyGraph)
        {
            this.Id = "Connection_" + System.Guid.NewGuid().ToString(); ;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
            this.storyGraph = _storyGraph;
            animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        }

        /// <summary>
        /// Draws BezierCurve based on whether it's currently selected or already attached to nodes.
        /// </summary>
        public virtual void Draw()
        {
            if (IsDone)
            {
                if(AnimateJourney < 0f){
                    float AnimateJourney = 0f;
                }

                if (AnimateJourney < AnimateDuration)
                {
                    AnimateJourney = AnimateJourney + Time.deltaTime;
                    float percent = Mathf.Clamp01(AnimateJourney / AnimateDuration);

                    float curvePercent = animationCurve.Evaluate(percent);

                    Handles.DrawBezier(
                        inPoint.rect.center,
                        outPoint.rect.center,
                        inPoint.rect.center + Vector2.left * 50f,
                        outPoint.rect.center - Vector2.left * 50f,
                        Color.Lerp(Color.green, Color.white, curvePercent),
                        null,
                        Mathf.Lerp(10f, 2f, curvePercent)
                    );
                } else {
                    IsDone = false;
                    AnimateJourney = -1f;
                }
            }
            else if (isSelected)
            {
                Handles.DrawBezier(
                    inPoint.rect.center,
                    outPoint.rect.center,
                    inPoint.rect.center + Vector2.left * 50f,
                    outPoint.rect.center - Vector2.left * 50f,
                    Color.white,
                    null,
                    6f
                );

            }
            else
            {
                Handles.DrawBezier(
                    inPoint.rect.center,
                    outPoint.rect.center,
                    inPoint.rect.center + Vector2.left * 50f,
                    outPoint.rect.center - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );
            }


            if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.CircleHandleCap))
            {
                storyGraph.RemoveConnection(this);
            }
        }
#endif
    }
}