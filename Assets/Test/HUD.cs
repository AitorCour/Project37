using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	//Gun gun;
	//private PlayerBehaviour plBehaviour;
	//public Text ammo;
	public Text ammoInv;
	public Text bandages;

    public GameObject key1;
    public GameObject key2;
    public GameObject lader;
    public GameObject note_1;
    public GameObject note_2;
    public GameObject noteFrag_1;
    public GameObject noteFrag_2;
    public bool hasKey1;
    public bool hasKey2;
    public bool hasLader;
    public bool hasFragNote_1;
    public bool hasFragNote_2;
    public bool hasNote_2;

    //
    public GameObject Busto;
    public GameObject Box;
    public GameObject Ball;

    public RectTransform[] slotPos;
    public bool[] slot;
    public GameObject[] objectsInv;
    public Sprite[] sprites;
    public Image image;
	void Start () 
	{
		if(Data.GetNoteFrag_1() == true)
        {
            hasFragNote_1 = true;
            SetKey();
        }
        else if(Data.GetNoteFrag_2() == true)
        {
            hasFragNote_2 = true;
            SetKey();
        }
        else if(Data.GetNote_2() == true)
        {
            hasNote_2 = true;
            SetKey();
        }
    }
    public void SetAmmo(int cA, int mun)
    {
        ammoInv.text = cA + " / " + mun.ToString();
    }
	public void SetLife(int newLife)
	{
        if (newLife == 3) image.sprite = sprites[0];
        if (newLife == 2) image.sprite = sprites[1];
        if (newLife == 1) image.sprite = sprites[2];
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
        if(hasFragNote_1 || hasFragNote_2)
        {
            note_1.SetActive(true);//Activa el obj vacio
            if (hasFragNote_1) noteFrag_1.SetActive(true);//activa uno de los frag
            if (hasFragNote_2) noteFrag_2.SetActive(true);
        }
        if(hasNote_2)
        {
            note_2.SetActive(true);
        }
	}
	public void SetBandages(int newBand)
	{
		bandages.text = "x " + newBand.ToString();
	}

    //
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
