using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls2 : MonoBehaviour
{
	public float speed;
	public float iniSpeed;
	public float rotSpeed;
	public float runSpeed;
	public bool godMode;
	
	public bool canWalk;

    private Animator animator;
    void Start () 
	{
		speed = iniSpeed;
        animator = GetComponentInChildren<Animator>();
    }

	void Update ()
	{
		if (canWalk)
		{
			//var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
			//var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            if(Input.GetAxis("Horizontal") != 0)
            {
                var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
            }
            if(Input.GetAxis("Vertical") > 0)
            {
                var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
                transform.Translate(0, 0, z);
				animator.SetBool("Walking", true);
				//animator.SetBool("WalkingBack", false);
            }
			else if(Input.GetAxis("Vertical") < 0)
            {
                var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
                transform.Translate(0, 0, z);
				animator.SetBool("WalkingBack", true);
				//animator.SetBool("Walking", false);
            }
			
			//transform.Translate(0, 0, z);
            //animator.SetBool("Walking", true);
            else
            {
                animator.SetBool("Walking", false);
				animator.SetBool("Running", false);
				animator.SetBool("WalkingBack", false);
            }
            //transform.Rotate(0, x, 0);
        }
		if (Input.GetButton("Run") && Input.GetAxis("Vertical") > 0.1)
		{
			//Debug.Log("isRunning");
			speed = runSpeed;
			animator.SetBool("Running", true);
			animator.SetBool("Walking", false);
			animator.SetBool("WalkingBack", false);
		}
		if (Input.GetButton("Run") && Input.GetAxis("Vertical") < -0.1) //hacia atras
		{
		    speed = iniSpeed;
			animator.SetBool("Walking", false);
			animator.SetBool("Running", false);
			animator.SetBool("WalkingBack", true);
		}

		if (Input.GetButtonUp("Run"))
		{
			speed = iniSpeed;
			animator.SetBool("Walking", true);
			animator.SetBool("Running", false);
			//animator.SetBool("WalkingBack", false);
		}

		if(godMode == true)
		{
			if (Input.GetKey("z")) 
			{
				transform.Translate(0, speed * Time.deltaTime, 0);
			}
			if (Input.GetKey("x")) 
			{
				transform.Translate(0, -speed * Time.deltaTime, 0);
			}
		}	
	}
}
