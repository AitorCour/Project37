using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour 
{
	private DragObjects dragObjects;
	private TankControls2 tankControl2;
	private PointRotate pointRot;

	public GameObject ammoText;
	private InputManager inputManager;
	private bool isTank;
	private bool isDragging;
	public bool isPointing;

	public float timeCounter;
	public float precisionTime = 2.0f;
	public bool precisionActive;

	public bool m_isAxisInUse = false;

	private SoundPlayer sound;
	private AudioSource sound2;

	public GameObject gun;
	// Use this for initialization
	void Start () 
	{
		dragObjects = GetComponent<DragObjects>();
		tankControl2 = GetComponent<TankControls2>();
		pointRot = GetComponent<PointRotate>();
		inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
		precisionActive = false;

		sound = GetComponentInChildren<SoundPlayer>();
		sound2 = GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Test
		if(Input.GetButtonDown("Jump") && !inputManager.isPaused && !inputManager.isInventoryOpened && !inputManager.isMapOpened )
		{
			if(!isPointing && gun.activeSelf)
			{
				SetPoint();
				Debug.Log("Mode Changed");	
			}
			else
			{
				//pointState.ResetGun();
				SetTank();
			}
		}
		//Mando
		if(Input.GetAxisRaw("Jump2") != 0)
		{
			if(m_isAxisInUse == false)
			{
				if(!isPointing && gun.activeSelf)
				{
					SetPoint();
					Debug.Log("Mode Changed");
					m_isAxisInUse = true;
				}
				/*else
				{
					//pointState.ResetGun();
					SetTank();
					NormalColor();
					m_isAxisInUse = false;
				}*/
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
		if(isPointing)
		{
			UpdatePoint();
		}
		else if(!isPointing)
		{
			timeCounter = 0;
			precisionActive = false;
			if(sound2.isPlaying)
			{
				sound2.Stop();
			}	
		}
		else
		{
			return;
		}
	}
	void UpdatePoint()
	{
		if(timeCounter >= precisionTime)
		{
			precisionActive = true;
			//Debug.Log("Special Shoot Ready");
			if(!sound2.isPlaying)
			{
				sound2.Play();
			}
		}

		else 
		{
			timeCounter += Time.deltaTime;
			if(sound2.isPlaying)
			{
				sound2.Stop();
			}
		}
	}
	public void SetDrag()
	{
		dragObjects.enabled = true;
		tankControl2.enabled = false;
		pointRot.enabled = false;

		isPointing = false;
		ammoText.SetActive(false);
	}

	public void SetTank()
	{
		dragObjects.enabled = false;
		tankControl2.enabled = true;
		pointRot.enabled = false;

		isPointing = false;
		ammoText.SetActive(false);
	}

	public void SetPoint()//apuntado
	{
		dragObjects.enabled = false;
		tankControl2.enabled = false;
		pointRot.enabled = true;
		isPointing = true;
		ammoText.SetActive(true);
	}
}
