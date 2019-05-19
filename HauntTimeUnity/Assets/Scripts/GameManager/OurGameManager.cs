using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OurGameManager : MonoBehaviour
{
    public bool paused;

    public UnityEvent onPaused;

    public UnityEvent onUnpaused;

    public HUDManager hud;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        if(!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            onPaused.Invoke();
        }
    }

    public void unPause()
    {
        if(paused)
        {
            paused = false;
            Time.timeScale = 1f;
            onUnpaused.Invoke();
        }
    }
}
