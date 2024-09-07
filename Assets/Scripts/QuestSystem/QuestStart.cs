using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class QuestStart : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfo questInfoForPoint;

    [Header("Config")]

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;
    public bool questActive = false;

    //debugging & testing purposes
    public bool forceQuestStart = false;
    public bool forceQuestFinish = false;
    public bool forceLogUpdate = false;


    private void Awake()
    {
        questId = questInfoForPoint.id;
    }

    private void Start()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }


    private void QuestStateChange(Quest quest)
    {
        //Debug.Log(quest.info.id + " " + questId);

        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            //Debug.Log(quest.info.id);
            currentQuestState = quest.state;
        }
    }

    
    private void Update()
    {
        if (playerIsNear && currentQuestState.Equals(QuestState.CAN_START)&& !questActive || forceQuestStart) 
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            questActive = true;

            //Debug code
            //Debug.Log("Quest Started: " + questId); 
            forceQuestStart = false;
        }

        else if (playerIsNear && currentQuestState.Equals(QuestState.CAN_FINISH) && questActive || forceQuestFinish)
        {
            this.GetComponent<Collider>().enabled = false;
            playerIsNear = false;
            CallQuestLogUpdate();
            GameEventsManager.instance.questEvents.FinishQuest(questId);

            //Debug code
            //Debug.Log("Quest Finished: " + questId);
            forceQuestFinish = false;
        }

        else if (playerIsNear && questActive && currentQuestState.Equals(QuestState.IN_PROGRESS) || forceLogUpdate) //Update    
        {
            //Debug.Log(0);
            CallQuestLogUpdate();
            //forceLogUpdate = false;
        }
    }

    public void CallQuestLogUpdate()
    {
        GameEventsManager.instance.questEvents.UpdateQuestLog(questId, playerIsNear);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            CallQuestLogUpdate();
        }
    }
}