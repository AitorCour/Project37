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
	//public int keys;
	//private int iniKeys = 0;
	public int potions;
    
	private ChangeScene changeScene;

    public bool key1;
    public bool key2;
	public bool lader;
	public bool damageRecived = false;

    private Animator animator;

	[Header("Terrains")]
	public LayerMask layerMask;

	//private TankControls2 tankControl2;
    // Use this for initialization
    void Start () 
	{
		hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        //playerLife = iniLife;
		isDead = false;
		changeScene =  GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();

		sound = GetComponentInChildren<SoundPlayer>();

        animator = GetComponentInChildren<Animator>();
        
        if (Data.IsKeyUnlock(0) == true)
        {
            key1 = true;
            hud.hasKey1 = true;
        }
		else key1 = false;
        if (Data.IsKeyUnlock(1) == true)
        {
            key2 = true;
            hud.hasKey2 = true;
        }
		else key2 = false;
        if (Data.IsLaderUnlock() == true)
        {
            lader = true;
            hud.hasLader = true;
        }
        else lader = false;

        playerLife = Data.GetLife();
        potions = Data.GetBandages();
        hud.SetBandages(potions);
        hud.SetLife(playerLife);
        hud.SetKey();
    }

	//Recibir Daño
	public void Damage(int hit)
	{
		if(isDead) return;
		if (!damageRecived)
		{
			playerLife -= hit;
            Data.SetLife(playerLife);
            animator.SetTrigger("Hit");

            animator.SetBool("Walking", false);
            animator.SetBool("Walking2", false);
            animator.SetBool("Walking3", false);

            animator.SetBool("Running", false);
            animator.SetBool("Running2", false);
            animator.SetBool("WalkingBack", false);

            damageRecived = true;

			if (playerLife <= 0)
			{
				playerLife = 0;
				Dead();
			}
			hud.SetLife(playerLife);
			if(playerLife >= 1)
			{
                sound.Stop();
				sound.Play(0);
			}
		}	
	}

	//Curarse
	public void Curation(int cure)
	{	
		if(isDead) return;
		if(playerLife == iniLife) return;
		if(potions >= 1)
		{
			playerLife += cure;
            Data.SetLife(playerLife);
            //Debug.Log("Cured +1");

            if (playerLife >= iniLife)
			{
				playerLife = iniLife;
			}
			potions -= 1;
            Data.SetBandages(potions);
			hud.SetLife(playerLife);
			hud.SetBandages(potions);
		}
		else
		{
            return;
		}
	}
	//Morir
	private void Dead()
	{
		isDead = true;
		sound.Play(1);
        animator.SetTrigger("Dead");
        if (isDead == true)
		{
			//Debug.Log ("isdead");
			changeScene.Death();
		}
	}
    public void ShootSound()
    {
        sound.Play(2);
    }
	public void NoShootSound()
	{
        sound.Play(3);
	}
	//Sumar Llaves
	public void GetKey1()
	{
		if(isDead) return;
        key1 = true;
        Data.UnlockKey(0);
		//Debug.Log ("key");
        hud.hasKey1 = true;
		hud.SetKey();
	}
    public void GetKey2()
    {
        if (isDead) return;
        key2 = true;
        Data.UnlockKey(1);
        //Debug.Log ("key");
        hud.hasKey2 = true;
        hud.SetKey();
    }
	public void GetLader()
	{
		if (isDead) return;
		lader = true;
		hud.hasLader = true;
		hud.SetKey();
	}
	public void LoseLader()
	{
		if (isDead) return;
		lader = false;
		hud.hasLader = false;
		hud.SetKey();
	}
    //Perder Llaves
    //Sumar Pociones
    public void GetPotions(int potion)
	{
		if(isDead) return;
		potions += potion;
        Data.SetBandages(potions);
		hud.SetBandages(potions);
	}
	public void GodMode()
	{
		potions = 999;
		iniLife = 999;
		playerLife = 999;
		hud.SetLife(playerLife);
		hud.SetBandages(potions);
	}
    public void PlayFootstep()
    {
        //sound.Play(3);
        //Debug.Log("Footstep");
        if(!damageRecived)
        {
            sound.PlayF();
        }
    }
	void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == LayerMask.NameToLayer("Carpet"))
        {
			sound.PlayCarpet();
        }

        if (hit.gameObject.layer == LayerMask.NameToLayer("Wood"))
        {
            sound.PlayWood();
        }

        if (hit.gameObject.layer == LayerMask.NameToLayer("Dirt"))
        {
            sound.PlayDirt();
        }
		if (hit.gameObject.layer == LayerMask.NameToLayer("Hall"))
        {
            sound.PlayHall();
        }
    }
}
