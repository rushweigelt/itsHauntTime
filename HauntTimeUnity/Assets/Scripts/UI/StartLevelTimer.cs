using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTimer : MonoBehaviour
{

    public GameObject[] flashes;

    public float scaleMin;

    public int currentlyShown;
    public float numberTimer;

    public void Start()
    {
        currentlyShown = 0;
        flashes[currentlyShown].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if(currentlyShown == flashes.Length)
        {
            GlobalManager.Instance.controlLockOn = false;
            Destroy(gameObject);
        }
        numberTimer += Time.deltaTime;

        if(numberTimer >= 1 && currentlyShown < flashes.Length)
        {
            flashes[currentlyShown].SetActive(false);
            currentlyShown++;
            numberTimer = 0;
            flashes[currentlyShown].SetActive(true);
        }

        float scaleLerp = Mathf.Lerp(1, scaleMin, numberTimer);
        flashes[currentlyShown].transform.localScale = new Vector3(scaleLerp, scaleLerp, scaleLerp);

    }
}
