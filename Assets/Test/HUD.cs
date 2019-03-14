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

    public GameObject Busto1;
    public GameObject Busto2;
    public GameObject Busto3;


    public RectTransform[] slotPos;
    public bool[] slot;
    public GameObject[] objectsInv;
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
            PickObject(Busto1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            PickObject(Busto2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            PickObject(Busto3);
        }


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSlot(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSlot(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSlot(3);
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
                break;
            }
        }
    }
    public void UseSlot(int i)
    {
        slot[i - 1] = false;
        objectsInv[i - 1].SetActive(false);
    }
}
