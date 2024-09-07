using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankHealthScript : DeathChecker
{
    IQuestObjective[] killQuests;

    public Animator tankAnimator;

    //Do not decalre start

    void Update()
    {
        if (!this.GetComponent<TankBehave>().isGrabbed)
            CheckStun();
    }
    public void CheckStun()
    {
        if (stunTimer > 0)
        {
            this.GetComponent<NavMeshAgent>().enabled = false;
            //tankAnimator.SetBool("isStunned", true);
            Debug.Log("Stunned");
            stunTimer -= Time.deltaTime;
        }
        //tankAnimator.SetBool("isStunned", false);
        this.GetComponent<NavMeshAgent>().enabled = true;
    }

    private void OnDestroy()
    {
        if (MonoBehaviour.FindObjectsOfType<KillQuestScript>() == null) return;
        killQuests = MonoBehaviour.FindObjectsOfType<KillQuestScript>();
        foreach (IQuestObjective instance in killQuests)
        {
            instance.EnemyEliminated(this.gameObject);
        }

        if (MonoBehaviour.FindAnyObjectByType<QuestStart>() == null) return;
        QuestStart[] questPoints = MonoBehaviour.FindObjectsOfType<QuestStart>(); //should work in theory
        foreach (QuestStart questStart in questPoints)
        {
            questStart.CallQuestLogUpdate();
        }

    }

    public override void TriggerDash()
    {
        return;
    }

}
