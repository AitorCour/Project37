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

            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }
        }
        if (oP2.isInsideTrigger)
        {
            positions.transform.position = oP2.transform.position;

            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }
        }
        if (oP3.isInsideTrigger)
        {
            positions.transform.position = oP3.transform.position;

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
        if (!boxPlaced && !ballPlaced)
        {
            busto.transform.position = positions.transform.position;
            bustPlaced = true;
            hud.UseBusto();
        }
        else return;
    }
    public void PlaceMusicBox()
    {
        if (!bustPlaced && !ballPlaced)
        {
            boxPlaced = true;
            hud.UseBox();
        }
        else return;
    }
    public void PlaceBall()
    {
        if (!bustPlaced && !boxPlaced)
        {
            ballPlaced = true;
            hud.UseKey();
        }
        else return;
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
