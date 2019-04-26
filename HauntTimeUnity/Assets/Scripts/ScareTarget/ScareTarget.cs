using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTarget : MonoBehaviour
{
    public GameObject scareIcon;
    public string scareState;
    public string[] fears;
    public BoxCollider2D bc;
    public bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    protected void InitialState()
    {

    }

    protected void ScaredState()
    {

    }

    protected void AlertedState()
    {

    }

    //if the player nears an interactable object, a thought bubble appears above the players head.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
            scareIcon.SetActive(true);
        }
    }
    //if the player leaves the range, disable thought bubble
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = false;
            scareIcon.SetActive(false);
        }
    }
}

