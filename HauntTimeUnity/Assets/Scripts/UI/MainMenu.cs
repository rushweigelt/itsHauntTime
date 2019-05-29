using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject currentMenu;

    public void Start()
    {
        currentMenu = mainMenu;
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void GoNextMenu(GameObject nextMenu)
    {
        nextMenu.SetActive(true);
        currentMenu.SetActive(false);
        currentMenu = nextMenu;
    }
}
