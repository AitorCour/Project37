using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    public int scene; //se introduce la scena a la que se quiere ir
    private bool isInsideTrigger = false;

    //public Animator animator;
    //public Image black;
    private AudioSource audioSource;
    private ChangeScene changeSc;
    //Donde irá
    public float xPos;
    public float zPos;
    public float yPos = 1;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        changeSc = GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action"))
        {
            //Debug.Log("Change Scene");
            audioSource.Play();
            changeSc.FadeChangeScene(scene);
            changeSc.pX = xPos;
            changeSc.pZ = zPos;
            changeSc.pY = yPos;
            changeSc.SavePosition();
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
