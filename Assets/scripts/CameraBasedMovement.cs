using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasedMovement : MonoBehaviour
{
    public GameObject cameraDef;

    public float speed;
    public float iniSpeed;
    public float rotSpeed;
    public float runSpeed;

    private bool isRunning;
    public bool canWalk;
    public bool pointing;

    private Animator animator;
    private PlayerBehaviour plBehaviour;
    private CameraManager cameraManager;
    public GameObject william;

    [Header("Speed Settings")]
    public float walkSpeed_1;
    public float walkSpeed_2;
    public float walkSpeed_3;
    public float runSpeed_1;
    public float runSpeed_2;

    private float timeCount = 5;
    private Vector3 rightDef;
    private Vector3 forwardDef;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        //animator = null;
        //isRunning = false;
        plBehaviour = GetComponent<PlayerBehaviour>();
        cameraManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<CameraManager>();
        //ChangeCamera();
        //plBehaviour = null;
        SetSpeed();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            Vector3 right = cameraDef.transform.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            rightDef = right;
            forwardDef = forward;
        }
        
        Vector3 movement = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            movement += rightDef * (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
            movement += forwardDef * (Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
            canWalk = true;
        }
        else canWalk = false;
        transform.Translate(movement);
        if(movement != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement);
            william.transform.rotation = Quaternion.RotateTowards(william.transform.rotation, lookRotation, timeCount);
            
        }
        if (canWalk == true && !pointing && !plBehaviour.damageRecived)
        {
            
                if (!isRunning)
                {
                    //animator.SetBool("Walking", true);
                    if (plBehaviour.playerLife == 3)
                    {
                        animator.SetBool("Walking", true);
                        animator.SetBool("Walking2", false);
                        animator.SetBool("Walking3", false);
                        SetSpeed();
                    }
                    else if (plBehaviour.playerLife == 2)
                    {
                        animator.SetBool("Walking2", true);
                        animator.SetBool("Walking", false);
                        animator.SetBool("Walking3", false);
                        SetSpeed();
                    }
                    else if (plBehaviour.playerLife == 1)
                    {
                        animator.SetBool("Walking3", true);
                        animator.SetBool("Walking", false);
                        animator.SetBool("Walking2", false);
                        SetSpeed();
                    }
                    animator.SetBool("Running", false);
                    animator.SetBool("Running2", false);
                }
                else if (isRunning)
                {
                    //animator.SetBool("Running", true);
                    if (plBehaviour.playerLife == 3)
                    {
                        animator.SetBool("Running", true);
                        animator.SetBool("Running2", false);
                    }
                    else if (plBehaviour.playerLife == 2)
                    {
                        animator.SetBool("Running", false);
                        animator.SetBool("Running2", true);
                    }
                    animator.SetBool("Walking", false);
                    animator.SetBool("Walking2", false);
                    animator.SetBool("Walking3", false);
                }
            

            else
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Walking2", false);
                animator.SetBool("Walking3", false);

                animator.SetBool("Running", false);
                animator.SetBool("Running2", false);
                animator.SetBool("WalkingBack", false);
                animator.SetBool("Back2", false);
                animator.SetBool("Back3", false);
            }
            //transform.Rotate(0, x, 0);

            if (Input.GetButton("Run") && plBehaviour.playerLife >= 2)
            {
                isRunning = true;
                SetSpeed();
            }
            if (isRunning && plBehaviour.playerLife <= 1)
            {
                isRunning = false;
            }

            if (Input.GetButtonUp("Run"))
            {
                //speed = iniSpeed;
                isRunning = false;
            }
        }

        if (pointing && canWalk)
        {
            //Debug.Log("Pointing");
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                var x = 1 * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                var x = -1 * Time.deltaTime * rotSpeed;
                transform.Rotate(0, x, 0);
                //animator.SetBool("Walking", true);
            }
        }

        if (!canWalk)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Walking2", false);
            animator.SetBool("Walking3", false);
            animator.SetBool("Running", false);
            animator.SetBool("Running2", false);
        }
        if (plBehaviour.playerLife == 2)
        {
            animator.SetBool("Injured", true);
        }
        else animator.SetBool("Injured", false);

        if (plBehaviour.playerLife == 1)
        {
            animator.SetBool("SuperInjured", true);
        }
        else animator.SetBool("SuperInjured", false);
    }

    public void SetSpeed()
    {
        if (isRunning)
        {
            if (plBehaviour.playerLife == 3)
            {
                speed = runSpeed_1;
            }
            else if (plBehaviour.playerLife == 2)
            {
                speed = runSpeed_2;
            }
        }
        else if (!isRunning)
        {
            if (plBehaviour.playerLife == 3)
            {
                speed = walkSpeed_1;
            }
            else if (plBehaviour.playerLife == 2)
            {
                speed = walkSpeed_2;
            }
            else if (plBehaviour.playerLife == 1)
            {
                speed = walkSpeed_3;
            }
        }
    }
    public void ChangeCamera()
    {
        cameraDef = cameraManager.actualCamera;
        Debug.Log("CameraChanged");
    }

}
