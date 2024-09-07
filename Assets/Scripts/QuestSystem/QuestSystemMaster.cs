using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemMaster : MonoBehaviour
{
    //public static QuestSystemMaster instance; // make it accisble to all script
    //public void Awake() => instance = this;// reduandency

    //[Header("Qust List:")]
    //[SerializeField] List<int> activeQuestList = new List<int>(); //selected quest to be active

    //private List<QuestPool> questPool = new List<QuestPool>(); //store all possible quest in a list

    //[SerializeField] public int questCountLimit = 1;

    //[SerializeField] public int numberOfScriptsToAdd = 3;

    //void Start()
    //{
    //    MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
    //    for (int i = 0; i < allScripts.Length; i++)
    //    {
    //        //if (allScripts[i] is Quest[i])
    //        //    questPool.Add(allScripts[i] as QuestPool);


    //    }
    //}
    //void Update()
    //{

    //}

    //public bool SetQuest(bool active, bool compeleted)
    //{
    //    return active;
    //    return compeleted;
    //}

    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;
    public TextMeshProUGUI questText = null;

    private Dictionary<string, Quest> questMap;

    private int compCount = 0;

    public bool questCompeletionDebug;

    public QuestPool questPool;

    List<string> queslogList = new List<string>();


    // quest start requirements here

    //
    private void Awake()
    {
        questMap = CreateQuestMap();
        //Debug.Log(questMap.Count);
        //Debug.Log(questPool.questList.Length);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.updateQuestLog += UpdateQuestLog;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    

    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.updateQuestLog -= UpdateQuestLog;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start()
    {

        foreach (Quest quest in questMap.Values)
        {
            // initialize any loaded quest steps
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // broadcast the initial state of all quests on startup
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }


    }
    private void UpdateQuestLog(string id, bool active) //Update quest log text box
    {


        if (!queslogList.Contains(id))
        {
            queslogList.Add(id);
        }
        if (active == false)
        {
            queslogList.Remove(id);

            if (queslogList.Count < 1)
            {
                questText.text = string.Empty; //Can replace with n quest active or wtv
                return;
            }
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        questText.text = string.Empty; //Disable this to disable the refresh

        foreach (string q in queslogList)
        {
            //Debug.Log(q);
            Quest quest = GetQuestById(q);
            questText.text += "Objective: " + quest.info.displayName + "\n(" + (quest.GetQuestData().questStepIndex) + " / " + quest.info.questStepPrefabs.Count().ToString() + ")\n\n";
        }
    }
    

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        // start true and prove to be false
        bool meetsRequirements = true;
        //TODO - add check here

        // check quest prerequisites for completion

        //foreach (GameObject prerequisiteQuestInfo in quest.info.questRequirement) //alternative
        foreach (QuestInfo prerequisiteQuestInfo in quest.info.questRequirement)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        // loop through ALL quests
        foreach (Quest quest in questMap.Values)
        {
            // if we're now meeting the requirements, switch over to the CAN_START state
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }

        //DEBUG CODE SECTION!! DO NOT FUCKING WORK HERE

        //foreach (Quest quest in questMap.Values)
        //{
        //    if (questCompeletionDebug)
        //    {
        //        Debug.Log(quest.info.itemReward);
        //    }
        //}
        //if (questCompeletionDebug)
        //{
        //    //Quest quest = GetQuestById("TestAlpha");
        //}

    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        compCount = quest.info.questStepPrefabs.Count();

        //questText.text = "Objective: " + quest.info.displayName + "\n(0/5)";
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        // move on to the next step
        quest.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for this quest
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }

        //Debug.Log("Quest Advanced: " + quest);
        compCount--;
        UpdateQuestLog(id, false);
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)  //reward code here
    {
        //GameEventsManager.instance.itemEvents.UnlockItem(quest.info.itemReward.name);

        if (quest.info.itemReward is ItemInfoSO)
        {
            GameEventsManager.instance.itemEvents.UnlockItem(quest.info.itemReward.name);
        }
        if (quest.info.itemReward is PointsInfoSO)
        {
            GameEventsManager.instance.pointsEvent.AddMultiplier(quest.info.itemReward.name); ;
        }
        if (quest.info.itemReward is MiscInfoSO)
        {
           GameEventsManager.instance.miscEvent.UnlockSpec(quest.info.itemReward.name);
        }

    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfo[] allQuests = questPool.questList; //put everything in a array in a SO and load that the array into this array... man. 
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        // Create the quest map
        foreach (QuestInfo questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    public Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];

        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }

        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
            string serializedData = JsonUtility.ToJson(questData);
            // instead, use an actual Save & Load system and write to a file, the cloud, etc..
            PlayerPrefs.SetString(quest.info.id, serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
        }
    }

    private Quest LoadQuest(QuestInfo questInfo)
    {
        Quest quest = null;
        try
        {
            // load quest from saved data
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            // otherwise, initialize a new quest
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
        }
        return quest;
    }
}

