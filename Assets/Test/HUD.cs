using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	Gun gun;
	//private PlayerBehaviour plBehaviour;
	//public Text ammo;
	public Text ammoInv;
	public Text bandages;
	public Text plLife;

    public GameObject key1;
    public GameObject key2;
    public GameObject lader;
    public bool hasKey1;
    public bool hasKey2;
    public bool hasLader;

    public GameObject Busto;
    public GameObject Box;
    public GameObject Ball;

    public RectTransform[] slotPos;
    public bool[] slot;
    public GameObject[] objectsInv;

    /*public bool hasBust = false;
    public bool hasBox = false;
    public bool hasBall = false;*/
	// Use this for initialization
	void Start () 
	{
		gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
        //plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        if (Data.IsKeyUnlock(1) == true)
        {
            hasKey1 = true;
            SetKey();
        }
        if (Data.IsKeyUnlock(2) == true)
        {
            hasKey2 = true;
            SetKey();
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		//ammo.text = gun.currentAmmo + " / " + gun.Munition.ToString();
		ammoInv.text = gun.currentAmmo + " / " + gun.Munition.ToString();
	}

	public void SetLife(int newLife)
	{
		plLife.text = "Life: " + newLife.ToString();
	}
	public void SetKey()
	{
        if(hasKey1)
        {
            key1.SetActive(true);
        }
        if(hasKey2)
        {
            key2.SetActive(true);
        }
        if(hasLader)
        {
            lader.SetActive(true);
        }
        if(!hasLader)
        {
            lader.SetActive(false);
        }
	}
	public void SetBandages(int newBand)
	{
		bandages.text = "x " + newBand.ToString();
	}

    public void PickObject(GameObject pickObject)
    {
        for(int i = 0; i < slot.Length; i++)
        {
            if(!slot[i])
            {
                slot[i] = true;
                pickObject.transform.position = slotPos[i].transform.position;
                objectsInv[i] = pickObject;
                objectsInv[i].SetActive(true);
                break;
            }
        }
    }

    public void UseSlot(int i)
    {
        slot[i - 1] = false;
        objectsInv[i - 1].SetActive(false);
        
    }

    private void SearchObject(string tag)
    {
        for(int i = 0; i < objectsInv.Length; i++){
            if(slot[i])
            {
                if(tag == objectsInv[i].tag)
                {
                    objectsInv[i].SetActive(false);
                    slot[i] = false;
                }
            }
        }
    }

    public void PickBusto()
    {
        PickObject(Busto);
    }
    public void PickBox()
    {
        PickObject(Box);
    }
    public void PickBall()
    {
        PickObject(Ball);
    }

    public void UseBusto()
    {
        SearchObject("Busto");
        //Debug.Log("useBust");
    }

    public void UseKey()
    {
        SearchObject("Key");
        //Debug.Log("useBall");
    }

    public void UseBox()
    {
        SearchObject("Box");
        //Debug.Log("useBox");
    }
}
