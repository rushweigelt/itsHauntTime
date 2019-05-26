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

    public TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        // maxSecs = seconds;
    }
    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        SecondsToMmSs(seconds);
        time = SecondsToMmSs(seconds);
        txt.text = time;
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
        return string.Format("{0}:{1}", Mathf.Floor(seconds/59), Mathf.RoundToInt(seconds%59));
    }
    void RunTimer()
    {
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        seconds += 1;
    }

}
