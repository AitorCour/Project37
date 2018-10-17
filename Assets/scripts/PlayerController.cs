using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 axis;
    public float speed;
    public Vector3 moveDirection;
    private float forceToGround = Physics.gravity.y;
    public float jumpSpeed;
    public bool jump;
    public float gravityMag;
    //public Animator animacion;

	// Use this for initialization
	void Start ()
    {
       // moveDirection.x = axis.x * speed;
      //  moveDirection.z = axis.y * speed;
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(controller.isGrounded && !jump)
        {
            moveDirection.y = forceToGround;
        }

        else
        {
            jump = false;
            moveDirection.y += Physics.gravity.y * gravityMag * Time.deltaTime;
        }

        Vector3 transformDirection = axis.x * transform.right + axis.y * transform.forward;


        moveDirection.x = transformDirection.x * speed;
        moveDirection.z = transformDirection.z * speed;
        

        controller.Move(moveDirection * Time.deltaTime);
	}

    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
        if(axis.x != 0 || axis.y !=0)
            {
           // animacion.SetBool("isWalking", true);
            }
        else
        {
           // animacion.SetBool("isWalking", false);
        }
    }

    public void StartJump()
    {
        if(!controller.isGrounded) return;

        moveDirection.y = jumpSpeed;
        jump = true;
    }

    
}
