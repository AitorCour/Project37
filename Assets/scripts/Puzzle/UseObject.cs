using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour
{
    public GameObject[] useButtons;
    public bool bustPlaced;
    public bool boxPlaced;
    public bool ballPlaced;

    private HUD hud;

    public Transform positions;
    private ObjectPos1 oP1;
    private ObjectPos1 oP2;
    private ObjectPos1 oP3;

    private bool pos1;
    private bool pos2;
    private bool pos3;
    private bool pos1free;
    private bool pos2free;
    private bool pos3free;
    public GameObject busto;
    public GameObject box;
    public GameObject ball;

    // Use this for initialization
    void Start ()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        oP1 = GameObject.FindGameObjectWithTag("Position_1").GetComponent<ObjectPos1>();
        oP2 = GameObject.FindGameObjectWithTag("Position_2").GetComponent<ObjectPos1>();
        oP3 = GameObject.FindGameObjectWithTag("Position_3").GetComponent<ObjectPos1>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(oP1.isInsideTrigger)
        {
            positions.transform.position = oP1.transform.position;
            pos1 = true;
            pos2 = false;
            pos3 = false;
            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }
        }
        if (oP2.isInsideTrigger)
        {
            positions.transform.position = oP2.transform.position;
            pos1 = false;
            pos2 = true;
            pos3 = false;
            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }
        }
        if (oP3.isInsideTrigger)
        {
            positions.transform.position = oP3.transform.position;
            pos1 = false;
            pos2 = false;
            pos3 = true;
            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }
        }

    }

    public void PickObject(GameObject pickObj)
    {
        pickObj.transform.position = new Vector3(0, 0, 0);//el obj recogido va al 000
    }


    public void PlaceBust()
    {
        
        if(pos1)
        {
            Debug.Log("position is 1");
            if(!pos1free)
            {
                busto.transform.position = positions.transform.position;
                pos1free = false;
                hud.UseBusto();
            }
            else return;
        }
        if(pos2)
        {
            Debug.Log("position is 2");
            if(!pos2free)
            {
            busto.transform.position = positions.transform.position;
            pos2free = false;
            hud.UseBusto();
            }
            else return;
        }
        if(pos3)
        {
            Debug.Log("position is 3");
            if(!pos3free)
            {
                busto.transform.position = positions.transform.position;
                pos3free = false;
                hud.UseBusto();
            }
            else return;
        }
    }
    public void PlaceMusicBox()
    {
        if(pos1)
        {
            Debug.Log("position is 1");
            if(!pos1free)
            {
                box.transform.position = positions.transform.position;
                pos1free = false;
                hud.UseBox();
            }
            else return;
        }
        if(pos2)
        {
            Debug.Log("position is 2");
            if(!pos2free)
            {
                box.transform.position = positions.transform.position;
                pos2free = false;
                hud.UseBox();
            }
            else return;
        }
        if(pos3)
        {
            Debug.Log("position is 3");
            if(!pos3free)
            {
                box.transform.position = positions.transform.position;
                pos3free = false;
                hud.UseBox();
            }
            else return;
        }
    }
    public void PlaceBall()
    {
        if(pos1)
        {
            Debug.Log("position is 1");
            if(!pos1free)
            {
                ball.transform.position = positions.transform.position;
                pos1free = false;
                hud.UseKey();
            }
            else return;
        }
        if(pos2)
        {
            Debug.Log("position is 2");
            if(!pos2free)
            {
                ball.transform.position = positions.transform.position;
                pos2free = false;
                hud.UseKey();
            }
            else return;
        }
        if(pos3)
        {
            Debug.Log("position is 3");
            if(!pos3free)
            {
                ball.transform.position = positions.transform.position;
                pos3free = false;
                hud.UseKey();
            }
            else return;
        }
    }
    public void PickBusto()
    {
        PickObject(busto);
    }
    public void PickBox()
    {
        PickObject(box);
    }
    public void PickBall()
    {
        PickObject(ball);
    }
}
