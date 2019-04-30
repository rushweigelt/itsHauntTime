using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : Transformation
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public override void Apply()
    {
        spriteRenderer.sprite = newSprite;

        // Call onFinished
        onFinished();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
