using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitandRestart : MonoBehaviour
{
    public GameObject pauseCont;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseMenu pause = pauseCont.GetComponent<PauseMenu>();
        pause.UnPause();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
