using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HissComponent : MonoBehaviour
{
    BoxCollider2D hissRange;

    public Cat cat;

    public void Start()
    {
        hissRange = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            cat.SetHiss(true);

            // Play hiss sound effect
            SoundController.Instance.PlaySoundEffect(SoundController.SoundType.CAT_HISS);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            cat.SetHiss(false);
        }
    }
}
