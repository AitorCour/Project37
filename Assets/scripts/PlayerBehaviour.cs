using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour 
{
	public int playerLife;
	private SoundPlayer sound;

	HUD hud;

	private int iniLife = 3;
	public bool isDead;
	public int keys;
	private int iniKeys = 0;
	public int potions;
	private int iniPotions = 0;
	public int cure = 1;
	private ChangeScene changeScene;
	// Use this for initialization
	void Start () 
	{
		hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

		isDead = false;
		playerLife = iniLife;
		hud.SetLife(playerLife);
		hud.SetBandages(potions);
		hud.SetKeys(keys);
		iniKeys = keys;
		iniPotions = potions;
		changeScene =  GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
		sound = GetComponentInChildren<SoundPlayer>();

	}

	//Recibir Daño
	public void Damage(int hit)
	{
		if(isDead) return;

		playerLife -= hit;

		if (playerLife <= 0)
		{
			playerLife = 0;
			Dead();
		}
		hud.SetLife(playerLife);
		sound.Play(1, 2);
	}

	//Curarse
	public void Curation(int cure)
	{	
		if(isDead) return;
		if(playerLife == iniLife) return;
		if(potions >= 1)
		{
			playerLife += cure;
			//Debug.Log("Cured +1");

			if (playerLife >= iniLife)
			{
				playerLife = iniLife;
			}
			potions -= 1;
			hud.SetLife(playerLife);
			hud.SetBandages(potions);
		}
		else
		{
			//Debug.Log("GetPotions");
		}
	}
	//Morir
	private void Dead()
	{
		isDead = true;
		if (isDead == true)
		{
			//Debug.Log ("isdead");
			changeScene.Death();
		}
	}
	//Sumar Llaves
	public void GetKeys(int key)
	{
		if(isDead) return;
		keys += key;
		//Debug.Log ("key");
		hud.SetKeys(keys);
	}
	//Perder Llaves
	public void LoseKeys(int key)
	{
		if(isDead) return;
		keys -= key;
		hud.SetKeys(keys);
	}
	//Sumar Pociones
	public void GetPotions(int potion)
	{
		if(isDead) return;
		potions += potion;
		//Debug.Log ("potion");
		hud.SetBandages(potions);
	}
	public void GodMode()
	{
		keys = 999;
		potions = 999;
		iniLife = 999;
		playerLife = 999;
		hud.SetLife(playerLife);
		hud.SetBandages(potions);
		hud.SetKeys(keys);
	}
}
