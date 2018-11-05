using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour 
{
	private CharacterController controller;

	public float speed;

	public float iniSpeed;
	public float rotateSpeed;
	public float transAmount;
	private float rotateAmount;
	public bool godMode;

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
			speed = 10;
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
