using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bullet; //bullet prefab
    //public GameObject player; //player this shooter is attached to
    public float bulletSpeed; //initial speed of bullet
    public float fireTime; //Cooldown before next fire
    public float damage; //How much damage each bullet does
    //public Animator animator; //animator for shooting
    public Transform shotFrom; //Transform to act as bullet spawn location
    private Rigidbody2D playerRB; //for adding velo to projectile

    private Vector2 lastVelocity = Vector2.right; //Orient right by default
    public Vector2 playerVec; //the last know way our player was looking
    private float timeToFire; //
    public int fireBoostDur; //how long fire rate is boosted for
    float ver; //Left Stick Vertical (Y of movement)
    float hor; //Left Stick Horizontal (X of movement)
    float rsHor; //Right Stick Horizontal (X of rotation)
    float rsVer; //RIght Stick of Vertical (Y of rotation)


    void Start ()
    {
        playerRB = GetComponent<Rigidbody2D>(); //assign our rigid body to this script at start
	}
	
	void Update ()
    {

        hor = Input.GetAxisRaw("LeftStickHorizontal"); //update to assign stick num
        ver = Input.GetAxisRaw("LeftStickVertical");  //update to assign stick num
        rsHor = Input.GetAxisRaw("RightStickHorizontal"); //update to assign stick num
        rsVer = Input.GetAxisRaw("RightStickVertical"); //update to assign stick num
        timeToFire -= Time.deltaTime;
        if (playerRB.velocity.magnitude > 0f)
        {
            lastVelocity = playerRB.velocity.normalized;
        }
        //if we have substantial stick pushing (about half), we fire in that direction
        if ((((-.5f > rsHor) || (.5f < rsHor)) || ((-.5f > rsVer) || (.5f < rsVer))) && timeToFire <= 0f)
        {
            timeToFire = fireTime;
            FireBullet();
        }
    }

    public void FireBullet()
    {
        //assign the players Vector to to a variable so we can shoot in the right direction. Then instantiate and fire
        playerVec = new Vector2(rsHor, rsVer);
        //animator.SetTrigger("shotFired");
        GameObject b = Instantiate(bullet, shotFrom.transform.position, shotFrom.transform.rotation) as GameObject;
        b.GetComponent<Rigidbody2D>().velocity = (playerVec * bulletSpeed); //+ playerRB.velocity.normalized *GetComponentInParent<PlayerMover>().moveSpeed;
    }
    //A routine and coroutine for upgrading fire rate within a certain time limit
    public void UpgradeFireRate(float x)
    {
        StartCoroutine(FireRateBoost(x));
    }

    public IEnumerator FireRateBoost(float x)
    {
        fireTime -= x;
        yield return new WaitForSeconds(fireBoostDur);
        fireTime += x;
        /*FIRE RATE, DEPRICATED
        Store store = GetComponent<Store>();
        store.fireActive = false;
        */
    }
    //a func to upgrade damage
    public void UpgradeDamage(float x)
    {
        damage += x;
    }
}
