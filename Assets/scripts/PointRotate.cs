using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotate : MonoBehaviour 
{
	public float rotSpeed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
		transform.Rotate(0, x, 0);
	}
}
