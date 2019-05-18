using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapInteraction : InteractableObject
{
    public Sprite initialSprite;
    public Sprite finalSprite;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override void Interact()
    {
        Debug.Log("Called base interact");
        if(spriteRenderer.sprite == initialSprite) {
            spriteRenderer.sprite = finalSprite;
        }
        else if(spriteRenderer.sprite == finalSprite) {
            spriteRenderer.sprite = initialSprite;
        }
    }
}
