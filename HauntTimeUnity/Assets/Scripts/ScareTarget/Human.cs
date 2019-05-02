using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ScareTarget
{
    public Animator anim;
    public Fan fan;
    public Transform transform;
    public Transform fridgeTrans;
    public float StartTime;
    public float duration;

    // Start is called before the first frame update
    protected override void Start()
    {
        state = "Initial";
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        StartTime = Time.time;
        duration = 5.0f;

    }

    // Update is called once per frame
    protected override void Update()
    {
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

    public void Scare()
    {
        Interact();
    }

    protected new void AlertedState()
    {

    }

    
}
