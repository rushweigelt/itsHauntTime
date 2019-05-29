using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalManager : MonoBehaviour
{

    //threshold values for medals
    [Header("Medal Times")]
    public int bronzeThresh;
    public int silverThresh;
    public int goldThresh;

    [Header("Medal Colors")]
    public Color bronzeMedal;
    public Color silverMedal;
    public Color goldMedal;
    public Color noMedal;

    //colored clock obj
    [Space(10)]
    public Image clock;
    public Image medal;
    public Timer timer;
    
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
            setMedal(Color.grey);

        }
        else if (time <= bronzeThresh && time > silverThresh)
        {
            //bronze
            setMedal(bronzeMedal);
        }
        else if (time <= silverThresh && time > goldThresh)
        {
            //silver
            setMedal(silverMedal);
        }
        else if (time <= goldThresh)
        {
            //gold
            setMedal(goldMedal);
        }
    }

    void setMedal(Color color) {
        clock.color = color;
        medal.color = color;
    }
}
