using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSafe : MonoBehaviour
{
    private bool isInsideTrigger;
    private TankControls2 tank;
    public GameObject safeBox;
    public GameObject cameraSafe;
    public GameObject cameraOther;
    // Use this for initialization
    void Start ()
    {
        tank = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButton("Action") && isInsideTrigger)
        {
            safeBox.GetComponent<SafeBox>().enabled = true;
            tank.canWalk = false;
            cameraSafe.SetActive(true);
            cameraOther.SetActive(false);
        }
        else if(Input.GetButton("Fire") && isInsideTrigger)
        {
            safeBox.GetComponent<SafeBox>().enabled = false;
            tank.canWalk = true;
            cameraSafe.SetActive(false);
            cameraOther.SetActive(true);
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
}
