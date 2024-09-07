using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceAgainstTime : QuestObjective
{
    [Header("Config")]
    [Tooltip("Toggle to enable checkpoints for this quest(not ordered")]
    public bool checkPoints;
    public Transform checkpointParent;
    public float timer;

    [Header("Adds")]
    [Tooltip("Toggle to ban Grappling")]
    public bool noGrapple; //Not done
    [Tooltip("Toggle to enable flag carrying for this quest")]
    public bool flag;
    public GameObject flagPrefab;

    [Header("Required")]
    private bool raceStarted;
    public GameObject startPoint, endPoint;
    private float rundownTimer; //Honestly why is this here?
    private TextMeshProUGUI timerDisplay;

    private void Awake()
    {
    }
    private void OnEnable()
    {
        timerDisplay = GameObject.Find("QuestTimer").GetComponent<TextMeshProUGUI>();


        startPoint = GameObject.Find(startPoint.name).gameObject;

        endPoint = GameObject.Find(endPoint.name).gameObject;

        //Config

        timerDisplay.text = null;
        endPoint.SetActive(false);

        rundownTimer += timer; //important! add to insteade of setting. beware of bugs.
                               //I have no fucking idea what this dose but im too afraid to remove it :<

        if (!checkPoints) return;

        checkpointParent = GameObject.Find(checkpointParent.name).transform;
    }
    private void Start()
    {

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
            rundownTimer -= Time.deltaTime;

            TimeSpan ts = TimeSpan.FromSeconds(rundownTimer);
            timerDisplay.text = ts.ToString("mm\\:ss\\:ff");


            if (!raceStarted)
            {
                if (checkPoints) LoadCheckPoints();
                else endPoint.SetActive(true);
            }
            raceStarted = true;
        }

        if (endPoint == null && raceStarted && !checkPoints) //Check quest compeletion
        {
            Destroy(startPoint.gameObject);
            FinishQuestObjective();
            if (checkpointParent == null) return;
            Destroy(checkpointParent);
        }


        if (rundownTimer <= 0) //Check time running out
        {
            rundownTimer = timer;
            raceStarted = false;
            startPoint.SetActive(true);
            endPoint.SetActive(false);
            if (checkpointParent == null) return;
            UnLoadCheckPoints();
        }

        if (checkPoints && CheckPointsCompeletion())
        {
            checkPoints = false;
            endPoint.SetActive(true);
        }


    }

    private bool CheckPointsCompeletion()
    {
        foreach (Transform chkPts in checkpointParent)
        {
            if (chkPts.gameObject.activeSelf) return false;
        }
        return true;
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
