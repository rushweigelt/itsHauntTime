using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaltShaker : SpriteSwapInteraction
{
    /// <summary>
    /// Collider preventing player from passing
    /// </summary>
    public GameObject saltWall;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        saltWall.SetActive(false);
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
