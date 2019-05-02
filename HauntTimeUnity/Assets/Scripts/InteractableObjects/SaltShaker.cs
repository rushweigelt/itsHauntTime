using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : SpriteSwapInteraction
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    //NPC interact with salt shaker
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("NPC"))
        {
            Debug.Log("NPC passed collider, interacted with SaltShaker");
            Interact();
        }
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Test");
        }
    }
}
