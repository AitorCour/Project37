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
	
	private bool isRunning;
	public bool canWalk;
    public bool pointing;

    private Animator animator;
    void Start () 
	{
		speed = iniSpeed;
        animator = GetComponentInChildren<Animator>();
		isRunning = false;
    }

	void Update ()
	{
		if (canWalk == true && !pointing)
		{
			//var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
			//var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                var x = 1 * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
            }
			if(Input.GetAxisRaw("Horizontal") < 0)
            {
                var x = -1 * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
                //hacer una condicion para saber si solo gira en el sitio, sin movimiento. desde allí podría ir al run directamente
            }
            if(Input.GetAxisRaw("Vertical") > 0)
            {
                var z = 1 * Time.deltaTime * speed;
                transform.Translate(0, 0, z);
				if(!isRunning)
				{
					animator.SetBool("Walking", true);
                    animator.SetBool("Running", false);
                }
				else if(isRunning)
				{
					animator.SetBool("Running", true);
                    animator.SetBool("Walking", false);
                }
				
				//animator.SetBool("WalkingBack", false);
            }
			else if(Input.GetAxisRaw("Vertical") < 0)
            {
                var z = -1 * Time.deltaTime * speed;
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

			if (Input.GetButton("Run") && Input.GetAxisRaw("Vertical") > 0)
			{
			//Debug.Log("isRunning");
			speed = runSpeed;
			isRunning = true;
			/*animator.SetBool("Running", true);
			animator.SetBool("Walking", false);
			animator.SetBool("WalkingBack", false);*/
			}
			if (Input.GetButton("Run") && Input.GetAxisRaw("Vertical") < 0) //hacia atras
			{
		    speed = iniSpeed;
			animator.SetBool("Walking", false);
			animator.SetBool("Running", false);
			animator.SetBool("WalkingBack", true);
			}

			if (Input.GetButtonUp("Run"))
			{
			speed = iniSpeed;
			isRunning = false;
			//animator.SetBool("Walking", true);
			//animator.SetBool("Running", false);
			//animator.SetBool("WalkingBack", false);
			}

			/*Fast Turn
            if(Input.GetAxis("Vertical")  < 0 && Input.GetButtonDown("Run"))
            {
                transform.Rotate(0, 180, 0);
                //https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html
            }*/
        }
		/*else if (canWalk == false)
		{
			//speed = 0;
			//Debug.Log("Cant Walk");
			//return;
		}*/

        if (pointing && canWalk)
        {
            //Debug.Log("Pointing");
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                var x = 1 * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                var x = -1 * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
            }
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
            speed = 5;
		}	
	}
}
