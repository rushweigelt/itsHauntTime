using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OurGameManager : MonoBehaviour
{
    public HUDManager hud;
    public Timer timer;
    public ScareTarget target;
    public Text winText;
    public GameObject textObj;
    
    // Start is called before the first frame update
    void Start()
    {
        textObj.SetActive(false);
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
            textObj.SetActive(true);
            winText.text = "Player has Won!";
            Time.timeScale = 0;
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
            textObj.SetActive(true);
            winText.text = "Player has Lost!";
            Debug.Log("The Player Has Lost");
            timer.seconds = 0;
            Time.timeScale = 0;
            //bring up restart/exit menu here?
        }
    }
}
