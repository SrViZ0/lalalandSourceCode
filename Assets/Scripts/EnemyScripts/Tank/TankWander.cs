using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Threading;

public class TankWander : MonoBehaviour
{
    //NavMesh
    public static NavMeshAgent navAgent;

    //Player Object
    public GameObject player;

    //for Roaming
    public Transform randomPoint;
    public float roamRadius;

    //Speed Values
    public float walkSpeed;
    public float runSpeed;

    //For Chasing Conditions
    public bool isClose;
    public float detectRadius;


    public float execTime;
    public float runTime;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        randomPoint = gameObject.transform;
    }

    void Update()
    {
        CheckPlayer(detectRadius);
        MovementBehave();
    }
    public void CheckPlayer(float detectDistance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectDistance)
        {
            isClose = true;
        }
        else
        {
            isClose = false;
        }
    }

    public void MovementBehave()
    {
        if (isClose)
        {
            runTime = 0f;
            ChasePlayer();
        }
        else if (!isClose)
        {
            //Roam to new position every "execTime" seconds
            runTime += Time.deltaTime;
            if (runTime >= execTime)
            {
                GoToRandomPoint(navAgent, randomPoint, roamRadius);
                runTime = 0f;
            }
        }
    }
    public void ChasePlayer()
    {
        runTime = 0f;
        navAgent.SetDestination(player.transform.position);
        navAgent.speed = runSpeed;
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
}
