using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Variables")]
    public GameObject enemyPrefab;
    [SerializeField] int curretnEnemyCount, maxEnemyCount, spawnCount;

    [Header("Player Vars")]
    [SerializeField] float playerDist, spawnDist, spawnInterval, offsetY_Value;

    private float timer;
    
    //Default Y offset is 0.25f
    public void Awake()
    {
        timer = 0;
    }

    private void Update()
    {
        RunTimer();
        if (RangeCheck() && !CountEnemy() && !SpawnTimer() /* && !VisibilityCheck()*/)
        {
            SpawnEnemy();
        }

    
    }

    public bool SpawnTimer()
    {
        if (timer > spawnInterval)
        {
            timer = 0;
            return false;
        }
        else
        return true;
    }

    public void RunTimer()
    {
        timer += Time.deltaTime;
    }
    //tisim coding
    public void SpawnEnemy()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefab, this.transform.position, Quaternion.identity, this.transform);
        }

    }
    public bool CountEnemy()
    {
        curretnEnemyCount = transform.childCount;
        if (curretnEnemyCount < maxEnemyCount)
        {
            return false;
        }
        else
        return true;
    }
    public bool RangeCheck()
    {
        playerDist = Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
        if (playerDist < spawnDist)
        {
            return true;
        }
        else
        return false;
    }

    public bool VisibilityCheck()
    {
        RaycastHit hitInfo;

        Physics.Raycast(this.transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - this.transform.position + new Vector3(0, offsetY_Value, 0), out hitInfo);

        Debug.DrawRay(this.transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position - this.transform.position + new Vector3(0, offsetY_Value, 0), Color.green);

        Debug.Log("Enemy Spawner ray hitting: " + hitInfo.collider.gameObject); 

        if (hitInfo.collider.gameObject == GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().gameObject && hitInfo.collider != null)
        {
            //Debug.Log("I see you");
            return false;
        }
        else
        {
            return true;
        }
    }

}
