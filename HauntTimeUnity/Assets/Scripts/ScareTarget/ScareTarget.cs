using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTarget : MonoBehaviour
{
    public GameObject scareIcon;
    public string scareState;
    public string[] fears;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void initialState()
    {

    }

    protected void scaredState()
    {

    }

    protected void alertedState()
    {

    }

    //if the player nears an interactable object, a thought bubble appears above the players head.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            scareIcon.SetActive(true);
        }
    }
    //if the player leaves the range, disable thought bubble
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            scareIcon.SetActive(false);
        }
    }
}

