using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ScareTarget
{
    public string name;
    public Animator animator;
    public RuntimeAnimatorController[] anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inRange == true)
            {
                //animator.runtimeAnimatorController(anim[1]);
            }
        }
    }

    void Walk()
    {

    }

    void MakeNoise()
    {

    }

    void setName()
    {
        
    }

    void getName()
    {

    }

    protected new void InitialState()
    {

    }

    protected new void ScaredState()
    {

    }

    protected new void AlertedState()
    {

    }
}
