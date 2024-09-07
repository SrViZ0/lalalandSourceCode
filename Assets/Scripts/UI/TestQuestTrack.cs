using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestTrack : MonoBehaviour
{
    public static GameObject trackedQuestObj;
    public GameObject waypointParent;

    private void Update()
    {
        foreach (Transform child in waypointParent.transform)
        {
            if (child.gameObject == trackedQuestObj && trackedQuestObj != null)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }

        Debug.Log(trackedQuestObj);
    }
}
