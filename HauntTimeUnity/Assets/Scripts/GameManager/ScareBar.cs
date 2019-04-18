using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBar : MonoBehaviour
{
    public GameObject bar;
    public GameObject emptyBar;
    public GameObject fillSliver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillBar(int scarePercent)
    {

    }

    void ReduceBar(int scarePercent)
    {

    }
    
    void EmptyBar(int scarePercent)
    {
        bar = emptyBar;
    }
}
