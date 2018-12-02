using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	Gun gun;
	//private PlayerBehaviour plBehaviour;
	public Text ammo;
	public Text ammoInv;
	public Text bandages;
	public Text keys;
	public Text plLife;
	// Use this for initialization
	void Start () 
	{
		gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		//plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ammo.text = gun.currentAmmo + " / " + gun.Munition.ToString();
		ammoInv.text = gun.currentAmmo + " / " + gun.Munition.ToString();
	}

	public void SetLife(int newLife)
	{
		plLife.text = "Life: " + newLife.ToString();
	}
	public void SetKeys(int newKey)
	{
		keys.text = "x " + newKey.ToString();
	}
	public void SetBandages(int newBand)
	{
		bandages.text = "x " + newBand.ToString();
	}
}
