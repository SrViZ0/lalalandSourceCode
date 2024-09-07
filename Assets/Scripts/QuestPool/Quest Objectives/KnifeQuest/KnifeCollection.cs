using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollection : QuestObjective
{
    public void ObjectiveCompeleteCheck()
    {
        FinishQuestObjective();
    }

    private void UpdateState() //dont touche generally
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
}
