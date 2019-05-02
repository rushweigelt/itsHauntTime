using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ScareTarget
{
    public Animator anim;
    public Fan fan;
    public Transform trans;
    public Transform fridgeTrans;
    public float StartTime;
    public float duration;

    // Start is called before the first frame update
    protected override void Start()
    {
        state = "Initial";
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        StartTime = Time.time;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (fan.isOn != true)
        {
            //Walk(fridgeTrans.position.x, fridgeTrans.position.y);
            Walk(trans.position.x, fridgeTrans.position.x);
        }

    }

    public void Walk(float posX, float posX2)
    {
        float t = (Time.time - StartTime) / duration;
        trans.position = new Vector3(Mathf.SmoothStep(posX, posX2, t), 0, 0);
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

    protected new void AlertedState()
    {

    }

    
}
