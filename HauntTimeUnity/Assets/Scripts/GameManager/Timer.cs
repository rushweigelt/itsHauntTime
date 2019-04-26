using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //public int minutes;
    public float seconds;
    public string time;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;
        SecondsToMmSs(seconds);
        //Debug.Log(SecondsToMmSs(seconds));
    }
    void SetTime(float seconds)
    {
        SecondsToMmSs(seconds);
    }
    void GetTime()
    {

    }
    string SecondsToMmSs(float seconds)
    {
        return string.Format("{0}:{1}", Mathf.Floor(seconds/60), Mathf.RoundToInt(seconds%60));
    }
    void RunTimer()
    {
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        seconds -= 1;
    }

}
