using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointState : MonoBehaviour 
{
	/*private CharacterController controller;
	public float rotateSpeed;
	private float rotateAmount;*/
	public Quaternion originalRot;
	float rotationSpeed = 1.0f;
	// Use this for initialization
	void Start () 
	{
		originalRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//rotateAmount = rotateSpeed * Time.deltaTime;

		if (Input.GetKeyDown("w")) //Mirar de hacer que no sea tan brusco
		{
			transform.Rotate(30, 0, 0);
		}
		if (Input.GetKeyUp("w")) 
		{
			transform.Rotate(-30, 0, 0);
		}
		if (Input.GetKeyDown("s")) 
		{
			transform.Rotate(-30, 0, 0);
		}
		if (Input.GetKeyUp("s")) 
		{
			transform.Rotate(30, 0, 0);
		}

		if (Input.GetKeyDown("f"))
		{
			//transform.rotation = Quaternion.Slerp(transform.rotation, originalRot, Time.time*rotationSpeed);
		}
	}
}
