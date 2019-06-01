using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calls proper player animation when player enters fan blown zone
/// </summary>
public class FanBlown : MonoBehaviour
{
    public PlayerAnimController player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.GetComponent<PlayerAnimController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.SetBool(PlayerAnimController.AnimState.BLOWN_AWAY, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
