using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    private PlayerBehaviour player;
    // Use this for initialization
    void Start()
    {
        player = GetComponentInParent<PlayerBehaviour>();
    }

    public void Footstep()
	{
		player.PlayFootstep();
	}
}
