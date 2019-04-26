using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ScareTarget
{
    public string name;
    public Animator anim;
    int idleHash = Animator.StringToHash("human_fakeIdle");
    int gettingUpHash = Animator.StringToHash("human_gettingUp");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (inRange == true)
            {
                anim.SetTrigger(gettingUpHash);
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
