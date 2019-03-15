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
    public bool hasKey1 = false;
    public bool hasKey2 = false;

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		//ammo.text = gun.currentAmmo + " / " + gun.Munition.ToString();
		ammoInv.text = gun.currentAmmo + " / " + gun.Munition.ToString();
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            PickObject(Busto);
            //GetBust();
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            PickObject(Box);
            //GetBall();
        }
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            PickObject(Ball);
            //GetBox();
        }


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSlot(1);
            /*hasBust = false;
            Busto.SetActive(false);*/
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSlot(2);
            /*hasBall = false;
            Ball.SetActive(false);*/
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSlot(3);
            /*hasBox = false;
            Box.SetActive(false);*/
        }
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
                Debug.Log("number is " + i);
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

    public void UseBusto()
    {
        SearchObject("Busto");
        Debug.Log("useBust");
    }

    public void UseKey()
    {
        SearchObject("Key");
        Debug.Log("useBall");
    }

    public void UseBox()
    {
        SearchObject("Box");
        Debug.Log("useBox");
    }
    /*void GetBust()
{
   hasBust = true;
   Busto.SetActive(true);
   if(hasBall && !hasBox)
   {
       Busto.transform.position = slotPos[1].transform.position;
   }
   else if (!hasBall && hasBox)
   {
       Busto.transform.position = slotPos[1].transform.position;
   }
   else if (!hasBall && !hasBox)
   {
       Busto.transform.position = slotPos[0].transform.position;
   }
   else Busto.transform.position = slotPos[2].transform.position;
}

void GetBall()
{
   hasBall = true;
   Ball.SetActive(true);
   if (hasBust && !hasBox)
   {
       Ball.transform.position = slotPos[1].transform.position;
   }
   else if (!hasBust && hasBox)
   {
       Ball.transform.position = slotPos[1].transform.position;
   }
   else if (!hasBust && !hasBox)
   {
       Ball.transform.position = slotPos[0].transform.position;
   }
   else Ball.transform.position = slotPos[2].transform.position;
}
void GetBox()
{
   hasBox = true;
   Box.SetActive(true);
   if (hasBust && !hasBall)
   {
       Box.transform.position = slotPos[1].transform.position;
   }
   else if (!hasBust && hasBall)
   {
       Box.transform.position = slotPos[1].transform.position;
   }
   else if (!hasBust && !hasBall)
   {
       Box.transform.position = slotPos[0].transform.position;
   }
   else Box.transform.position = slotPos[2].transform.position;
}*/
}
