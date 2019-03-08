using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBox : MonoBehaviour 
{
	public Transform cylinder;
	private float rotSpeed = 20;

	//Bools
	public bool firstActive = false;
	public bool secondActive = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxisRaw("Horizontal") < 0)
        {
            float z = 1 * Time.deltaTime * rotSpeed;
            cylinder.transform.Rotate(0, 0, z);    
        }
		if(Input.GetAxisRaw("Horizontal") > 0)
        {
            float z = -1 * Time.deltaTime * rotSpeed;
            cylinder.transform.Rotate(0, 0, z);
        }

		if(cylinder.eulerAngles.z >= 10 && cylinder.eulerAngles.z <= 15 && Input.GetButton("Action"))
		{
			Debug.Log("Click");
			firstActive = true;
		}
		else if(cylinder.eulerAngles.z >= 15 && cylinder.eulerAngles.z <= 20 && Input.GetButton("Action") && firstActive == true)
		{
			Debug.Log("Click 2");
			secondActive = true;
		}
		else if(Input.GetButton("Action"))
		{
			Debug.Log("NO");
			firstActive = false;
		}
	}
}
