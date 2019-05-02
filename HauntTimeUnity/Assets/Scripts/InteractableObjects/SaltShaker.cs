using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : SpriteSwapInteraction
{
    public string state; //possible 'initial', 'spilled', 'blownAway'
    public Human human;
    public bool npcTouched;
    public GameObject saltWall;
    // Start is called before the first frame update
    protected override void Start()
    {
        state = "initial";
        saltWall.SetActive(false);
        //npcTouched = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(human.state == "Fridge" && state == "initial")
        {
            this.Interact();
            saltWall.SetActive(true);
            state = "spilled";
            //npcTouched = true;
        }
        if (state == "blown")
        {
            human.state = "Scareable";
        }
    }

    //NPC interact with salt shaker
    /* DEPRECATED, HANDLED VIA STATES NOW
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Npc"))
        {
            Debug.Log("NPC passed collider, interacted with SaltShaker");
            Interact();
        }
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Test");
        }
    }
    */
}
