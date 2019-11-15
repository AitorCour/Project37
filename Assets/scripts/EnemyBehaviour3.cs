using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour3 : MonoBehaviour 
{	
	public enum State { Idle, Patrol, Chase, Attack, Sleep, Hit, Dead };
	public State state;
	private NavMeshAgent agent;
	private SoundEnemy sound;
    private SoundObj soundObj;
	public ParticleSystem blood;
	public float timeCounter;
    public float timeCounterHit;
    public float idleTime;
	public float sleepTime;
	public float hitTime = 3.0f;
	private bool slept;
    private bool trembled;
    private bool chasing;
	//private PlayerBehaviour plBehaviour;

	[Header("Path properties")]
    public Transform[] points;//poner los points en el orden que los seguirá. No para en el mas cercano, si tiene otro orden, preguntar
    public int currentPoint;
    public bool stopAtEachPoint;
    public float reachDistance;//el reach distance no funciona si se pone a 0.1

	public Transform player;
	public float maxAngle;
	public float maxRadius;
	//public float detectRadius;
	//public float normalRadius;
	public bool isInFov;
	public bool detected;
	[Header("Attack Properties")]
    public float attackDistance;
	public float attackTime;
    //public int EnemyDamage;
	private Animator animator;

    private int startingHealth;
	private int currentHealt;

	/*public float radius;
	public LayerMask targetMask;*/

	public float iniSpeed;
	public float finalSpeed;

	private CollisionDamage colDamage;
    private CapsuleCollider colliderEnemy;
	public bool canReciveDamage;
    public bool enemyIsDead;
    public GameObject musicaTension;
	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		sound = GetComponentInChildren<SoundEnemy>();
        soundObj = GetComponentInChildren<SoundObj>();
		animator = GetComponentInChildren<Animator>();
		//plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		colDamage = GetComponentInChildren<CollisionDamage>();

		GoNearOther();
        SetIdle();
        startingHealth = 5;
		currentHealt = startingHealth;
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
            case State.Hit:
                HitUpdate();
                break;
			default:
				break;
		 }
    }
	private void FixedUpdate () 
	{
		isInFov = inFOV(transform, player, maxAngle, maxRadius);
		if (!isInFov && chasing && detected)
		{
            //detected = false;
            StartCoroutine(DetectedTime());
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
            timeCounter = 0;
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
            //GoNextPoint();
            if(!trembled)
            {
                SetTembleques();
                trembled = true;
            }
            
            //SetIdle();
            if (/*stopAtEachPoint &&*/ timeCounter >= 5)
            {
                //animator.SetBool("trembling", false);
                SetIdle();
                GoNextPoint();
                timeCounter = 0;
                trembled = false;
                return;
            }

            else timeCounter += Time.deltaTime;
        }

        //PATROL -> CHASE
        if(detected) SetChase();
    }

	void ChaseUpdate()
    {
        agent.SetDestination(player.position);
        musicaTension.SetActive(true);
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
			//colDamage.CanDoDamage = true;
			SetAttack();
			return;
		}
	}
	void AttackUpdate()
	{
		//ATTACK -> Chase
		if(/*Vector3.Distance(transform.position, player.position) > attackDistance &&*/ timeCounter >= attackTime)
		{
			colDamage.CanDoDamage = false;
			SetChase();
            timeCounter = 0;
            Debug.Log("FromAttackToChase");
            return;
		}	
        //ATTACK -> ATTACK
        /*else if(Vector3.Distance(transform.position, player.position) <= attackDistance && timeCounter >= attackTime)
        {
            //colDamage.CanDoDamage = true;
            SetAttack();
            timeCounter = 0;
            return;
        }*/
		else timeCounter += Time.deltaTime;
	}
	void SleepUpdate()
	{
		//SLEEP -> Idle
		if(timeCounter >= sleepTime)
        {
            SetIdle();
			colDamage.CanDoDamage = false;
            timeCounter = 0;
            slept = true;
        }
        else timeCounter += Time.deltaTime;
		canReciveDamage = false;
		agent.speed = 0;
	}
    void HitUpdate()
    {
        //HIT -> IDLE
        if (timeCounterHit >= hitTime)
        {
            SetIdle();
            timeCounterHit = 0;
            //return;
        }
        else timeCounterHit += Time.deltaTime;
    }
	#region Sets

	void SetIdle()
    {
        timeCounter = 0;
        //anim.SetBool("isMoving", true);
        agent.isStopped = true;
        soundObj.Play(this.gameObject, 6);
        //particulas.Stop();
		//maxRadius = normalRadius;
        state = State.Idle;
		//radius = 2;
		animator.SetBool("Walking", false);
		canReciveDamage = true;
    }
    void SetPatrol()
    {
        //anim.SetBool("isMoving", true);
        soundObj.Play(this.gameObject, 5);
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        state = State.Patrol;
		animator.SetBool("Walking", true);
		//animator.SetBool("Attacking", false);
		//animator.SetBool("Sleeping", false);
		canReciveDamage = true;
        timeCounter = 0;
    }
	void SetChase()
    {
        if (chasing) return;
		soundObj.Play(this.gameObject, 5);
        agent.isStopped = false;
        agent.stoppingDistance = 0.9f;//La stopping distance determina la distancia 
        //a la que se para el enemigo del player. Si es mayor que el attack distance, se quedará parado
		//maxRadius = detectRadius;
        state = State.Chase;
		animator.SetBool("Walking", true);
		canReciveDamage = true;
        timeCounter = 0;
        chasing = true;
        Debug.Log("SetChase");
    }
	void SetAttack()
	{
        chasing = false;
		agent.isStopped = true;
		state = State.Attack;
        animator.SetTrigger("attack");
		canReciveDamage = true;
        soundObj.Play(this.gameObject, 1);
        timeCounter = 0;
    }
	void SetDead()
	{
        //gameObject.SetActive(false);
        musicaTension.SetActive(false);
        state = State.Dead;
		agent.speed = 0.0f;//Parar al enemy cuando esta muerto
        animator.SetTrigger("dead");
		colDamage.CanDoDamage = false;
        colliderEnemy.enabled = false;
        soundObj.Play(this.gameObject, 3);
        //this.enabled = false;
        enemyIsDead = true;
	}
	void SetSleep()
	{
		agent.isStopped = true;
		state = State.Sleep;
        animator.SetTrigger("sleep");
        soundObj.Play(this.gameObject, 2);
		//animator.SetBool("Attacking", false);
		//animator.SetBool("Walking", false);
	}
    void SetHit()
    {
        agent.isStopped = true;
        state = State.Hit;
        detected = true;
        StartCoroutine(DetectedTime());
        soundObj.Play(this.gameObject, 0);
        animator.SetTrigger("hit");
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
			blood.Play();
            if (currentHealt != 2 && currentHealt != 0)
            {
                SetHit();
            }
            else if (currentHealt == 2)
            {
                SetSleep();
            }
            else if (currentHealt <= 0)
            {
                SetDead();
            }
        }	
	}
    void SetTembleques()
    {
        //animator.SetBool("trembling", true);
        animator.SetTrigger("trembling");
        animator.SetBool("Walking", false);
    }
    public void PlayFootstepEnemy()
    {
        sound.PlayF();
    }
    public void PlayDeadFall()
    {
        soundObj.Play(this.gameObject, 4);
    }

    private IEnumerator DetectedTime()
    {
        yield return new WaitForSeconds(5);
        detected = false;
        Debug.Log("NotDetected");
        chasing = false;
    }
}
