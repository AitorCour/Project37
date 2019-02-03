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

	void Start () 
	{
		speed = iniSpeed;
	}

	void Update ()
	{
		if (canWalk)
		{
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);
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
