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
	private bool isPointing;
	// Use this for initialization
	void Start () 
	{
		dragObjects = GetComponent<DragObjects>();
		tankControl = GetComponent<TankControl>();
		pointRot = GetComponent<PointRotate>();
		pointState = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PointState>();
		pointState.transform.Rotate(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Test
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(!isPointing)
			{
				SetPoint();
				Debug.Log("Mode Changed");
			}
			else
			{
				pointState.transform.Rotate(0, 0, 0);
				SetTank();
			}
		}
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
}
