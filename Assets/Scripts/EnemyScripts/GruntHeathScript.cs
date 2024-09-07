using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntHeathScript : DeathChecker
{
    IQuestObjective[] killQuests;
    void Update()
    {
        if (!this.GetComponent<GruntBehave>().isGrabbed)
        CheckStun();
    }
    public void CheckStun()
    {
        if (stunTimer > 0)
        {
            this.GetComponent<NavMeshAgent>().enabled = false;
            Debug.Log("Stunned");
            stunTimer -= Time.deltaTime;
        }
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
