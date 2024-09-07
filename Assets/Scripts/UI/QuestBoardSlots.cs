using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestBoardSlots : MonoBehaviour
{
    public Transform stateObject;
    [SerializeField] private Image compIconSlot;

    [SerializeField] private QuestInfo questInfo;
    private Quest quest;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private QuestSystemMaster qsm;
    [SerializeField] private Sprite finish, unfinished;

    [SerializeField] private GameObject questTrackObj;

    public bool isTracking;
    public bool hasWaypoint;

    public TextMeshProUGUI trackText;

    private void Awake()
    {
        isTracking = false;
        Invoke(nameof(FindQuestInfo), 0.5f);
        //Debug.Log(questInfo.id);
    }
    void FindQuestInfo()
    {
        qsm = GameObject.Find("QuestManager").GetComponent<QuestSystemMaster>();
        quest = qsm.GetQuestById(questInfo.id);
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (hasWaypoint)
        {
            if (questTrackObj == null)
            {
                Destroy(trackText.transform.parent.gameObject);
            }
        }
        if (quest != null) 
        { 
            textDisplay.text = questInfo.displayName;
            if (quest.state.Equals(QuestState.FINISHED))
            {
                compIconSlot.sprite = finish;
            }
            else
            {
                compIconSlot.sprite = unfinished;
            }
        }

        if (isTracking && TestQuestTrack.trackedQuestObj != questTrackObj)
        {
            isTracking = false;

            TestQuestTrack.trackedQuestObj = null;
            trackText.text = "Track";

            Debug.Log("Untracking");
        }
    }

    public void TrackQuest()
    {
        if (!isTracking)
        {
            isTracking = true;

            TestQuestTrack.trackedQuestObj = questTrackObj;
            TestQuestTrack.trackedQuestObj.SetActive(true);
            trackText.text = "Untrack";

            Debug.Log("Tracking");
        }
        else if (isTracking)
        {
            isTracking = false;

            TestQuestTrack.trackedQuestObj = null;
            trackText.text = "Track";

            Debug.Log("Untracking");
        }
    }
}
