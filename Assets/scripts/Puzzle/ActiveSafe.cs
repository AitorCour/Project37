using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSafe : MonoBehaviour
{
    private bool isInsideTrigger;
    public bool puzzleActive;
    private TankControls2 tank;
    public GameObject safeBox;
    public GameObject cameraSafe;
    public GameObject cameraOther;
    public Text eText;
    public GameObject textPanel = null;
    public string message = "Hello World";
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
            ActiveBox();
        }
        else if(Input.GetButton("Fire") && isInsideTrigger)
        {
            DesactiveBox();
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
    void ActiveBox()
    {
        safeBox.GetComponent<SafeBox>().enabled = true;
        tank.canWalk = false;
        cameraSafe.SetActive(true);
        cameraOther.SetActive(false);
        textPanel.SetActive(true);
        eText.text = message;
    }
    void DesactiveBox()
    {
        safeBox.GetComponent<SafeBox>().enabled = false;
        tank.canWalk = true;
        cameraSafe.SetActive(false);
        cameraOther.SetActive(true);
        textPanel.SetActive(false);
    }
    public void PuzzleEnd()
    {
        safeBox.GetComponent<SafeBox>().enabled = false;
        tank.canWalk = true;
        cameraSafe.SetActive(false);
        cameraOther.SetActive(true);
        textPanel.SetActive(false);
        puzzleActive = false;
        this.enabled = false;
    }
}
