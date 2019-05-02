using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBoxToggle : MonoBehaviour
{

    public GameObject powerSource;

    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (powerSource.GetComponent<ElectricalOutlet>().pluggedIn)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
