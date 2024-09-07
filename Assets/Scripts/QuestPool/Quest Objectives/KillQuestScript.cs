using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class KillQuestScript : QuestObjective, IQuestObjective //This thing is a placeholder btw 0w0
{
    [Header("Required")]
    private int enemyDeathCount;
    [SerializeField] private List<GameObject> deathMethods = new List<GameObject>();
    [Tooltip("How many enemies to kill in total to finish quest section")]
    [SerializeField] private int enemiesToKill;
    [SerializeField] private List<GameObject> enemyTypes;
    [Header("Config")]
    [Tooltip("Toggle for Kill XX no. of enemies in XX seconds")]
    [SerializeField] private bool groupKillOnly;
    [SerializeField] private float timeGiven;

    private float timer;
    //[Header("Debugging")]
    //[SerializeField] private bool killEnenmy = false;

    private void Awake()
    {
        timer = 0;
        if (timeGiven <= 0.01f)
        {
            timeGiven = Mathf.Infinity;
        }
    }
    private void Update()
    {
        ObjectiveCompeleteCheck();

        if (!groupKillOnly) return;

        switch (timer)
        {
            case >0 : timer -= Time.deltaTime;
                break;

            case 0  : enemyDeathCount = 0;
                break;

            case <0 : timer = 0;
                break;
        }
    }

    private void ObjectiveCompeleteCheck()
    {
        if (enemyDeathCount >= enemiesToKill) // quest compelete check placeholder
        {
            FinishQuestObjective();
        }
    }

    private void UpdateState()
    {
        string state = null;
        string status = null;
        ChangeState(state, status);
        //Debug.Log(status + "updated");

    }
    protected override void SetQuestObjState(string state)
    {
        UpdateState();
    }

    public void EnemyEliminated(GameObject enemy)
    {
        if (enemy.Equals(null))
        {
            return;
        }
        DeathChecker enemyInfo = enemy.GetComponent<DeathChecker>();


        if (CheckEnemyType(enemy) && CheckDeathCause(enemyInfo)) //Run 2 Loops at once
        {
            enemyDeathCount++;
            if (!groupKillOnly) return;
            timer = timeGiven;
        }

    }

    bool CheckEnemyType(GameObject enemy)
    {
        foreach (GameObject enemyType in enemyTypes)
        {
            //Debug.Log(enemy.GetComponent<UID_Generator>().id == enemyType.GetComponent<UID_Generator>().id);
            if (enemy.GetComponent<UID_Generator>().id == enemyType.GetComponent<UID_Generator>().id) //check if correct enemy is killed
            {
                return true;
            }
        }
        return false;
    }

    bool CheckDeathCause(DeathChecker enemyInfo)
    {
        foreach (GameObject deathMethod in deathMethods)
        {
            //Debug.Log(enemy.GetComponent<UID_Generator>().id == enemyType.GetComponent<UID_Generator>().id);
            if (enemyInfo.deathCause.GetComponent<UID_Generator>().id == deathMethod.gameObject.GetComponent<UID_Generator>().id) //Check if death cause matches the ones listed
            {
                return true;
            }
        }
        return false;
    }
}