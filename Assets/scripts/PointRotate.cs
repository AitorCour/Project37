using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotate : MonoBehaviour 
{
	private CharacterController controller;
	public float rotateSpeed;
	private float rotateAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		rotateAmount = rotateSpeed * Time.deltaTime;
		if (Input.GetKey("a")) 
		{
			transform.Rotate(0, -rotateAmount, 0);
		}
		if (Input.GetKey("d")) 
		{
			transform.Rotate(0, rotateAmount, 0);
		}
	}
}
