using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDManager : Singleton<HUDManager>
{
    public Timer timer;

    public UnityEvent onPaused;

    public UnityEvent onUnpaused;

    public UnityEvent onGameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public string DisplayTimer( Timer t)
    {
        return t.SecondsToMmSs(t.seconds);
    } */
}
