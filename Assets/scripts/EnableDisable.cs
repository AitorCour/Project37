using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour 
{
	private TankControls2 tankControl2;

	//public GameObject ammoText;
	private InputManager inputManager;
	private LookAtEnemy autoAim;
	private bool isTank;
	private bool isDragging;
	public bool isPointing;

	//public float timeCounter;
	//public float precisionTime = 2.0f;
	//public bool precisionActive;
	public bool m_isAxisInUse = false;
	private bool canShoot = true;

	private PlayerBehaviour plBehaviour;

    //Time Counters
	private float timeCounterIN;
	private float inmuneTime = 2.5f;
    private float timeCounterNoShoot;
    private float noShootTime = 0.5f;
    private bool shootState = false;

    private Animator animator;
	// Use this for initialization
	void Start () 
	{
		tankControl2 = GetComponent<TankControls2>();
		inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
		//precisionActive = false;
		autoAim = GetComponentInChildren<LookAtEnemy>();
		autoAim.enabled = false;

		plBehaviour = GetComponent<PlayerBehaviour>();

        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Test
		if(Input.GetButtonDown("Jump") && !inputManager.isPaused && !inputManager.isInventoryOpened && !inputManager.isMapOpened )
		{
			if(!isPointing && canShoot)
			{   
                SetPoint();
				Debug.Log("Mode Changed");
                shootState = true;
			}
			else
			{
				//pointState.ResetGun();
				SetTank();
                shootState = false;
			}
		}
		//Mando
		if(Input.GetAxisRaw("Jump2") != 0)
		{
			if(m_isAxisInUse == false)
			{
				if(!isPointing && canShoot)
				{
					SetPoint();
					Debug.Log("Mode Changed");
					m_isAxisInUse = true;
				}
			}	
		}
		if(Input.GetAxisRaw("Jump2") == 0)
		{
			if(m_isAxisInUse == true)
			{
				SetTank();
				m_isAxisInUse = false;
			}
		}
		else
		{
			return;
		}

		if(plBehaviour.damageRecived)
		{
			//Debug.Log("Start inmune");
			timeCounterIN += Time.deltaTime;
			tankControl2.canWalk = false;
			//inputManager.canShoot = false;
			if(timeCounterIN >= inmuneTime)
			{
				//Debug.Log("End inmune");
				timeCounterIN = 0;
				plBehaviour.damageRecived = false;
				tankControl2.canWalk = true;
				//inputManager.canShoot = true;
			}
		}
        if(shootState)
        {
            timeCounterNoShoot += Time.deltaTime;
            if (timeCounterNoShoot >= noShootTime)
            {
                timeCounterNoShoot = 0;
                isPointing = true;
                shootState = false;
            }
        }

		//IDLEs
		if(plBehaviour.playerLife == 2)
		{
			animator.SetBool("Injured", true);
		}
		else animator.SetBool("Injured", false);
		
		if(plBehaviour.playerLife == 1)
		{
			animator.SetBool("SuperInjured", true);
		}
		else animator.SetBool("SuperInjured", false);
	}

	public void SetTank()
	{
		//dragObjects.enabled = false;
		tankControl2.enabled = true;
		//pointRot.enabled = false;
        tankControl2.pointing = false;

		isPointing = false;
		//ammoText.SetActive(false);

		autoAim.enabled = false;
		animator.SetBool("Pointing", false);
	}

	public void SetPoint()//apuntado
	{
        tankControl2.pointing = true;
        //isPointing = true;
		//ammoText.SetActive(true);

		autoAim.enabled = true;
		animator.SetBool("Pointing", true);
		animator.SetBool("Walking", false);
		animator.SetBool("Running", false);
		animator.SetBool("WalkingBack", false);
	}

	public void EquipGun()
	{
		canShoot = true;
	}
	public void DesequipGun()
	{
		canShoot = false;
	}
}
