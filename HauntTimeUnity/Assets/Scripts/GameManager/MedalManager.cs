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
    public Color bronze;
    public Color silver;
    public Color gold;
    public Color noMedal;

    //colored clock obj
    //[Space(10)]
    public Image clock;
    public Sprite goldMedal;
    public Sprite silverMedal;
    public Sprite bronzeMedal;
    public Image currentMedal;
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
            setTimerColor(Color.grey);

        }
        else if (time <= bronzeThresh && time > silverThresh)
        {
            //bronze
            setTimerColor(bronze);
            currentMedal.sprite = bronzeMedal;
        }
        else if (time <= silverThresh && time > goldThresh)
        {
            //silver
            setTimerColor(silver);
            currentMedal.sprite = silverMedal;
        }
        else if (time <= goldThresh)
        {
            //gold
            setTimerColor(gold);
            currentMedal.sprite = goldMedal;
        }
    }

    void setTimerColor(Color color) {
        clock.color = color;
        //medal.color = color;
    }
}
