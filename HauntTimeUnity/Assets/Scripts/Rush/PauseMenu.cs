using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject gameOver;

    // Use this for initialization
    void Start()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Joystick1Button9)))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                Pause();
                //print("here");
            }
            else if (pauseMenu.activeInHierarchy)
            {
                UnPause();
            }
        }

    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        //print("here");
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    
}
