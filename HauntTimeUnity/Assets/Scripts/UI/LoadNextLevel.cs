using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{


    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
