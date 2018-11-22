using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
	private GameManager gameManager;
	public GameObject cameraGod;
    private Gun gun;
	private PlayerBehaviour plBehaviour;
	private TankControls2 tankControl;
	private EnableDisable enDis;
	public int damage;
	private int cure = 1;

    private MouseCursor mouseCursor;
	public bool isPaused = false;
	public bool isInventoryOpened = false;
	public bool isMapOpened = false;
	private bool godActive = false;
	// Use this for initialization
	void Start ()
    {
		//shoot2 = GetComponent<Animator>();
		gameManager = GetComponent<GameManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		tankControl = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();
		enDis = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();

        mouseCursor = new MouseCursor();
        mouseCursor.HideCursor();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

		if (Input.GetButtonDown("Fire") && enDis.isPointing && enDis.precisionActive == false) 
		{
			gun.Shot ();
			//Debug.Log("Shoot");
			enDis.timeCounter = 0;
		}
		else if(Input.GetButtonDown("Fire") && enDis.isPointing && enDis.precisionActive) 
		{
			gun.PrecisionShot ();
			//Debug.Log("SpecialShoot");
			enDis.timeCounter = 0;
			enDis.precisionActive = false;
			enDis.NormalColor();
		}
		if((Input.GetAxisRaw("Fire") != 0))
		{
			if(enDis.isPointing && enDis.precisionActive == false)
			{
				gun.Shot ();
				//Debug.Log("Shoot");
				enDis.timeCounter = 0;
			}
			else if(enDis.isPointing && enDis.precisionActive)
			{
				gun.PrecisionShot ();
				//Debug.Log("SpecialShoot");
				enDis.timeCounter = 0;
				enDis.precisionActive = false;
				enDis.NormalColor();
			}
		}
        if(Input.GetButtonDown("Run") /*&& gun.currentAmmo <= 0*/ && enDis.isPointing)
		{
			gun.Reload();
			Debug.Log("reload");
		}




		//            TEST             //
		//QuitarseVida
		if(Input.GetKeyDown(KeyCode.Q)) 
		{
			plBehaviour.Damage(damage);
		}
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			plBehaviour.Curation(cure);
		}
		if(Input.GetKeyDown(KeyCode.F10)) 
		{
			if(!godActive)
			{
				SetGod();
			}
			else
			{
				SetNormal();
			}
		}

		//PAUSE
		if (Input.GetButtonDown("Esc"))
		{
			if(!isPaused && !isInventoryOpened && !isMapOpened)
			{
				gameManager.Pause();
			}
			else if(isInventoryOpened)
			{
				return;
			}
			else if(isMapOpened)
			{
				return;
			}
			else
			{
				gameManager.Resume();
			}
		}

		//INVENTORY
		if (Input.GetButtonDown("Inv"))
		{
			if(!isPaused && !isInventoryOpened && !isMapOpened)
			{
				gameManager.OpenInventory();
				//Debug.Log ("Z pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else if(isMapOpened)
			{
				return;
			}
			else
			{
				gameManager.CloseInventory();
				//mouseCursor.HideCursor();
			}
		}
		//MAP
		if (Input.GetButtonDown("Map"))
		{
			if(!isPaused && !isInventoryOpened && !isMapOpened)
			{
				gameManager.OpenMap();
				Debug.Log ("map pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else if(isInventoryOpened)
			{
				return;
			}
			else
			{
				gameManager.CloseMap();
				//mouseCursor.HideCursor();
			}
		}
    }

	public void SetPause (bool p)
	{
		isPaused = p;
	}

	public void SetInventory (bool i)
	{
		isInventoryOpened = i;
	}

	public void SetMap (bool m)
	{
		isMapOpened = m;
	}
	public void SetGod()
	{
		plBehaviour.GodMode();
		gun.GodMode();
		tankControl.godMode = true;
		playerController.enabled = false;
		cameraGod.SetActive(true);
		godActive = true;
	}
	public void SetNormal()
	{
		tankControl.godMode = false;
		playerController.enabled = true;
		cameraGod.SetActive(false);
		godActive = false;
	}
}
