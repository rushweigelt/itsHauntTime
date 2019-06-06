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

    [Range(0,1)]
    public float inactiveMedalAlpha;

    //colored clock obj
    //[Space(10)]
    public Image clock;
    public Sprite goldMedalSprite;
    public Sprite silverMedalSprite;
    public Sprite bronzeMedalSprite;

    public Image goldMedal;
    public Image silverMedal;
    public Image bronzeMedal;

    public Timer timer;

    private void Start()
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
            setTimerColor(Color.grey);

        }
        else if (time <= bronzeThresh && time > silverThresh)
        {
            //bronze
            setTimerColor(bronze);

            setMedalAlpha(bronzeMedal, 1);
            setMedalAlpha(silverMedal, inactiveMedalAlpha);
            setMedalAlpha(goldMedal, inactiveMedalAlpha);

            //goldMedal.sprite = bronzeMedalSprite;
        }
        else if (time <= silverThresh && time > goldThresh)
        {
            //silver
            setTimerColor(silver);

            setMedalAlpha(silverMedal, 1);
            setMedalAlpha(bronzeMedal, inactiveMedalAlpha);
            setMedalAlpha(goldMedal, inactiveMedalAlpha);
            //goldMedal.sprite = silverMedalSprite;
        }
        else if (time <= goldThresh)
        {
            //gold

            setTimerColor(gold);

            setMedalAlpha(goldMedal, 1);
            setMedalAlpha(bronzeMedal, inactiveMedalAlpha);
            setMedalAlpha(silverMedal, inactiveMedalAlpha);
            //goldMedal.sprite = goldMedalSprite;
        }
    }

    void setMedalAlpha(Image medal, float alpha)
    {
        Color color = medal.color;
        color.a = alpha;
        medal.color = color;
    }

    void setTimerColor(Color color) {
        clock.color = color;
        //medal.color = color;
    }
}
