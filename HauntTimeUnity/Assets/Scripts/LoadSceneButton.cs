using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public enum Scene {
        MAIN_MENU,
        GAME
    }

    public Scene scene;

    public Dictionary<Scene, string> sceneDict = new Dictionary<Scene, string>() {
        {Scene.MAIN_MENU, "MainMenu"},
        {Scene.GAME, "Prototype"}
    };

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        Debug.Log("Loading scene " + scene);
        SceneManager.LoadScene(sceneDict[scene]);
    }
}
