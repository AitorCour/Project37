using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    public int scene; //se introduce la scena a la que se quiere ir
    private bool isInsideTrigger = false;
    public bool doorOpened;
    public bool opening = false;
    //public Animator animator;
    //public Image black;
    private AudioSource audioSource;
    private ChangeScene changeSc;
    private InputManager iM;
    //Donde irá
    public float xPos;
    public float zPos;
    public float yPos = 1;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        changeSc = GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!opening && isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
            //Debug.Log("Change Scene");
            audioSource.Play();
            doorOpened = true;
            changeSc.FadeChangeScene(scene);
            changeSc.pX = xPos;
            changeSc.pZ = zPos;
            changeSc.pY = yPos;
            changeSc.SavePosition();
            iM.canPause = false;
            opening = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideTrigger = true;
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
