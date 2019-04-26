using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : InteractableObject
{
    public Sprite upright;
    public Sprite fallenOver;
    public SpriteRenderer saltRenderer;
    public bool isUpright;

    // Start is called before the first frame update
    protected override void Start()
    {
        isUpright = true;
        saltRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isUpright == true && inRange == true)
            {
                isUpright = false;
                KnockOver();
            }
            else if (isUpright == false && inRange == true )
            {
                isUpright = true;
                Fix();
            }
        }
    }

    void KnockOver()
    {
        saltRenderer.sprite = fallenOver;
    }

    void Fix()
    {
        saltRenderer.sprite = upright;
    }

}
