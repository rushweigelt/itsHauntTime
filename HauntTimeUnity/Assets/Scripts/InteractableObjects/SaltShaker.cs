using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaltShaker : SpriteSwapInteraction
{

    /// <summary>
    /// Event called when cat jumps
    /// </summary>
    public UnityEvent CatJump;

    public GameObject saltWall;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        saltWall.SetActive(false);
        CatJump.AddListener(() => Spill());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void Spill()
    {
        base.Interact();
        saltWall.SetActive(true);

    }

    public void BlowAway()
    {
        saltWall.SetActive(false);
    }
}
