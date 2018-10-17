using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDesactiveObject : MonoBehaviour 
{
	public GameObject gun;

	private bool HaveGun = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (HaveGun)
		{
			SelectGun();
		}

		else DeselectGun();*/
	}

	void SelectGun()
	{
		gun.SetActive(true);
		Debug.Log("Gun Selected");
		HaveGun = true;
	}

	void DeselectGun()
	{
		gun.SetActive(false);
		Debug.Log("Gun Deselected");
		HaveGun = false;
	}
}
