using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraAutoDoor : MonoBehaviour
{
    public int scene; //se introduce la scena a la que se quiere ir
    private AudioSource audioSource;
    private ChangeScene changeSc;
    //Donde irá
    public float xPos;
    public float zPos;
    public float yPos = 1;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        changeSc = GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();
            changeSc.FadeChangeScene(scene);
            changeSc.pX = xPos;
            changeSc.pZ = zPos;
            changeSc.pY = yPos;
            changeSc.SavePosition();
        }
    }
}
