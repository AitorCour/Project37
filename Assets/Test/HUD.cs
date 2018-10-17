using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	public Gun gun;
	public Text ammo;
	// Use this for initialization
	void Start () 
	{
		gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ammo.text = gun.currentAmmo + " / " + gun.Munition.ToString();
	}
}
