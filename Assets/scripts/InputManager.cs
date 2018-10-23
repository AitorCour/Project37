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
        //Mover al player
        //Vector2 inputAxis = Vector2.zero;
        //inputAxis.x = Input.GetAxis("Horizontal");
        //inputAxis.y = Input.GetAxis("Vertical");
        //playerController.SetAxis(inputAxis);
        //LLamar al salto
        //if(Input.GetButton("Jump")) playerController.StartJump();
        //Camara rotación
        //Vector2 mouseAxis = Vector2.zero;
        //mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
        //mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;

        //lookRotation.SetRotation(mouseAxis);

        if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

		if (Input.GetKeyDown(KeyCode.O) && enDis.isPointing) 
		{
			
			gun.Shot ();
			Debug.Log("Shoot");
		}
        if(Input.GetKeyDown(KeyCode.R)) gun.Reload();


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
