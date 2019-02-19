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
            if(Input.GetAxis("Vertical") != 0)
            {
                var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
                transform.Translate(0, 0, z);
                animator.SetBool("Walking", true);
            }
			
			//transform.Translate(0, 0, z);
            //animator.SetBool("Walking", true);
            else
            {
                animator.SetBool("Walking", false);
            }
            //transform.Rotate(0, x, 0);
        }
		if (Input.GetButton("Run") && Input.GetAxis("Vertical") > 0)
		{
			//Debug.Log("isRunning");
			speed = runSpeed;
		}
		if (Input.GetButton("Run") && Input.GetAxis("Vertical") < 0)
		{
		    speed = iniSpeed;
		}

		if (Input.GetButtonUp("Run"))
		{
			speed = iniSpeed;
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
