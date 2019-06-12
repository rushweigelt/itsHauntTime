using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //public int minutes;
    public float seconds;
    // public float maxSecs;
    public string time;

    public TextMeshProUGUI finalTimeTxt;

    public TextMeshProUGUI timerTxt;

    //bool to control timer
    public bool t;
    // Start is called before the first frame update
    void Start()
    {
        t = true;
        // maxSecs = seconds;
    }
    // Update is called once per frame
    void Update()
    {
        if (t)
        {
            RunTimer();
            SecondsToMmSs(seconds);
            time = SecondsToMmSs(seconds);
            timerTxt.text = time;
        }
        else
        {
            SecondsToMmSs(seconds);
            time = SecondsToMmSs(seconds);
            timerTxt.text = time;
        }
    }
    void SetTime(float seconds)
    {
        SecondsToMmSs(seconds);
    }
    void GetTime()
    {

    }
    public string SecondsToMmSs(float seconds)
    {
        return string.Format("{0}:{1}", Mathf.Floor(seconds/59), Mathf.RoundToInt(seconds%59).ToString("D2"));
    }
    public void RunTimer()
    {
        seconds += Time.deltaTime;
    }
    /* does not work, seconds go insane for some reason. This would be preferable to update tho, I agree
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1f);
        seconds += 1;
    }
    */

    public void TimerOff()
    {
        if (t)
        {
            t = false;
        }
    }

    public void TimerOn()
    {
        if(!t)
        {
            t = true;
        }
    }

    public void SetFinalTime()
    {
        finalTimeTxt.text = string.Format("Your Time: {0}:{1}", Mathf.Floor(seconds / 59), Mathf.RoundToInt(seconds % 59).ToString("D2"));
    }

}
