using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour 
{
	private CharacterController controller;
	//private Vector2 axis;
	//public Vector3 moveDirection;

	public float speed;

	public float iniSpeed;
	public float rotateSpeed;
	public float transAmount;
	private float rotateAmount;
	public bool godMode;

	//public KeyCode god;
	// Use this for initialization
	void Start () 
	{
		speed = iniSpeed;
		controller = GetComponent<CharacterController>();
		godMode = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		 /*Vector3 transformDirection = axis.x * transform.right + axis.y * transform.forward;
		 moveDirection.z = transformDirection.z * speed;
		 controller.Move(moveDirection * Time.deltaTime);
		 transform.Rotate(0, Input.GetAxis("Horizontal")*rotateSpeed, 0);*/
		
		transAmount = speed * Time.deltaTime;
		rotateAmount = rotateSpeed * Time.deltaTime;

		if (Input.GetKey("w")) 
		{
			transform.Translate(0, 0, transAmount);
		}
		if (Input.GetKey("s")) 
		{
			transform.Translate(0, 0, -transAmount);
		}
		if (Input.GetKey("a")) 
		{
			transform.Rotate(0, -rotateAmount, 0);
		}
		if (Input.GetKey("d")) 
		{
			transform.Rotate(0, rotateAmount, 0);
		}
		if (Input.GetKey(KeyCode.LeftShift)) //Probar a hacer un bool,para cuando corra y cuando no
		{
			//Debug.Log("isRunning");
			speed = 5;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = iniSpeed;
		}
		if(godMode == true)
		{
			if (Input.GetKey("z")) 
			{
				transform.Translate(0, transAmount, 0);
			}
			if (Input.GetKey("x")) 
			{
				transform.Translate(0, -transAmount, 0);
			}
		}
	}
}
