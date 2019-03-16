using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour
{
    public GameObject[] useButtons;
    public GameObject busto;
    public GameObject musicBox;
    private bool isInsideTrigger = false;
    public bool bustPlaced;
    private bool boxPlaced;
    private bool ballPlaced;

    private HUD hud;

    // Use this for initialization
    void Start ()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isInsideTrigger)
        {
            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }             
        }
        else if(!isInsideTrigger)
        {
            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(false);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //solo funciona con player
        {
            isInsideTrigger = true; //cambia el bool
                                    //Debug.Log ("Enterd 2");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideTrigger = false;
        }
    }
    public void PlaceBust()
    {
        if (!boxPlaced && !ballPlaced)
        {
            busto.transform.position = gameObject.transform.position;
            bustPlaced = true;
            hud.UseBusto();
        }
        else return;
    }
    public void PlaceMusicBox()
    {
        if (!bustPlaced && !ballPlaced)
        {
            musicBox.transform.position = gameObject.transform.position;
            boxPlaced = true;
            hud.UseBox();
        }
        else return;
    }
    public void PlaceBall()
    {
        if (!bustPlaced && !boxPlaced)
        {
            musicBox.transform.position = gameObject.transform.position;
            ballPlaced = true;
            hud.UseKey();
        }
        else return;
    }
}
