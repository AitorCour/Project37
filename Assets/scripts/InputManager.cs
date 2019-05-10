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
	private bool canShoot;
	public bool ini_menu;
    public bool canPause = true;
    private bool m_isAxisInUse;
    private bool shootState;
    public GameObject menu;
    private SoundObj sound;

    private float timeCounterNoShoot;
    private float noShootTime = 0.5f;
    private float timeCounterIN;
    private float inmuneTime = 2.5f;

	public string FolderName = "/Levels";
    public GameData gameData;
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
	void Update ()
    {
        if(Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
		
        else if(Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

        //Debug.Log(canShoot);

		if (Input.GetButtonDown("Fire") && enDis.isPointing && canShoot && !isInventoryOpened && !isPaused && !isMapOpened && !plBehaviour.damageRecived) 
		{
			gun.Shot ();
			//Debug.Log("Shoot");
			//enDis.timeCounter = 0;
		}
		if((Input.GetAxisRaw("Fire") != 0))
		{
			if(enDis.isPointing && canShoot && !isInventoryOpened && !isPaused && !isMapOpened && !plBehaviour.damageRecived)
			{
				gun.Shot ();
				//Debug.Log("Shoot");
				//enDis.timeCounter = 0;
			}
		}
        if(Input.GetButtonDown("Reload") /*&& gun.currentAmmo <= 0*/ && enDis.isPointing)
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
		if(Input.GetKey(KeyCode.AltGr))
        {
            if (Input.GetKeyDown(KeyCode.D)) 
			{
				Data.DeleteFolder();
				Debug.Log("Click to Delete");
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
        //Enable Disable
        if (Input.GetButtonDown("Jump") && !isPaused && !isInventoryOpened && !isMapOpened)
        {
            enDis.SetPoint();
            shootState = true;
            canShoot = false;
            timeCounterNoShoot = 0;
        }
        //Mando
        if (Input.GetAxis("Jump2") == 1 && m_isAxisInUse == false && !isPaused && !isInventoryOpened && !isMapOpened)
        {
            enDis.SetPoint();
            m_isAxisInUse = true;
            shootState = true;
            canShoot = false;
            timeCounterNoShoot = 0;
        }
        else if (Input.GetAxis("Jump2") == 0 && m_isAxisInUse == true && !isPaused && !isInventoryOpened && !isMapOpened)
        {
            enDis.SetTank();
            m_isAxisInUse = false;
        }

        if(shootState)
        {
            timeCounterNoShoot += Time.deltaTime;
            if (timeCounterNoShoot >= noShootTime)
            {
                timeCounterNoShoot = 0;
                canShoot = true;
                shootState = false;
            }
        }
        //Estado Daño
        if (plBehaviour.damageRecived)
        {
            //Debug.Log("Start inmune");
            timeCounterIN += Time.deltaTime;
            tankControl.canWalk = false;
            //inputManager.canShoot = false;
            if (timeCounterIN >= inmuneTime)
            {
                //Debug.Log("End inmune");
                timeCounterIN = 0;
                plBehaviour.damageRecived = false;
                tankControl.canWalk = true;
                //inputManager.canShoot = true;
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
