using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpQuest : QuestObjective
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectiveCompeleteCheck();
    }

    void ObjectiveCompeleteCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FinishQuestObjective();
        }
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
