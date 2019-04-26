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
        // isUpright = true;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override void Interact()
    {
        if(spriteRenderer.sprite == initialSprite) {
            spriteRenderer.sprite = finalSprite;
        }
        else if(spriteRenderer.sprite == finalSprite) {
            spriteRenderer.sprite = initialSprite;
        }
    }

    void KnockOver()
    {
        spriteRenderer.sprite = finalSprite;
    }

    void Fix()
    {
        spriteRenderer.sprite = initialSprite;
    }

}
