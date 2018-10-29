using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour2 : MonoBehaviour 
{
	public enum State { Idle, Patrol, Chase};
	public State state;
	private NavMeshAgent agent;

	private float timeCounter;
    public float idleTime = 1.0f;

    [Header("Path properties")]
    public Transform[] points;//poner los points en el orden que los seguirá. No para en el mas cercano, si tiene otro orden, preguntar
    public int currentPoint;
    public bool stopAtEachPoint;
    public float reachDistance;//el reach distance no funciona si se pone a 0.1

	[Header("Target properties")]
    public LayerMask targetMask;
    public bool targetDetected;
    public Transform targetTransform;
    //public float radius;

	[Header("Target2 properties")]
	public float fieldOfViewAngle = 110f;
	public bool playerInSight;

	private SphereCollider col;
	private GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
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
			case State.Chase:
                ChaseUpdate();
                break;	
		}
	}

	private void FixedUpdate()
    {
        //!!!
        targetDetected = false; 
        /*Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, targetMask);
        if(hitColliders.Length != 0)
        {
            targetDetected = true;
            targetTransform = hitColliders[0].transform;
        }*/
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
        if(targetDetected) SetChase(); 
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
        if(targetDetected) SetChase();
    }

	void ChaseUpdate()
    {
        agent.SetDestination(targetTransform.position);

        //CHASE -> IDLE

        if(!targetDetected)
        {
            SetIdle();
            GoNextPoint();
            return;
        }

        //CHASE -> ATTACK

        /*if(Vector3.Distance(transform.position, Targettransform.position) <= attackdistance && !Sleeping && !Attacking)
        {
            SetAttack();
        }*/
    }

	#region Sets

	void SetIdle()
    {
        timeCounter = 0;
        //anim.SetBool("isMoving", true);
        agent.isStopped = true;
        //radius = 5;//Aplica el radio
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
	void OnTriggerStay(Collider other)
    {
		//NEW
		if(other.gameObject == player)
		{
			playerInSight = false;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;

				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
				{
					if(hit.collider.gameObject == player)
					{
						playerInSight = true;
						Debug.Log("Seen");
						targetDetected = true;
					}
				}
			}
		}	
    }
	void SetChase()
    {
        //anim.SetBool("isMoving", false);
        //anim.SetTrigger("IsChasing");
        agent.isStopped = false;
        agent.stoppingDistance = 2.4f;
        //radius = 10;//el radio se hace mas grande
        state = State.Chase;
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
	/*private void OnDrawGizmos()
    {
        Color color = Color.red;
        color.a = 0.1f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }*/
}
