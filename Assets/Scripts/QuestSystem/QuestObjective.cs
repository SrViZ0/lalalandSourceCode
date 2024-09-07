using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class QuestObjective : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;
    private int stepIndex;

    public void InitializeQuestObjective(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        if (questStepState != null && questStepState != "")
        {
            SetQuestObjState(questStepState);
        }
    }

    protected void FinishQuestObjective()
    {
            if (!isFinished)
            {
                isFinished = true;
                GameEventsManager.instance.questEvents.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }
    }
    protected void ChangeState(string newState, string newStatus)
    {
            GameEventsManager.instance.questEvents.QuestStepStateChange(
            questId,
            stepIndex,
            new QuestStepState(newState, newStatus)
        );
    }
    protected abstract void SetQuestObjState(string state);
}
