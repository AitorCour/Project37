using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CreeperBehaviour : MonoBehaviour
{
    /*public enum State { Idle, Patrol, Chase, Attack, Sleep};
    public State state;

    private Animator anim;
    private NavMeshAgent agent;
    private SoundPlayer sound;
    public ParticleSystem particulas;

    private float timeCounter;
    public float idleTime = 1.0f;

    [Header("Path properties")]
    public Transform[] points;
    public int currentPoint;
    public bool stopAtEachPoint;
    public float reachDistance = 0.1f;

    [Header("Overlap properties")]
    private float radius;
    public LayerMask mask;
    public bool TargetDetected;
    public Transform Targettransform;
    public float radiusIdle;
    public float radiusChase;

    [Header("Attack/Sleep properties")]
    public float attackdistance;
    public bool Sleeping;
    private float timeCounterSleep;
    //public bool Attacking;
    //private float timeCounterAttack;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        sound = GetComponentInChildren<SoundPlayer>();

        GoNearOther();

        SetIdle();

        particulas.Stop();
    }

    void Update()
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
            /*case State.Attack:
                AttackUpdate();
                break;*/
            /*case State.Sleep:
                SleepUpdate();
                break;
            default:
                break;
        }

    }

    private void FixedUpdate()
    {
        TargetDetected = false;
           
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
        if(hitColliders.Length != 0)
        {
            TargetDetected = true;
            Targettransform = hitColliders[0].transform;   
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
        if(TargetDetected) SetChase();
        
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
        if(TargetDetected) SetChase();
    }

    void ChaseUpdate()
    {
        agent.SetDestination(Targettransform.position);

        //CHASE -> IDLE

        if(!TargetDetected)
        {
            SetIdle();
            GoNextPoint();
            return;
        }

        //CHASE -> ATTACK

        /*if(Vector3.Distance(transform.position, Targettransform.position) <= attackdistance && !Sleeping && !Attacking)
        {
            SetAttack();
        }*//*
    }

    public void AttackUpdate()
    {
        timeCounterAttack += Time.deltaTime;

        //SLEEP -> IDLE

        if(timeCounterAttack >= 2f)
        {
            SetIdle();
            timeCounterAttack = 0;
            Attacking = false;
        }
    }*/

    /*public void SleepUpdate()
    {
        timeCounterSleep += Time.deltaTime;

        //SLEEP -> IDLE

        if (timeCounterSleep >= 5f)
        {
            SetIdle();
            timeCounterSleep = 0;
            Sleeping = false;
            particulas.Stop();
        }
    }

    #region Sets
    void SetIdle()
    {
        timeCounter = 0;
        anim.SetBool("isMoving", true);
        agent.isStopped = true;
        radius = radiusIdle;
        particulas.Stop();
        state = State.Idle;
    }
    void SetPatrol()
    {
        anim.SetBool("isMoving", true);
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        state = State.Patrol;
    }
    void SetChase()
    {
        anim.SetBool("isMoving", false);
        anim.SetTrigger("IsChasing");
        agent.isStopped = false;
        agent.stoppingDistance = 2.4f;
        radius = radiusChase;
        state = State.Chase;
    }
    /*void SetAttack()
    {

        anim.SetBool("isMoving", false);
        agent.isStopped = true;
        Attacking = true;
        state = State.Attack;
    }*/
   /* public void SetSleep()
    {
        anim.SetBool("isMoving", false);
        anim.SetTrigger("isSleep");
        agent.isStopped = true;
        timeCounterSleep = 0;
        Sleeping = true;
        particulas.Play();
        state = State.Sleep;
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
        for(int i = 0; i < points.Length; ++i)
        {
            float Dis = Vector3.Distance(transform.position, points[i].position);
            if(Dis < minDistance)
            {
                minDistance = Dis;
                currentPoint = i;
            }
        }

        agent.SetDestination(points[currentPoint].position);
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }*/
}
