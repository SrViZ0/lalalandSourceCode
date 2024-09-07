using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UbelEats : QuestObjective //Capture the Flag thing idk
{
    [Header("Config")]
    public bool checkPoints;
    public Transform checkpointParent;

    [Header("Adds")]
    public bool noGrapple;
    [Space(1)]
    public bool flagEnable;
    public GameObject flagMesh;

    private GameObject flagObject;

    

    [Header("Required")]
    public bool timerOn;
    public float timer;
    [Space(1)]
    public GameObject startPoint, endPoint;

    private float rundownTimer;
    private TextMeshProUGUI timerDisplay;

    public static bool questOngoing;
    private bool raceStarted;

    private void Awake()
    {
    }
    private void OnEnable()
    {
        timerDisplay = GameObject.Find("QuestTimer").GetComponent<TextMeshProUGUI>();


        startPoint = GameObject.Find(startPoint.name).gameObject;

        endPoint = GameObject.Find(endPoint.name).gameObject;

        try
        {
            startPoint.GetComponent<Collider>().enabled = true;
            endPoint.GetComponent<Collider>().enabled = true;
        }
        catch (NullReferenceException)
        {
            return;
        }

        //Config

        timerDisplay.text = null;
        endPoint.SetActive(false);

        if (!timerOn) timer = Mathf.Infinity;

        rundownTimer += timer; //important! add to insteade of setting. beware of bugs.

        if (checkPoints)  checkpointParent = GameObject.Find(checkpointParent.name).transform;

        if (flagEnable)
        {
            flagObject = GameObject.Find("FlagParent").transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        ObjectiveCompeleteCheck(); //check for quest compeletion every frame
    }

    private void ObjectiveCompeleteCheck()
    {
        //foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        //{
        //    Destroy(go); //>:)
        //}

        if (!startPoint.activeInHierarchy)
        {
            if (questOngoing && !raceStarted) 
            {
                startPoint.SetActive(true);
                return;
            }

            rundownTimer -= Time.deltaTime;

            if (timerOn) 
            {
                TimeSpan ts = TimeSpan.FromSeconds(rundownTimer);
                timerDisplay.text = ts.ToString("mm\\:ss\\:ff"); 
            }

            if (!raceStarted)
            {
                if (checkPoints) LoadCheckPoints();
                else endPoint.SetActive(true);//maybe redundant cuz End point spawn check
                if (flagEnable) SpawnFlag();
            }
            raceStarted = true;
        }

        if (!endPoint.activeInHierarchy && raceStarted && !checkPoints) //Check quest compeletion
        {
            if (flagEnable) //check flag thing
            {
                if (!CarryingFlag()) { endPoint.SetActive(true); return; }
                flagObject.gameObject.SetActive(false);
            }
            FinishQuestObjective();
            Destroy(endPoint.gameObject);
            Destroy(startPoint.gameObject);
            if (checkpointParent == null) return;
            Destroy(checkpointParent);
        }


        if (rundownTimer <= 0) //Check time running out
        {
            ResetQuest();
        }

        if (checkPoints && CheckPointsCompeletion()) //End point spawn check
        {
            if (flagEnable) //check flag thing
            {
                if (!CarryingFlag()) return;
            }

            checkPoints = false;
            endPoint.SetActive(true);
        }

        //if (CarryingFlag() /* && Grapple Condtion through interface or smtg*/) 
        //{ 
        //    DropFlag();
        //}


    }


    private bool CheckPointsCompeletion()
    {
        foreach (Transform chkPts in checkpointParent)
        {
            if (chkPts.gameObject.activeSelf) return false;
        }
        return true;
    }

    private bool CarryingFlag()
    {
        if (flagObject.transform.parent == null)
        {
            return false;
        }
        if (flagEnable)
        {
            if (flagObject.transform.parent.gameObject != GameObject.Find("FlagParent").gameObject)
            {
                return false;
            }
        }
        return true;
    }


    private void SpawnFlag()
    {
        //ToDo check if flag is already active, only 1 flag active at once.
        flagObject.SetActive(true);
        flagObject.transform.parent = null;
        flagObject.transform.position = startPoint.transform.position;
        //ToDo - Instentiate flags at startCoords
    }

    private void DropFlag()
    {
        //Todo - set flag GO parent to null to clear parent
        //Todo - shift this to flag script
    }

    private void ResetQuest()
    {
        rundownTimer = timer;
        raceStarted = false;

        startPoint.SetActive(true);
        endPoint.SetActive(false);

        if (flagEnable) SpawnFlag();

        if(checkpointParent == null) return;
        UnLoadCheckPoints();
    }
    private void LoadCheckPoints()
    {
        foreach (Transform chkPts in checkpointParent) 
        { 
            chkPts.gameObject.SetActive(true);
        }
    }

    private void UnLoadCheckPoints()
    {
        foreach (Transform chkPts in checkpointParent)
        {
            chkPts.gameObject.SetActive(false);
        }
        checkPoints = true;
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
