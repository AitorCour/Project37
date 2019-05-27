using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorBlocked : MonoBehaviour
{
    public GameObject TextPanel = null;
    public int scene; //se introduce la scena a la que se quiere ir
    private bool isInsideTrigger = false;
    public bool isDoorOpen;
    private bool MessageReaded = false;
    public string message = "Hello World";
    public Text eText;

    //public Animator animator;
    //public Image black;
    private SoundObj soundObj;
    private ChangeScene changeSc;
    //Donde irá
    public float xPos;
    public float zPos;
    public float yPos = 1;
    void Start()
    {
        soundObj = GetComponentInChildren<SoundObj>();
        changeSc = GameObject.FindGameObjectWithTag("Manager").GetComponent<ChangeScene>();
    }
    void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action"))
        {
            if(!MessageReaded && !isDoorOpen)
            {
                Read();
            }
            else if (isDoorOpen) // utilizar esto para si vuelve a la habitación o lo que sea, que la puerta ya esté abierta
            {
                //SceneManager.LoadScene(scene);
                soundObj.Play(this.gameObject, 2);
                //StartCoroutine(Fade());
                changeSc.FadeChangeScene(scene);
                changeSc.pX = xPos;
                changeSc.pZ = zPos;
                changeSc.pY = yPos;
                changeSc.SavePosition();
            }
            else if (MessageReaded)
            {
                ReadEnd();
            }
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
    private void Read()
    {
        TextPanel.SetActive(true);
        Debug.Log("reading");
        MessageReaded = true;
        Time.timeScale = 0;
        eText.text = message;
        soundObj.Play(this.gameObject, 0);
    }
    private void ReadEnd()
    {
        TextPanel.SetActive(false);
        Debug.Log("quit");
        MessageReaded = false;
        Time.timeScale = 1;
    }
}
