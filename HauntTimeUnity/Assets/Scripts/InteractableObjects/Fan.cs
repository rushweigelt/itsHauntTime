using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Electronic
{
    public BoxCollider2D fanRange;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && this.isOn == true)
        {
            //push player via basic physics or changing their transform location over time.

        }
    }




}
