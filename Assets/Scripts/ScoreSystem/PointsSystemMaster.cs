using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystemMaster : MonoBehaviour, IAddPoints, IAddMultiplier
{
    public int points;
    public float maxAddtionalMulti;
    private float baseMultiplier;
    private float multiplier;
    [SerializeField] private PointsInfoSO[] pointsInfoPool;
    private float rundownTimer;
    private float timer;

    public TextMeshProUGUI hubPointsTracker;
    public TextMeshProUGUI playerPointsTracker;
    public Slider pointSlider;

    private Dictionary<string, Points> pointsMap;

    private void OnEnable()
    {
        GameEventsManager.instance.pointsEvent.onMultiplierIncrement += ModifyMultipliers;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.pointsEvent.onMultiplierIncrement -= ModifyMultipliers;
    }

    private void Awake()
    {
        baseMultiplier = 1f;
        pointsMap = CreatePointsMap();
    }

    private void ModifyMultipliers(string id) //Base Multiplier
    {
        //Todo - Change Base Multipleir here
        Points pointsMulti = GetPointsObjById(id);
        baseMultiplier += pointsMulti.info.multiplier;
    }

    public void AddPoints(int p)
    {
        points += Mathf.RoundToInt(p * (baseMultiplier+(baseMultiplier * multiplier * 0.751f)));//Formula for calulating score
        timer += 2f;
    }

    public void AddMutipliers(float m)
    {
        multiplier += m;
        if (multiplier > maxAddtionalMulti)
        {
            multiplier = maxAddtionalMulti; //Autocorrect value
        }
    }

    private void Update()
    {
        Debug.Log("Points" + points);
        Debug.Log($"Base {baseMultiplier}x \n Addtional {multiplier}x \n Total {baseMultiplier + (baseMultiplier * multiplier * 0.751f)}x");
        if (timer <= 0 && multiplier > 0)
        {
            RunDownMultiplier();
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (hubPointsTracker != null && playerPointsTracker != null && pointSlider != null)
        hubPointsTracker.text = points.ToString();
        playerPointsTracker.text = " x " + points.ToString();
        pointSlider.value = points;
    }

    private void RunDownMultiplier()
    {
        multiplier -= Time.deltaTime;
        if (multiplier < 0) //prevent negative multi
        {
            multiplier = 0;
        }
    }

    private Dictionary<string, Points> CreatePointsMap()
    {
        Dictionary<string, Points> idToPointsMap = new Dictionary<string, Points>();

        // Create the quest map
        foreach (PointsInfoSO pointsInfo in pointsInfoPool)
        {
            if (idToPointsMap.ContainsKey(pointsInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + pointsInfo.id);
            }
            idToPointsMap.Add(pointsInfo.id, new Points(pointsInfo));
        }
        return idToPointsMap;
    }

    public Points GetPointsObjById(string id)
    {
        Points pointsObj = pointsMap[id];
        if (pointsObj == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return pointsObj;
    }
}
