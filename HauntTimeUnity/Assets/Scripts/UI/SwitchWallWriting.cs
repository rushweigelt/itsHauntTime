using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWallWriting : MonoBehaviour
{

    public GameObject[] textDisplays;
    public int currentlyShown;

    public float textTimer;
    public float textLimit;

    public void Start()
    {
       

        textDisplays[0].SetActive(true);
        currentlyShown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalManager.Instance.controlLockOn)
        {
            textTimer += Time.deltaTime;
            if (textTimer >= textLimit)
            {
                textTimer = 0;
                textDisplays[currentlyShown].SetActive(false);
                currentlyShown++;
                if (currentlyShown == textDisplays.Length)
                {
                    currentlyShown = 0;
                }
                textDisplays[currentlyShown].SetActive(true);
            }
        }
        
    }
}
