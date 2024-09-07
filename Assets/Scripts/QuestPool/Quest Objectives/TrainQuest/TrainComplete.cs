using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class TrainComplete : QuestObjective
{
    public bool reachedObjtv;
    public GameObject trainPart;
    

    private void Awake()
    {
        trainPart = GameObject.Find("Collect Part");
    }

    private void Update()
    {
        ObjectiveCompeleteCheck(); //check for quest compeletion every frame
    }

    private void ObjectiveCompeleteCheck()
    {
        if (trainPart.GetComponent<CollectObject>().isCollected)
        {
            FinishQuestObjective(); //Send out update to compelete 
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
