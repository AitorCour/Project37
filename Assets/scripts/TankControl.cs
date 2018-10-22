using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour 
{
	private CharacterController controller;

	private float speed;

	public float iniSpeed;
	public float rotateSpeed;
	private float transAmount;
	private float rotateAmount;

	// Use this for initialization
	void Start () 
	{
		speed = iniSpeed;
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
	}
	//solucionar: la velocidad se multiplica cuando el personaje gira y va hacia delante a la vez
}
