﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour3 : MonoBehaviour 
{	
	public enum State { Idle, Patrol, Chase, Attack, Sleep, Dead };
	public State state;
	private NavMeshAgent agent;
	private SoundPlayer sound;

	private float timeCounter;
    public float idleTime = 1.0f;
	public float sleepTime = 3.0f;
	public float hitTime = 3.0f;
	private bool sleeping;
	private bool slept;
	private PlayerBehaviour plBehaviour;

	[Header("Path properties")]
    public Transform[] points;//poner los points en el orden que los seguirá. No para en el mas cercano, si tiene otro orden, preguntar
    public int currentPoint;
    public bool stopAtEachPoint;
    public float reachDistance;//el reach distance no funciona si se pone a 0.1

	public Transform player;
	public float maxAngle;
	public float maxRadius;
	public float detectRadius;
	public float normalRadius;
	private bool isInFov = false;
	public bool detected = false;
	[Header("Attack Properties")]
    public float attackDistance;
	private float attackTime = 3.25f;
    public int EnemyDamage;
	private Animator animator;

	private int startingHealth = 5;
	public int currentHealt;

	/*public float radius;
	public LayerMask targetMask;*/

	private float iniSpeed = 0.6f;
	private float finalSpeed = 1.0f;

	private CollisionDamage colDamage;
    private CapsuleCollider colliderEnemy;
	public bool canReciveDamage;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		sound = GetComponentInChildren<SoundPlayer>();
		animator = GetComponentInChildren<Animator>();
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		colDamage = GetComponentInChildren<CollisionDamage>();

		GoNearOther();
        SetIdle();
		currentHealt = startingHealth;
		sleeping = false;
        colliderEnemy = GetComponent<CapsuleCollider>();
		
		canReciveDamage = true;
		slept = false;
		agent.speed = iniSpeed;
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
		{
			Gizmos.color = Color.red;
			//detected = false;
		}
		else
		{
			Gizmos.color = Color.green;
			//detected = true;
		}
		Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

		Gizmos.color = Color.black;
		Gizmos.DrawRay(transform.position, transform.forward * maxRadius);

		/*Color color = Color.red;
        color.a = 0.1f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);*/
	}
	public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
	{
		Collider[] overlaps = new Collider[100];
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
			case State.Attack:
				AttackUpdate();
				break;
			case State.Sleep:
				SleepUpdate();
				break;
			default:
				break;
		}
    }
	private void FixedUpdate () 
	{
		isInFov = inFOV(transform, player, maxAngle, maxRadius);
		if (!isInFov)
		{
			detected = false;
		}
		if (isInFov)
		{
			detected = true;
		}
		if(detected)
		{
			agent.SetDestination(player.position);//Coje la position del player y va a por él
		}
		/*Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, targetMask);
        if(hitColliders.Length != 0)
        {
            detected = true;
            //targetTransform = hitColliders[0].transform;
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
        if(detected) SetChase(); 
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
        if(detected) SetChase();
    }

	void ChaseUpdate()
    {
        agent.SetDestination(player.position);
		if(!slept)
		{
			agent.speed = iniSpeed;//Velocidad aumenta cuando ve al player
		}
		else if(slept)
		{
			agent.speed = finalSpeed;
		}
		
        //CHASE -> IDLE

        if(!detected)
        {
            SetIdle();
            GoNextPoint();
			//agent.speed = 0.5f;//Velocidad vuelve a la normalidad si no ve al player
			colDamage.CanDoDamage = false;
            return;
        }
		//Chase -> Attack
		else if(Vector3.Distance(transform.position, player.position) <= attackDistance)
		{
			colDamage.CanDoDamage = true;
			SetAttack();
			return;
		}
	}
	void AttackUpdate()
	{
		//ATTACK -> Chase
		if(Vector3.Distance(transform.position, player.position) > attackDistance && timeCounter >= attackTime)
		{
			colDamage.CanDoDamage = false;
			SetChase();
			return;
		}
		
		else timeCounter += Time.deltaTime;
	}
	void SleepUpdate()
	{
		//SLEEP -> Idle
		if(timeCounter >= sleepTime)
        {
            SetIdle();
			//currentHealt = startingHealth;
			sleeping = false;
			colDamage.CanDoDamage = false;
			//canReciveDamage = true;
        }
        else timeCounter += Time.deltaTime;
		canReciveDamage = false;
		agent.speed = finalSpeed;
		slept = true;
	}
	#region Sets

	void SetIdle()
    {
        timeCounter = 0;
        //anim.SetBool("isMoving", true);
        agent.isStopped = true;
        //particulas.Stop();
		maxRadius = normalRadius;
        state = State.Idle;
		//radius = 2;
		animator.SetBool("Walking", false);
		animator.SetBool("Attacking", false);
		animator.SetBool("Sleeping", false);
		canReciveDamage = true;
    }
    void SetPatrol()
    {
        //anim.SetBool("isMoving", true);
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        state = State.Patrol;
		animator.SetBool("Walking", true);
		animator.SetBool("Attacking", false);
		animator.SetBool("Sleeping", false);
		canReciveDamage = true;
    }
	void SetChase()
    {
        //anim.SetBool("isMoving", false);
        //anim.SetTrigger("IsChasing");
		sound.Play(1, 2);
        agent.isStopped = false;
        agent.stoppingDistance = 0.9f;//La stopping distance determina la distancia a la que se para el enemigo del player. Si es mayor que el attack distance, se quedará parado
		maxRadius = detectRadius;
        state = State.Chase;
		animator.SetBool("Walking", true);
		animator.SetBool("Attacking", false);
		animator.SetBool("Sleeping", false);
		//radius = 3;
		canReciveDamage = true;
    }
	void SetAttack()
	{
		agent.isStopped = true;
		state = State.Attack;
		animator.SetBool("Attacking", true);
		animator.SetBool("Walking", false);
		animator.SetBool("Sleeping", false);
		canReciveDamage = true;
	}
	void SetDead()
	{
		//gameObject.SetActive(false);
		state = State.Dead;
		agent.speed = 0.0f;//Parar al enemy cuando esta muerto
		animator.SetBool("Dead", true);
		colDamage.CanDoDamage = false;
        colliderEnemy.enabled = false;
		this.enabled = false;
	}
	void SetSleep()
	{
		agent.isStopped = true;
		sleeping = true;
		state = State.Sleep;
		animator.SetBool("Sleeping", true);
		animator.SetBool("Attacking", false);
		animator.SetBool("Walking", false);
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

	public void ReciveDamage(int amount, Vector3 hitPoint)
	{
		if(!canReciveDamage)
		{
			return;
		}
		else
		{
			currentHealt -= amount;
			animator.SetTrigger("hit");
			//agent.isStopped = true;
			agent.SetDestination(player.position);
			
			if(currentHealt == 2)
			{
				SetSleep();
				sleeping = true;
			}
			else if(currentHealt <= 0)
			{
				SetDead();
			}
		}	
	}
	/*public void DeadlyDamage(Vector3 hitPoint)
	{
		//currentHealt = 0;
		SetDead();
	}*/
}
