using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour
{
    //private NavMeshAgent myNav;
	//private Rigidbody rigidbody;
	private PlayerController plController;

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

	public bool canShoot;

	public bool ini_menu;
	public GameObject menu;

    //private Potion potion;
    //private Munition munition;
    [Header("Misc Conditions")]
    public bool canPause = true;

    private SoundObj sound;

    // Use this for initialization
    void Start ()
    {
		//shoot2 = GetComponent<Animator>();
		gameManager = GetComponent<GameManager>();
        //myNav = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
		//rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
		plController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		tankControl = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();
		enDis = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();
        sound = GameObject.FindGameObjectWithTag("HUD").GetComponent<SoundObj>();


        mouseCursor = new MouseCursor();
        mouseCursor.HideCursor();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(gun.isShooting)
		{
			canShoot = false;
		}
        if (!gun.isShooting && !isInventoryOpened && !isPaused && !isMapOpened)
		{
			canShoot = true;
		}

		if(gun.currentAmmo <= 0)
		{
			canShoot = false;
		}
        if(isInventoryOpened || isPaused || isMapOpened)
        {
            canShoot = false;
        }
	}

	void Update ()
    {
        if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

        //Debug.Log(canShoot);

		if (Input.GetButtonDown("Fire") && enDis.isPointing && canShoot && !isInventoryOpened && !isPaused && !isMapOpened && !plBehaviour.damageRecived) 
		{
			gun.Shot ();
			Debug.Log("Shoot");
			//enDis.timeCounter = 0;
		}
		/*else if(Input.GetButtonDown("Fire") && enDis.isPointing && enDis.precisionActive && canShoot && !isInventoryOpened && !isPaused && !isMapOpened) 
		{
			gun.PrecisionShot ();
			Debug.Log("SpecialShoot");
			enDis.timeCounter = 0;
			//enDis.precisionActive = false;
		}*/
		if((Input.GetAxisRaw("Fire") != 0))
		{
			if(enDis.isPointing && canShoot && !isInventoryOpened && !isPaused && !isMapOpened && !plBehaviour.damageRecived)
			{
				gun.Shot ();
				//Debug.Log("Shoot");
				//enDis.timeCounter = 0;
			}
			/*else if(enDis.isPointing && enDis.precisionActive && canShoot && !isInventoryOpened && !isPaused && !isMapOpened)
			{
				gun.PrecisionShot ();
				//Debug.Log("SpecialShoot");
				enDis.timeCounter = 0;
				//enDis.precisionActive = false;
				sound.Play(1, 2);
			}*/
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
			if(!isPaused && !isInventoryOpened && !isMapOpened && !ini_menu && canPause)
			{
				gameManager.Pause();
			}
			else if(isInventoryOpened)
			{
				return;
			}
			else if(isMapOpened || ini_menu || !canPause)
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
			if(!isPaused && !isInventoryOpened && !isMapOpened && !ini_menu && canPause)
			{
				gameManager.OpenInventory();
                sound.Play(4);
				//Debug.Log ("Z pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else if(isMapOpened || ini_menu || !canPause)
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
			if(!isPaused && !isInventoryOpened && !isMapOpened && !ini_menu && canPause)
			{
				gameManager.OpenMap();
				Debug.Log ("map pressed");
				//mouseCursor.ShowCursor();
			}
			else if(isPaused)
			{
				return;
			}
			else if(isInventoryOpened || ini_menu || !canPause)
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
	/*public void QuitMenu ()
	{
		ini_menu = false;
	}*/
	public void SetGod()
	{
		plBehaviour.GodMode();
		gun.GodMode();
		tankControl.godMode = true;
		//myNav.enabled = false;
		//rigidbody.useGravity = false;
		plController.enabled = false;
		cameraGod.SetActive(true);
		godActive = true;
	}
	public void SetNormal()
	{
		tankControl.godMode = false;
		//myNav.enabled = true;
		//rigidbody.useGravity = true;
		plController.enabled = true;
		cameraGod.SetActive(false);
		godActive = false;
	}
	public void PlayerCanWalk()
	{
		tankControl.canWalk = true;
		ini_menu = false;
	}
}
