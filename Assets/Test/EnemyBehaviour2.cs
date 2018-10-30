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

	/*[Header("Target2 properties")]
	public float fieldOfViewAngle = 110f;
	public bool playerInSight;

	private SphereCollider col;
	private GameObject player;*/
	public Transform player;
	public float maxAngle;
	public float maxRadius;
	private bool isInFov = false;
	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();

		GoNearOther();
        SetIdle();
	}

	private void OnDrawGizmos() //Dibujar el campo de visión
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, maxRadius);

		Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
		Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position, fovLine1);
		Gizmos.DrawRay(transform.position, fovLine2);

		if (!isInFov)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.green;
			//Debug.Log("green");
		
		Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

		Gizmos.color = Color.black;
		Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
	}
	public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
	{
		Collider[] overlaps = new Collider[10];
		int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

		for(int i = 0; i < count + 1; i++)
		{
			if(overlaps[i] != null)
			{
				if(overlaps[i].transform == target)
				{
					Vector3 directionBetween = (target.position - checkingObject.position).normalized;
					directionBetween.y *= 0;

					float angle = Vector3.Angle(checkingObject.forward, directionBetween);

					if(angle <= maxAngle)
					{
						Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
						RaycastHit hit;

						if(Physics.Raycast(ray, out hit, maxRadius))
						{
							if (hit.transform == target)
							{
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}
	// Update is called once per frame
	private void Update () 
	{
		isInFov = inFOV(transform, player, maxAngle, maxRadius);

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
	/*void OnTriggerStay(Collider other)
    {
		//NEW
		if(other.gameObject == player)
		{
			targetDetected = false;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;

				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
				{
					if(hit.collider.gameObject == player)
					{
						Debug.Log("Seen");
						targetDetected = true;
					}
				}
			}
		}	
    }*/
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
