using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseObject : MonoBehaviour
{
    public GameObject[] useButtons;
    /*public bool bustPlaced;
    public bool boxPlaced;
    public bool ballPlaced;*/

    private HUD hud;

    public Transform positions;
    private ObjectPos1 oP1;
    private ObjectPos1 oP2;
    private ObjectPos1 oP3;

    private bool pos1;
    private bool pos2;
    private bool pos3;
    public bool pos1free;
    public bool pos2free;
    public bool pos3free;
    public GameObject busto;
    public GameObject box;
    public GameObject ball;

    private bool ballCorrect;
    private bool boxCorrect;
    private bool bustCorrect;

    //On complete
    private bool puzzleComplete = false;
    public GameObject barrasCine;//Barras de cinematica
    public GameObject key;//Object to Unlock
    private GameManager gameManager;
    public GameObject TextPanel = null;
    public string message = "Hello World";
    public Text eText;
    private AudioSource sound;
    private TankControls2 tank;
    public float timeCounter;
    private float messageTime = 6f;
    // Use this for initialization
    void Start ()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        oP1 = GameObject.FindGameObjectWithTag("Position_1").GetComponent<ObjectPos1>();
        oP2 = GameObject.FindGameObjectWithTag("Position_2").GetComponent<ObjectPos1>();
        oP3 = GameObject.FindGameObjectWithTag("Position_3").GetComponent<ObjectPos1>();
        sound = GetComponentInChildren<AudioSource>();
        tank = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControls2>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
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
        if(ballCorrect && boxCorrect && bustCorrect && !puzzleComplete)
        {
            UnlockKey();
            timeCounter += Time.deltaTime;
            if (timeCounter >= messageTime)
            {
                //Desactive barras, player canWalk
                PuzzleDone();
                timeCounter = 0;
                puzzleComplete = true;
            }
            //Poner que los obj ya no puedan recojerse
        }
        if(timeCounter >= 2f)
        {
            //Ruido LLave
            sound.Play();
            //Text - Se ha escuchado algo
            TextPanel.SetActive(true);
            eText.text = message;
        }
        else if(timeCounter >= messageTime)
        {
            //Desactive barras, player canWalk
            PuzzleDone();
            timeCounter = 0;
        }
    }

    private void PickObject(GameObject pickObj)
    {
        pickObj.transform.position = new Vector3(0, 0, 0);//el obj recogido va al 000
    }


    public void PlaceBust()
    {
        
        if(pos1)
        {
            //Debug.Log("position is 1");
            if(pos1free)
            {
                busto.transform.position = positions.transform.position;
                pos1free = false;
                hud.UseBusto();
                bustCorrect = true;
                Debug.Log("Position Correct");
                oP1.PutSound();
            }
            else return;
        }
        if(pos2)
        {
            //Debug.Log("position is 2");
            if(pos2free)
            {
                busto.transform.position = positions.transform.position;
                pos2free = false;
                hud.UseBusto();
                bustCorrect = false;
                oP2.PutSound();
            }
            else return;
        }
        if (pos3)
        {
            //Debug.Log("position is 3");
            if (pos3free)
            {
                busto.transform.position = positions.transform.position;
                pos3free = false;
                hud.UseBusto();
                bustCorrect = false;
                oP3.PutSound();
            }
            else return;
        }
    }
    public void PlaceMusicBox()
    {
        if(pos1)
        {
            //Debug.Log("position is 1");
            if(pos1free)
            {
                box.transform.position = positions.transform.position;
                pos1free = false;
                hud.UseBox();
                boxCorrect = false;
                oP1.PutSound();
            }
            else return;
        }
        if(pos2)
        {
            //Debug.Log("position is 2");
            if(pos2free)
            {
                box.transform.position = positions.transform.position;
                pos2free = false;
                hud.UseBox();
                boxCorrect = true;
                Debug.Log("Position Correct");
                oP2.PutSound();
            }
            else return;
        }
        if (pos3)
        {
            //Debug.Log("position is 3");
            if (pos3free)
            {
                box.transform.position = positions.transform.position;
                pos3free = false;
                hud.UseBox();
                boxCorrect = false;
                oP3.PutSound();
            }
            else return;
        }
    }
    public void PlaceBall()
    {
        if(pos1)
        {
            //Debug.Log("position is 1");
            if(pos1free)
            {
                ball.transform.position = positions.transform.position;
                pos1free = false;
                hud.UseKey();
                ballCorrect = false;
                oP1.PutSound();
            }
            else return;
        }
        if(pos2)
        {
            //Debug.Log("position is 2");
            if(pos2free)
            {
                ball.transform.position = positions.transform.position;
                pos2free = false;
                hud.UseKey();
                ballCorrect = false;
                oP2.PutSound();
            }
            else return;
        }
        if (pos3)
        {
            //Debug.Log("position is 3");
            if (pos3free)
            {
                ball.transform.position = positions.transform.position;
                pos3free = false;
                hud.UseKey();
                ballCorrect = true;
                Debug.Log("Position Correct");
                oP3.PutSound();
            }
            else return;
        }
    }
    public void PickBusto()
    {
        //PickObject(busto);
        if (busto.transform.position == oP1.transform.position)
        {
            pos1free = true;
            PickObject(busto);
        }
        else if (busto.transform.position == oP2.transform.position)
        {
            pos2free = true;
            PickObject(busto);
        }
        else if (busto.transform.position == oP3.transform.position)
        {
            pos3free = true;
            PickObject(busto);
        }
      else PickObject(busto);
    }
    public void PickBox()
    {
        if (box.transform.position == oP1.transform.position)
        {
            pos1free = true;
            PickObject(box);
        }
        else if (box.transform.position == oP2.transform.position)
        {
            pos2free = true;
            PickObject(box);
        }
        else if (box.transform.position == oP3.transform.position)
        {
            pos3free = true;
            PickObject(box);
        }
      else PickObject(box);
    }
    public void PickBall()
    {
        if (ball.transform.position == oP1.transform.position)
        {
            pos1free = true;
            PickObject(ball);
        }
        else if (ball.transform.position == oP2.transform.position)
        {
            pos2free = true;
            PickObject(ball);
        }
        else if (ball.transform.position == oP3.transform.position)
        {
            pos3free = true;
            PickObject(ball);
        }
      else PickObject(ball);
    }
    void UnlockKey()
    {
        //Se activan Barras cinematica.
        gameManager.CloseInventory();
        barrasCine.SetActive(true);
        //William no puede moverse -Pause, canWalk?-
        tank.canWalk = false;
        //Ruido LLave
        /*sound.Play();
        //Text - Se ha escuchado algo
        TextPanel.SetActive(true);
        eText.text = message;*/
        //Llave activa
        key.SetActive(true);
    }
    void PuzzleDone()
    {
        barrasCine.SetActive(false);
        tank.canWalk = true;
        TextPanel.SetActive(false);
    }
}
