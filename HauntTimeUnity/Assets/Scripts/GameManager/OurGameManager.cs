using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurGameManager : MonoBehaviour
{
    public HUDManager hud;
    public Timer timer;
    public ScareTarget target;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        WinState(target.state);
        LoseState(timer);
    }

    void WinState(string state)
    {
        if(target.state.Equals("Scared"))
        {
            Debug.Log("Player has won");
            //gold
            if (timer.seconds > timer.maxSecs/2)
            {

            }
            //silver
            else if (timer.seconds <= timer.maxSecs/2 && timer.seconds > timer.maxSecs/3)
            {

            }
            //bronze
            else
            {

            }
            //bring up load screen
        }
    }

    void LoseState(Timer t)
    {
        if (t.seconds <= 0)
        {
            Debug.Log("The Player Has Lost");
            timer.seconds = 0;
            //bring up restart/exit menu here?
        }
    }
}
