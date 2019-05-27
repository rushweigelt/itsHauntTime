using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalManager : MonoBehaviour
{

    //threshold values for medals
    public int bronzeThresh;
    public int silverThresh;
    public int goldThresh;

    //colored clock obj
    public Image clock;
    public Image medal;

    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetermineMedal(timer.seconds);
    }
    //determine the medal for endgame and medal-symbol color
    public void DetermineMedal(float time)
    {
        if (time > bronzeThresh)
        {
            //no medal
            Debug.Log("No Medal");
            clock.color = Color.gray;
            medal.color = Color.gray;

        }
        else if (time <= bronzeThresh && time > silverThresh)
        {
            //bronze
            Debug.Log("Bronze Medal");
            clock.color = new Color(150, 116, 68);
            medal.color = new Color(150, 116, 68);
        }
        else if (time <= silverThresh && time > goldThresh)
        {
            //silver
            Debug.Log("Silver Medal");
            clock.color = new Color(192, 192, 192);
            medal.color = new Color(192, 192, 192);
        }
        else if (time <= goldThresh)
        {
            //gold
            Debug.Log("Gold Medal");
            clock.color = new Color(255, 215, 0);
            medal.color = new Color(255, 215, 0);

        }

    }
}
