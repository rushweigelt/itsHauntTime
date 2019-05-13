using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : InteractableObject
{

    Animator anim;

    public enum AnimationState { SLEEPING, JUMPING, HISSING }

    public AnimationState animationState;

    public SaltShaker salt;

    public GameObject saltOb;

    public BoxCollider2D detectRange;

    public float moveRate;

    /// <summary>
    /// Event called when fan is rattled
    /// </summary>
    public UnityEvent FanRattle;

    /// <summary>
    /// Event called when plugged in
    /// </summary>
    public UnityEvent onPoweredOff;

    //additional box collider for hiss-range
    public BoxCollider2D hissBox;


    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
        anim = GetComponent<Animator>();

        //Listener for rattle

        FanRattle.AddListener(() => Jump());
        onPoweredOff.AddListener(() => Hiss());

        hissBox.enabled = true;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void Hiss()
    {
        Debug.Log("Hiss");
        hissBox.enabled = true;
    }

    public void Jump()
    {
        Vector3 velo = new Vector3(moveRate, 0, 0);
        Debug.Log("Fan was rattled, cat should jump and the hitbox for hissing should be disabled.");
        hissBox.enabled = false;
        while(saltOb.transform.position.x >= transform.position.x)
        {
            transform.position += velo * Time.deltaTime;
        }
        salt.CatJump.Invoke();
    }

    /*
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            animationState = AnimationState.HISSING;
        }
    }
    */
}
