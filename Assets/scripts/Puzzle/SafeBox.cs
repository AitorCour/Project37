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
	public bool thirdActive = false;
	public bool fourthActive = false;
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
		//Primero
		if(cylinder.eulerAngles.z >= 10 && cylinder.eulerAngles.z <= 15 && Input.GetButton("Action"))
		{
			Debug.Log("Click 1");
			firstActive = true;
		}
		//Segundo
		else if(cylinder.eulerAngles.z >= 15 && cylinder.eulerAngles.z <= 20 && Input.GetButton("Action") && firstActive == true)
		{
			Debug.Log("Click 2");
			secondActive = true;
		}
		//Tercero
		else if(cylinder.eulerAngles.z >= 20 && cylinder.eulerAngles.z <= 25 && Input.GetButton("Action") && secondActive == true)
		{
			Debug.Log("Click 3");
			thirdActive = true;
		}
		//Quarto
		else if(cylinder.eulerAngles.z >= 25 && cylinder.eulerAngles.z <= 30 && Input.GetButton("Action") && thirdActive == true)
		{
			Debug.Log("Click 4");
			fourthActive = true;
			Debug.Log("OPEN");
		}
		//Fallo
		else if(Input.GetButton("Action"))
		{
			Debug.Log("NO");
			firstActive = false;
			secondActive = false;
			thirdActive = false;
			fourthActive = false;
		}
	}
}
