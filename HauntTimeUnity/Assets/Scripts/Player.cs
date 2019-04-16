using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject interactPrompt;

    // Start is called before the first frame update
    void Start()
    {
        //turn off interactive prompt at start of scene.
        interactPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
