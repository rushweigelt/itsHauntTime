using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calls proper player animation when player enters fan blown zone
/// </summary>
public class PlayerAnimationTrigger : MonoBehaviour
{
    PlayerAnimController player;

    public PlayerAnimController.AnimState playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.GetComponent<PlayerAnimController>();
    }

    // TODO: create derived classes that handle their own specific animation trigger rules
}
