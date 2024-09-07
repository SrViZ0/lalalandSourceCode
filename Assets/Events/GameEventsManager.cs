using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public QuestEvents questEvents;
    public ItemEvent itemEvents;
    public PointsEvent pointsEvent;
    public MiscUnlockEvent miscEvent;
    public EnemyEvent enemyEvent;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        questEvents = new QuestEvents();
        itemEvents = new ItemEvent();
        pointsEvent = new PointsEvent();
        miscEvent = new MiscUnlockEvent();
        //enemyEvent = new EnemyEvent();
        
    }
}
