using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBox : MonoBehaviour 
{
	public Transform cylinder;
	private float rotSpeed = 60;
    public GameObject key2;
	//Bools
	public bool firstActive = false;
	public bool secondActive = false;
	public bool thirdActive = false;
	public bool fourthActive = false;
    private SoundObj sound;
    private ActiveSafe acSafe;
    //private ParticleSystem pS;
	// Use this for initialization
	void Start () 
	{
        sound = GetComponentInChildren<SoundObj>();
        key2.SetActive(false);
        acSafe = GetComponentInChildren<ActiveSafe>();
        //pS = GetComponentInChildren<ParticleSystem>();
        //pS.Stop();
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
		if(cylinder.eulerAngles.z >= 170 && cylinder.eulerAngles.z <= 190 && Input.GetButtonDown("Action") && !firstActive )
		{
			Debug.Log("Click 1");
            sound.Play(this.gameObject, 0);
            firstActive = true;
		}
		//Segundo
		else if(cylinder.eulerAngles.z >= 30 && cylinder.eulerAngles.z <= 50 && Input.GetButtonDown("Action") && firstActive == true && !secondActive)
		{
			Debug.Log("Click 2");
            sound.Play(this.gameObject, 0);
            secondActive = true;
		}
		//Tercero
		else if(cylinder.eulerAngles.z >= 245 && cylinder.eulerAngles.z <= 265 && Input.GetButtonDown("Action") && secondActive == true && thirdActive == false)
		{
			Debug.Log("Click 3");
            sound.Play(this.gameObject, 0);
            thirdActive = true;
		}
		//Quarto
		else if(cylinder.eulerAngles.z >= 245 && cylinder.eulerAngles.z <= 265 && Input.GetButtonDown("Action") && thirdActive == true)
		{
			Debug.Log("Click 4");
			fourthActive = true;
			Debug.Log("OPEN");
            sound.Play(this.gameObject, 2);
            key2.SetActive(true);
            acSafe.PuzzleEnd();
        }
		//Fallo
		else if(Input.GetButtonDown("Action"))
		{
			Debug.Log("NO");
            sound.Play(this.gameObject, 1);
			firstActive = false;
			secondActive = false;
			thirdActive = false;
			fourthActive = false;
		}
	}
}
