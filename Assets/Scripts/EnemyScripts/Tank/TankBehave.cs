using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Threading;
using UnityEngine.Assertions.Must;

public class TankBehave : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent navAgent;
    public GameObject player;
    public Rigidbody rb;
    public DamageScript damageScript;

    [Header("Speed Values")]
    public float walkSpeed;
    public float runSpeed;
    public float speedLimit;

    [Header("Time Values")]
    public float execTime;
    public float runTime;

    [Header("Arise")]
    public float ariseDist;
    public bool canArise;

    [Header("Roaming")]
    public Transform randomPoint;
    public float roamRadius;

    [Header("Chasing")]
    public bool isClose;
    public float detectRadius;

    [Header("Preparing")]
    public bool canPrepare;
    public float prepareRadius;
    private float initialPrepTime;
    public float prepTime;

    [Header("Attacking")]
    public bool canAttack;
    public float attackForce;
    public float forceDiv;

    [Header("Bool")]
    public bool isGrabbed;
    public bool hasFallen;
    public bool canStand = false;

    [Header("Animation")]
    public Animator animator;

    public BehaveState state;
    public enum BehaveState
    {
        roaming,
        chasing,
        preparing,
        attacking,
        stunned,
    }

    private void StateHandler()
    {
        if (!isClose)
        {
            state = BehaveState.roaming;
        }
        else if (isClose)
        {
            state = BehaveState.chasing;
        }

        if (canPrepare)
        {
            state = BehaveState.preparing;
        }
        if (canAttack)
        {
            state = BehaveState.attacking;
        }
    }


    void Start()
    {
        initialPrepTime = prepTime;
        rb = gameObject.GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        randomPoint = gameObject.transform;
    }

    void Update()
    {
        //LimitVelocity();
        StateHandler();
        CheckPlayer(detectRadius);
        MovementBehave();
        CheckGrab();
    }
    private void FixedUpdate()
    {

    }
    public void CheckPlayer(float detectDistance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < ariseDist)
        {
            canArise = true;
            animator.SetBool("canArise", true);
        }
        else
        {
            canArise = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < detectDistance)
        {
            isClose = true;
        }
        else
        {
            isClose = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < prepareRadius)
        {
            canPrepare = true;
        }
        else
        {
            canPrepare = false;
        }
    }

    public void MovementBehave()
    {
        if (!isGrabbed && animator.GetBool("hasRisen") == true)
        {
            if (!isClose)
            {
                RoamAround();
            }
            else if (isClose)
            {
                runTime = 0f;
                ChasePlayer();
            }

            if (canPrepare && !canAttack)
            {
                PrepareAttack();
            }
            else
            {
                prepTime = initialPrepTime;
            }

            if (canAttack)
            {
                AttackPlayer();
            }
        }
    }
    public void RoamAround()
    {
        //Roam to new position every "execTime" seconds
        runTime += Time.deltaTime;
        if (runTime >= execTime)
        {
            animator.SetBool("isChasing", false);
            GoToRandomPoint(navAgent, randomPoint, roamRadius);
            runTime = 0f;
        }
    }
    public void ChasePlayer()
    {
        animator.SetBool("isChasing", true);
        runTime = 0f;
        navAgent.SetDestination(player.transform.position);
        navAgent.speed = runSpeed;
    }

    private Transform toLookAt;

    public void PrepareAttack()
    {
        navAgent.speed = 0f;

        if (prepTime > 0)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            prepTime -= Time.deltaTime;
        }
        if (prepTime == 0)
        {
            canAttack = true;
        }
        else if (prepTime < 0)
        {
            prepTime = 0;
        }
    }
    public void AttackPlayer()
    {
        navAgent.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.AddForce(transform.forward * attackForce / forceDiv, ForceMode.Impulse);
        Invoke(nameof(ResetAttack), 0.5f);
        animator.SetBool("canAttack", true);
    }
    public void ResetAttack()
    {
        rb.velocity = Vector3.zero;
        navAgent.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = true;
        canAttack = false;
    }
    public void GoToRandomPoint(NavMeshAgent navAgent, Transform transform, float radius)
    {
        navAgent.speed = walkSpeed;
        //random point in radius
        var randomSphere = Random.insideUnitSphere * radius;
        //turns point to 2d
        randomSphere.y = 0;

        var position = transform.position + randomSphere;

        if (NavMesh.SamplePosition(position, out var hit, radius, NavMesh.AllAreas))
        {
            navAgent.SetDestination(hit.position);
        }
    }

    public void CheckGrab()
    {
        if (isGrabbed)
        {
            rb.isKinematic = false;
            navAgent.enabled = false;
        }
        else
        {
            if (!canAttack)
            {
                rb.isKinematic = true;
            }
            navAgent.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            ResetAttack();
        }
        if (collision.gameObject.layer == 6 && isGrabbed)
        {
            hasFallen = true;
            if (hasFallen)
            {
                Debug.Log("hasCollided");
                isGrabbed = false;
                canStand = true;
            }
        }
    }
    //private void LimitVelocity()
    //{
    //    if (rb.velocity.magnitude > speedLimit)
    //    {
    //        rb.velocity = rb.velocity.normalized * speedLimit;
    //    }
    //}
}
