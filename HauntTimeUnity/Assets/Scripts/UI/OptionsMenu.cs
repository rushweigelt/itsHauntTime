using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource _as;
    public bool muteOn;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    //public Toggle t;
    // Start is called before the first frame update
    void Start()
    {
        muteOn = false;
    }

    public void ToggleMute(Toggle t)
    {
        if (t.isOn)
        {
            Debug.Log("muted");
            muteOn = true;
            _as.volume = 0f;
        }
        else
        {
            Debug.Log("Unmuted");
            muteOn = false;
            _as.volume = 1f;
        }
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void closeOptions()
    {
        optionsMenu.SetActive(false);
    }
}
