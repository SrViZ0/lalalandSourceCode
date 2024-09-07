using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Threading;

public class RangedBehave : MonoBehaviour
{
    //NavMesh
    public static NavMeshAgent navAgent;

    //Player Object
    public GameObject player;
    public GameObject bullet;

    //for Roaming
    public Transform randomPoint;
    public float roamRadius;

    //for Shooting
    public Transform shootPoint;
    public float attackRunTime;
    public float attackInterval;

    //Speed Values
    public float walkSpeed;
    public float runSpeed;

    //For Chasing Conditions
    public bool isClose;
    public bool canRun;
    public float runRadius;
    public float detectRadius;

    public float execTime;
    public float runTime;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        randomPoint = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer(detectRadius, runRadius);
        MovementBehave();
    }
    public void CheckPlayer(float detectDistance, float runDistance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectDistance)
        {
            isClose = true;
        }
        else
        {
            isClose = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < runDistance)
        {
            canRun = true;
        }
        else
        {
            canRun = false;
        }
    }
    public void MovementBehave()
    {
        if (isClose)
        {
            Attack();
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

        if (canRun)
        {
            RunAway();
        }
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
    public void Attack()
    {
        navAgent.speed = 0f;
        transform.LookAt(player.transform);
        //Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
        attackRunTime += Time.deltaTime;
        if (attackRunTime >= attackInterval)
        {
            Instantiate(bullet, shootPoint.transform.position, gameObject.transform.rotation);
            attackRunTime = 0f;
        }
    }
    public void RunAway()
    {

    }
}
