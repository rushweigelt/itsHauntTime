using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetControlLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Lock player movement if unlocked
        if (PlayerMoveController.Instance.canMove)
        {
            PlayerMoveController.Instance.canMove = false;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
