using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMover : MonoBehaviour
{
    public float speed; //a speed multiplier
    public Rigidbody2D rb; //rigidbody for movement
    public float slowDown; //the amount we slowdown when the stick isnt pushed
    public float rotationSpeed; //the speed with which we rotate to stick input
    float ver; //left stick vertical (movement Y)
    float hor; //left stick horizontal (movement X)
    float rsHor; //right stick horizontal (rotation X)
    float rsVer; //right stick vertical (rotation Y)
    public float speedBoostDur; //the time which we boost speed
    public bool inverse; //inverse boolean
    public float inverseDur; //the duration of inverse attack
	// Use this for initialization
	void Start ()
    {
        //initialize inverse to false, assign rigidbody
        inverse = false;
        rb = GetComponent<Rigidbody2D>();
        hor = Input.GetAxisRaw("LeftStickHorizontal");
        ver = Input.GetAxisRaw("LeftStickVertical");
        rsHor = Input.GetAxisRaw("RightStickHorizontal");
        rsVer = Input.GetAxisRaw("RightStickVertical");
	}
	// Update is called once per frame
	void Update ()
    {
        //update our stick input vars
        hor = Input.GetAxisRaw("LeftStickHorizontal");
        ver = Input.GetAxisRaw("LeftStickVertical");
        rsHor = Input.GetAxisRaw("RightStickHorizontal");
        rsVer = Input.GetAxisRaw("RightStickVertical");
    }

    private void FixedUpdate()
    {
        //movement updates
        //if we're not inversed,
        if (inverse == false)
        {
            //if we have no stick input, slow to a stop
            if (hor != 0 && ver != 0)
            {
                rb.velocity = new Vector2((hor * speed) * slowDown, (ver * speed) * slowDown);
            }
            //if we have stick input, multiply by speed var
            else
            {
                rb.velocity = new Vector2((hor * speed), (ver * speed));
            }
            //looking updates
            float angle = Mathf.Atan2(rsVer, rsHor) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
        //inversed controls, same as above but values are negated
        else
        {
            if (hor != 0 && ver != 0)
            {
                rb.velocity = new Vector2((-hor * speed) * slowDown, (-ver * speed) * slowDown);
            }
            else
            {
                rb.velocity = new Vector2((-hor * speed), (-ver * speed));
            }
            //looking updates
            float angle = Mathf.Atan2(rsVer, rsHor) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
    }
    //speed upgrade coroutine
    public void UpgradeMoveSpeed(float x)
    {
        StartCoroutine(SpeedBoost(x));
    }
    public IEnumerator SpeedBoost(float x)
    {
        speed += x;
        yield return new WaitForSeconds(speedBoostDur);
        speed -= x;
        /* CHANGE TO IMPLEMENT SPEEDBOOST
        Store store = GetComponent<Store>();
        store.boostActive = false;
        Store2 enStore = enemy.GetComponent<Store2>();
        enStore.slowActive = false;
        */
    }
    //inverse control coroutine
    public void InverseControls()
    {
        StartCoroutine(Inverse());
    }

    public IEnumerator Inverse()
    {
        inverse = true;
        yield return new WaitForSeconds(inverseDur);
        inverse = false;
        /*FIX TO ADD INVERSE FUNCTION
        Store2 enStore = enemy.GetComponent<Store2>();
        enStore.inverseActive = false;
        */
    }
}
