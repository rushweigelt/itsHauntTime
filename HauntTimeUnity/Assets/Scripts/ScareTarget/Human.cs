using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ScareTarget
{
    public string name;
    //public string State;
    public Animator anim;
    public Fan fan;
    int idleHash = Animator.StringToHash("human_fakeIdle");
    int gettingUpHash = Animator.StringToHash("human_gettingUp");
    public Transform transform;
    public Transform fridgeTrans;
    public float StartTime;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        state = "Initial";
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        StartTime = Time.time;
        duration = 5.0f;

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
        if (fan.isOn != true)
        {
            //Walk(fridgeTrans.position.x, fridgeTrans.position.y);
            Walk(transform.position.x, fridgeTrans.position.x);
        }
    }

    public void Walk(float posX, float posX2)
    {
        float t = (Time.time - this.StartTime) / duration;
        transform.position = new Vector3(Mathf.SmoothStep(posX, posX2, t), 0, 0);
        state = "Fridge";

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

    protected new void Scared()
    {
        if(state == "Scareable")
        {
            Interact();
        }
    }

    protected new void AlertedState()
    {

    }

    protected override void Interact()
    {
        base.Interact();
        state = "Scared";
    }
}
