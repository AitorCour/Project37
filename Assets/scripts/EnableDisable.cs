using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour 
{
	private DragObjects dragObjects;
	private TankControl tankControl;
	private PointRotate pointRot;
	private PointState pointState;
	public GameObject ammoText;
	private bool isTank;
	private bool isDragging;
	public bool isPointing;

	public float timeCounter;
	public float precisionTime = 2.0f;
	public bool precisionActive;

	public Material material;
	public Color newColor;

	public bool m_isAxisInUse = false;
	// Use this for initialization
	void Start () 
	{
		dragObjects = GetComponent<DragObjects>();
		tankControl = GetComponent<TankControl>();
		pointRot = GetComponent<PointRotate>();
		pointState = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PointState>();
		pointState.transform.Rotate(0, 0, 0);
		precisionActive = false;
		material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Test
		if(Input.GetButtonDown("Jump"))
		{
			if(!isPointing)
			{
				SetPoint();
				Debug.Log("Mode Changed");
			}
			else
			{
				//pointState.ResetGun();
				SetTank();
				NormalColor();
			}
		}
		//Mando
		if(Input.GetAxisRaw("Jump") != 0)
		{
			if(m_isAxisInUse == false)
			{
				if(!isPointing)
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
		if(Input.GetAxisRaw("Jump") == 0)
		{
			if(m_isAxisInUse == true)
			{
				SetTank();
				NormalColor();
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
		}
	}
	void UpdatePoint()
	{
		if(timeCounter >= precisionTime)
		{
			precisionActive = true;
			//Debug.Log("Special Shoot Ready");
			material.color = newColor;
		}
		else timeCounter += Time.deltaTime;
	}
	public void SetDrag()
	{
		dragObjects.enabled = true;
		tankControl.enabled = false;
		pointRot.enabled = false;
		pointState.enabled = false;
		isPointing = false;
		ammoText.SetActive(false);
	}

	public void SetTank()
	{
		dragObjects.enabled = false;
		tankControl.enabled = true;
		pointRot.enabled = false;
		pointState.enabled = false;
		isPointing = false;
		ammoText.SetActive(false);
	}

	public void SetPoint()//apuntado
	{
		dragObjects.enabled = false;
		tankControl.enabled = false;
		pointRot.enabled = true;
		pointState.enabled = true;
		isPointing = true;
		pointState.transform.Rotate(0, 0, 0);
		ammoText.SetActive(true);
	}
	public void NormalColor()
	{
		material.color = Color.yellow;
	}
}
