﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour 
{
	public int PlayerLife;
	private SoundPlayer sound;

	private int iniLife = 3;
	public bool isDead;
	public int Keys;
	private int iniKeys = 0;
	public int Potions;
	private int iniPotions = 0;
	public int cure = 1;
	private ChangeScene changeScene;
	// Use this for initialization
	void Start () 
	{
		isDead = false;
		PlayerLife = iniLife;
		iniKeys = Keys;
		iniPotions = Potions;
		changeScene =  GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
		sound = GetComponentInChildren<SoundPlayer>();
	}

	//Recibir Daño
	public void Damage(int hit)
	{
		if(isDead) return;

		PlayerLife -= hit;

		if (PlayerLife <= 0)
		{
			PlayerLife = 0;
			Dead();
		}
		sound.Play(1, 2);
	}

	//Curarse
	public void Curation(int cure)
	{
		if(isDead) return;
		if(PlayerLife == iniLife) return;
		if(Potions >= 1)
		{
			PlayerLife += cure;
			Debug.Log("Cured +1");

			if (PlayerLife >= iniLife)
			{
				PlayerLife = iniLife;
			}
			Potions -= 1;
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
			Debug.Log ("isdead");
			changeScene.Death();
		}
	}
	//Sumar Llaves
	public void GetKeys(int key)
	{
		if(isDead) return;
		Keys += key;
		//Debug.Log ("key");
	}
	//Perder Llaves
	public void LoseKeys(int key)
	{
		if(isDead) return;
		Keys -= key;
	}
	//Sumar Pociones
	public void GetPotions(int potion)
	{
		if(isDead) return;
		Potions += potion;
		//Debug.Log ("potion");
	}
	public void GodMode()
	{
		Keys = 999;
		Potions = 999;
		iniLife = 999;
		PlayerLife = 999;
	}
}
