using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Collection : QuestObjective
{
    public bool reachedObjtv;
    //public GameObject startPoint;
    public Transform checkpointParent;

    //public GameObject endPoint;
    //public Transform[]coords; //0 is startpoint, 1 is endpoint.  

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        checkpointParent = GameObject.Find("Collecitables Points").gameObject.transform;
        //Instantiate(startpoint, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position, Quaternion.identity, this.transform); //alt
        //Instantiate(endPoint, coords[1].position, coords[1].rotation, this.transform);
    }

    private void Update()
    {
        ObjectiveCompeleteCheck(); //check for quest compeletion every frame

    }

    private void ObjectiveCompeleteCheck()
    {
        int childCount = checkpointParent.transform.childCount;
        
        if (childCount > 0)
        {
            reachedObjtv = false;
        }
        else
        {
            reachedObjtv = true;
        }
        if (reachedObjtv)
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
