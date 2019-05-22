using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public enum Scene {
        MAIN_MENU,
        GAME,
        CURRENT
    }

    public Scene scene;

    public Dictionary<Scene, string> sceneDict = new Dictionary<Scene, string>() {
        {Scene.MAIN_MENU, "MainMenu"},
        {Scene.GAME, "Prototype Test Floor"},
        {Scene.CURRENT, null}
    };

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        if(scene.Equals(Scene.CURRENT))
        {
            Debug.Log("Reloading current scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Loading scene " + scene);
            SceneManager.LoadScene(sceneDict[scene]);
        }
        
    }
}
