﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour 
{
	private TankControls2 tankControl2;
	private LookAtEnemy autoAim;
	public bool isPointing;
    private Animator animator;
	// Use this for initialization
	void Start () 
	{
		tankControl2 = GetComponent<TankControls2>();
		autoAim = GetComponentInChildren<LookAtEnemy>();
		autoAim.enabled = false;
        animator = GetComponentInChildren<Animator>();
	}

    public void SetTank()
	{
		//dragObjects.enabled = false;
		tankControl2.enabled = true;
		//pointRot.enabled = false;
        tankControl2.pointing = false;

		isPointing = false;
		//ammoText.SetActive(false);

		autoAim.enabled = false;
		animator.SetBool("Pointing", false);
	}
	public void SetPoint()//apuntado
	{
        if(!isPointing)
        {
            tankControl2.pointing = true;
            autoAim.enabled = true;
            isPointing = true;
            animator.SetBool("Pointing", true);
            animator.SetBool("Walking", false);
            animator.SetBool("Walking2", false);
            animator.SetBool("Walking3", false);
            animator.SetBool("Running", false);
            animator.SetBool("Running2", false);
            animator.SetBool("WalkingBack", false);
            animator.SetBool("Back2", false);
            animator.SetBool("Back3", false);
        }
        else
        {
            SetTank();
        }
	}
}
