using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour 
{/*
	public enum State {Walking, Dragging, Pointing};
	public State state;

	private CharacterController controller;

	public float speed;
	public float iniSpeed;
	public float rotateSpeed;
	private float transAmount;
	private float rotateAmount;

	private float dragSpeed;

	public GameObject ammoText;

	void Start () 
	{
		speed = iniSpeed;
	}
	
	void Update () 
	{
		switch(state)
		{
			case State.Walking:
				WalkingUpdate();
				break;
			case State.Dragging:
				DraggingUpdate();
			case State.Pointing:
				PointingUpdate();
				break;
			default:
				break;
		}
	}

	void WalkingUpdate()
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
		if (Input.GetKey(KeyCode.LeftShift))
		{
			//Debug.Log("isRunning");
			speed = 10;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = iniSpeed;
		}
	}

	void DraggingUpdate()
	{
		transAmount = dragSpeed * Time.deltaTime;

		if (Input.GetKey("w")) 
		{
			transform.Translate(0, 0, transAmount);
		}
		if (Input.GetKey("s")) 
		{
			transform.Translate(0, 0, -transAmount);
		}
	}

	void PointingUpdate()
	{
		ammoText.SetActive(true);

		rotateAmount = rotateSpeed * Time.deltaTime;
		if (Input.GetKey("a")) 
		{
			transform.Rotate(0, -rotateAmount, 0);
		}
		if (Input.GetKey("d")) 
		{
			transform.Rotate(0, rotateAmount, 0);
		}
	}*/
}
