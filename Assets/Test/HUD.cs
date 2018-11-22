using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	private Gun gun;
	private PlayerBehaviour plBehaviour;
	public Text ammo;
	public Text ammoInv;
	public Text bandages;
	public Text keys;
	public Text plLife;
	// Use this for initialization
	void Start () 
	{
		gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ammo.text = gun.currentAmmo + " / " + gun.Munition.ToString();
		bandages.text = "Bandages: " + plBehaviour.Potions.ToString();
		keys.text = "Keys: " + plBehaviour.Keys.ToString();
		plLife.text = "Life: " + plBehaviour.PlayerLife.ToString();
		ammoInv.text = gun.currentAmmo + " / " + gun.Munition.ToString();
	}
}
