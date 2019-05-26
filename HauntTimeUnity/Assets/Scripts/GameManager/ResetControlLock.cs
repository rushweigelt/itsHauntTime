using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetControlLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalManager.Instance.controlLockOn)
        {
            GlobalManager.Instance.controlLockOn = true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
