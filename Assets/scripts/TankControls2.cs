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
    private PlayerBehaviour plBehaviour;

    [Header("Speed Settings")]
    public float walkSpeed_1;
    public float walkSpeed_2;
    public float walkSpeed_3;
    public float runSpeed_1;
    public float runSpeed_2;


    void Start () 
	{
		speed = iniSpeed;
        animator = GetComponentInChildren<Animator>();
		isRunning = false;
        plBehaviour = GetComponent<PlayerBehaviour>();
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
                /*var z = 1 * Time.deltaTime * speed;
                transform.Translate(0, 0, z);*/
				if(!isRunning)
				{
					//animator.SetBool("Walking", true);
                    if(plBehaviour.playerLife == 3)
                    {
                        animator.SetBool("Walking", true);
                        animator.SetBool("Walking2", false);
                        animator.SetBool("Walking3", false);
                        float z = 1 * Time.deltaTime * walkSpeed_1;
                        transform.Translate(0, 0, z);
                    }
                    else if(plBehaviour.playerLife == 2)
                    {
                        animator.SetBool("Walking2", true);
                        animator.SetBool("Walking", false);
                        animator.SetBool("Walking3", false);
                        float z = 1 * Time.deltaTime * walkSpeed_2;
                        transform.Translate(0, 0, z);
                    }
                    else if(plBehaviour.playerLife == 1)
                    {
                        animator.SetBool("Walking3", true);
                        animator.SetBool("Walking", false);
                        animator.SetBool("Walking2", false);
                        float z = 1 * Time.deltaTime * walkSpeed_3;
                        transform.Translate(0, 0, z);
                    }
                    animator.SetBool("Running", false);
                    animator.SetBool("Running2", false);
                }
				else if(isRunning)
				{
                    //animator.SetBool("Running", true);
                    float z = 1 * Time.deltaTime * speed;
                    if (plBehaviour.playerLife == 3)
                    {
                        animator.SetBool("Running", true);
                        animator.SetBool("Running2", false);
                    }
                    else if (plBehaviour.playerLife == 2)
                    {
                        animator.SetBool("Running", false);
                        animator.SetBool("Running2", true);
                    }
                    animator.SetBool("Walking", false);
                    animator.SetBool("Walking2", false);
                    animator.SetBool("Walking3", false);
                }
            }
			else if(Input.GetAxisRaw("Vertical") < 0)
            {
                var z = -1 * Time.deltaTime * speed;
                transform.Translate(0, 0, z);
				animator.SetBool("WalkingBack", true);
				//animator.SetBool("Walking", false);
            }
            else
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Walking2", false);
                animator.SetBool("Walking3", false);

                animator.SetBool("Running", false);
                animator.SetBool("Running2", false);
                animator.SetBool("WalkingBack", false);
            }
            //transform.Rotate(0, x, 0);

			if (Input.GetButton("Run") && Input.GetAxisRaw("Vertical") > 0 && plBehaviour.playerLife >= 2)
			{
                //Debug.Log("isRunning");
                //speed = runSpeed;
			    isRunning = true;
                if(plBehaviour.playerLife == 3)
                {
                    speed = runSpeed_1;
                }
                else if(plBehaviour.playerLife == 2)
                {
                    speed = runSpeed_2;
                }
			}
			if (Input.GetButton("Run") && Input.GetAxisRaw("Vertical") < 0) //hacia atras
			{
		        speed = iniSpeed;
			    animator.SetBool("Walking", false);
                animator.SetBool("Walking2", false);
                animator.SetBool("Walking3", false);
            
                animator.SetBool("Running", false);
			    animator.SetBool("WalkingBack", true);
			}

			if (Input.GetButtonUp("Run"))
			{
			    speed = iniSpeed;
			    isRunning = false;
			}

			/*Fast Turn
            if(Input.GetAxis("Vertical")  < 0 && Input.GetButtonDown("Run"))
            {
                transform.Rotate(0, 180, 0);
                //https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html
            }*/
        }

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
