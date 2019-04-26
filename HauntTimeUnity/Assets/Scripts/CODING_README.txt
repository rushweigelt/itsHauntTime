Our most basic three abstract classes, in my opinion, should be InteractableObject, ScareTarget, GhostDetector (with GhostDetector being a total stretch rn).

Section I. InteractableObjects

InteractableObjects must have at least the following three attributes, thus we mandate it by inhereting these traits onto every interactable obj in our game:
1) BoxCollider bc --needed to detect when player is in range of item
2) GameObject interactPrompt -- the GameObject that indicates we have an interactable object. 
3) Bool inRange -- a simple bool we use to ensure the player is in range of this item

Functions--simply toogles our bool and activates/deactivates the prompt 
void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
            interactPrompt.SetActive(true);
        }
    }
    //if the player leaves the range, disable thought bubble
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = false;
            interactPrompt.SetActive(false);
        }
    }

So to make your own class, you would simply do something like this: 
By making it inherent from InteractableObject, you don't need to recode the above--meaning the bool, collider, and obj are automatically included.
public class SaltShaker : InteractableObject
{
    public Sprite upright;
    public Sprite fallenOver;
    public SpriteRenderer saltRenderer;
    public bool isUpright;

    // Start is called before the first frame update
    protected override void Start()
    {
        isUpright = true;
        saltRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isUpright == true && inRange == true)
            {
                isUpright = false;
                KnockOver();
            }
            else if (isUpright == false && inRange == true )
            {
                isUpright = true;
                Fix();
            }
        }
    }

    void KnockOver()
    {
        saltRenderer.sprite = fallenOver;
    }

    void Fix()
    {
        saltRenderer.sprite = upright;
    }

}


Section II. ScareTarget
ScareTargets must all have the following attributes:
1) GameObject scareIcon -- a prompt that pops up over the user's head to indicate a scare is possible.
2) string ScareState -- a string that indicates the target's current level of timidity (default, alert, scareable!)
3) BoxCollider bc -- a box collider to detect range re: the player.
4) bool inRange -- a bool we can flip when in range, thus allowing for target-specific keystroke actions.

Functions:
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