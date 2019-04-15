using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isDefaultState = true;
    public BoxCollider2D interactRange;
    //public GameObject interactRangeGameObj;
    public GameObject interactPrompt;


    // Start is called before the first frame update
    void Start()
    {
        SetToDefaultState();
        interactPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToDefaultState()
    {
        isDefaultState = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Near Interactable Obj");
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Leaving Interactable Obj");
            interactPrompt.SetActive(false);
        }
    }
}
