using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
	private GameManager gameManager;
	//public Animator shoot2;
    private Gun gun;
	private PlayerBehaviour plBehaviour;
	private EnableDisable enDis;
	//plBehaviour
	public int damage;
	private int cure = 1;

    private MouseCursor mouseCursor;
	private bool isPaused = false;
	private bool isInventoryOpened = false;

	// Use this for initialization
	void Start ()
    {
		//shoot2 = GetComponent<Animator>();
		gameManager = GetComponent<GameManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //lookRotation = playerController.GetComponent<LookRotation>();
        gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		enDis = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();

        mouseCursor = new MouseCursor();
        mouseCursor.HideCursor();

		//eventSystem = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

		if (Input.GetKeyDown(KeyCode.O) && enDis.isPointing && enDis.precisionActive == false) 
		{
			gun.Shot ();
			Debug.Log("Shoot");
			enDis.timeCounter = 0;
		}
		else if(Input.GetKeyDown(KeyCode.O) && enDis.isPointing && enDis.precisionActive) 
		{
			gun.PrecisionShot ();
			Debug.Log("SpecialShoot");
			enDis.timeCounter = 0;
			enDis.precisionActive = false;
		}
        if(Input.GetKeyDown(KeyCode.O) && gun.currentAmmo <= 0) gun.Reload();


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

		//PAUSE
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(!isPaused && !isInventoryOpened)
			{
				gameManager.Pause();
			}
			else if(isInventoryOpened)
			{
				return;
			}
			else
			{
				gameManager.Resume();
			}
		}

		//INVENTORY
		if (Input.GetKeyDown(KeyCode.I))
		{
			if(!isPaused && !isInventoryOpened)
			{
				gameManager.OpenInventory();
				//Debug.Log ("Z pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else
			{
				gameManager.CloseInventory();
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
}
