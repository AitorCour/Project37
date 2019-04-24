using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBox : MonoBehaviour 
{
	public Transform cylinder;
	private float rotSpeed = 30;
    public GameObject key2;
	//Bools
	public bool firstActive = false;
	public bool secondActive = false;
	public bool thirdActive = false;
	public bool fourthActive = false;
    private SoundObj sound;
	// Use this for initialization
	void Start () 
	{
        sound = GetComponentInChildren<SoundObj>();
        key2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxisRaw("Horizontal") < 0)
        {
            float z = 1 * Time.deltaTime * rotSpeed;
            cylinder.transform.Rotate(0, 0, z);
            //sound.Play(0);
        }
		if(Input.GetAxisRaw("Horizontal") > 0)
        {
            float z = -1 * Time.deltaTime * rotSpeed;
            cylinder.transform.Rotate(0, 0, z);
            //sound.Play(0);
        }
		//Primero
		if(cylinder.eulerAngles.z >= 170 && cylinder.eulerAngles.z <= 190 && Input.GetButton("Action"))
		{
			Debug.Log("Click 1");
			firstActive = true;
		}
		//Segundo
		else if(cylinder.eulerAngles.z >= 30 && cylinder.eulerAngles.z <= 50 && Input.GetButton("Action") && firstActive == true)
		{
			Debug.Log("Click 2");
			secondActive = true;
		}
		//Tercero
		else if(cylinder.eulerAngles.z >= 245 && cylinder.eulerAngles.z <= 265 && Input.GetButton("Action") && secondActive == true && thirdActive == false)
		{
			Debug.Log("Click 3");
			thirdActive = true;
		}
		//Quarto
		else if(cylinder.eulerAngles.z >= 245 && cylinder.eulerAngles.z <= 265 && Input.GetButton("Action") && thirdActive == true)
		{
			Debug.Log("Click 4");
			fourthActive = true;
			Debug.Log("OPEN");
            sound.Play(2);
            key2.SetActive(true);
        }
		//Fallo
		else if(Input.GetButton("Action"))
		{
			Debug.Log("NO");
            sound.Play(1);
			firstActive = false;
			secondActive = false;
			thirdActive = false;
			fourthActive = false;
		}
	}
}
