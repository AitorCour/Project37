using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour 
{
private CharacterController controller;

	public float speed;

	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0f, 0f,speed * Input.GetAxis("Vertical") * Time.deltaTime);
	}
}
