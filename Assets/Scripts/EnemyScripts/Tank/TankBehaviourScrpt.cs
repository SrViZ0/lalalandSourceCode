using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankBehaviourScrpt : MonoBehaviour
{
    [Header("Enemies Variable")]
    public float displacementDist; // distance in front of the nearest enemy to set nearestAlliesPos
    [Tooltip("how far to check for allies")]
    public float detectionRange; // distance to check for enemies

    private GameObject[] enemiesInRange; // stores nearby enemies
    private GameObject nearestEnemy; // stores nearest enemy
    private Vector3 nearestAlliesPos; // stores the position in front of the nearest enemy

    [Header("Self Behaviour Variables")]
    public float minBehavChangeTime; // time to change behaviour
    public float maxBehavChangeTime;
    float behavChangeTimer; // stores the timer

    public int behaviourtypes; // stores behaviour types

    void Start()
    {
        behavChangeTimer = Random.Range(minBehavChangeTime, maxBehavChangeTime);
    }

    void Update()
    {
        // Update behavior timer
        behavChangeTimer -= Time.deltaTime;
        if (behavChangeTimer <= 0f)
        {
            // Change behavior
            ChangeBehavior();
            behavChangeTimer = Random.Range(minBehavChangeTime, maxBehavChangeTime);
        }

        // Find enemies in range
        enemiesInRange = GameObject.FindGameObjectsWithTag("Enemy");

        // Find nearest enemy
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // If nearest enemy is found, calculate position in front of the rotation proxy
        if (nearestEnemy != null)
        {
            Transform rotationProxy = nearestEnemy.transform.Find("RotationProxy");
            if (rotationProxy != null)
            {
                Vector3 directionToProxy = rotationProxy.transform.position - transform.position;
                nearestAlliesPos = rotationProxy.transform.position + directionToProxy.normalized * displacementDist;
            }
        }
    }
    
    void ChangeBehavior()
    {
        // Implement your behavior change logic here
        // For example, you can randomly choose a behavior type
        behaviourtypes = Random.Range(0, 3);
        Debug.Log("Changed behavior to: " + behaviourtypes);
    }
}
