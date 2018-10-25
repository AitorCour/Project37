using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour 
{
	public enum State { Idle, Patrol};
	public State state;
	private NavMeshAgent agent;

	private float timeCounter;
    public float idleTime = 1.0f;

    [Header("Path properties")]
    public Transform[] points;//poner los points en el orden que los seguirá. No para en el mas cercano, si tiene otro orden, preguntar
    public int currentPoint;
    public bool stopAtEachPoint;
    public float reachDistance;//el reach distance no funciona si se pone a 0.1
	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();

		GoNearOther();
        SetIdle();
	}
	
	// Update is called once per frame
	void Update () 
	{
		 switch(state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Patrol:
                PatrolUpdate();
                break;
		}
	}

	void IdleUpdate()
    {   
        //IDLE -> PATROL
        if(timeCounter >= idleTime)
        {
            SetPatrol();
        }
        else timeCounter += Time.deltaTime;

        //IDLE -> CHASE
        //if(TargetDetected) SetChase(); 
    }

	void PatrolUpdate()
    {
        //PATROL -> IDLE
        if(Vector3.Distance(transform.position, points[currentPoint].position) < reachDistance)
        {
            GoNextPoint();
            if(stopAtEachPoint) SetIdle();
        }

        //PATROL -> CHASE
        //if(TargetDetected) SetChase();
    }

	#region Sets

	void SetIdle()
    {
        timeCounter = 0;
        //anim.SetBool("isMoving", true);
        agent.isStopped = true;
        //radius = radiusIdle;
        //particulas.Stop();
        state = State.Idle;
    }
    void SetPatrol()
    {
        //anim.SetBool("isMoving", true);
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        state = State.Patrol;
    }

	#endregion

	void GoNextPoint()
    {
        currentPoint++;
        if(currentPoint >= points.Length) currentPoint = 0;

        agent.SetDestination(points[currentPoint].position);
    }

    void GoNearOther()
    {
        float minDistance = Mathf.Infinity;
        for(int i = 0; i < points.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, points[i].position);
            if(dist < minDistance)
            {
                minDistance = dist;
                currentPoint = i;
            }
        }
        agent.SetDestination(points[currentPoint].position);
    }
}
