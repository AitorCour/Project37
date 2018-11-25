using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls2 : MonoBehaviour
{
	//private CharacterController controller;
	public float speed;
	public float iniSpeed;
	//public float transAmount;

	public enum RotationAxes {MouseXAndY = 0, MouseX = 1, MouseY = 2}
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15f;
	//public float sensitivityY = 15f;

	public float minimumX = -360f;
	public float maximumX = 360f;

	//public float minimumY = -60f;
	//public float maximumY = 60f;

	float rotationY = 0f;

	public bool godMode;
	
	void Start () 
	{
		speed = iniSpeed;
		//controller = GetComponent<CharacterController>();
	}

	void Update ()
	{
		
		
		transform.Translate(0f, 0f,speed * Input.GetAxis("Vertical") * Time.deltaTime);
		

		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * sensitivityX;

			//rotationY +=  Input.GetAxis("Vertical") * sensitivityY;
			//rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX){
			transform.Rotate(0, Input.GetAxis("Horizontal") * sensitivityX, 0);
		}
		else
		{
			//rotationY += Input.GetAxis("Vertical") * sensitivityY;
			//rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}

		if (Input.GetButton("Run")) //Probar a hacer un bool,para cuando corra y cuando no
		{
			//Debug.Log("isRunning");
			speed = 5;
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
