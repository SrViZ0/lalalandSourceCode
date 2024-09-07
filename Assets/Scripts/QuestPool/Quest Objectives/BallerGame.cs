using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallerGame : MonoBehaviour
{
    public PointsSystemMaster psm;
    public int rewardPoints;

    public GameObject basketball;

    public int shotsLanded;
    public TextMeshProUGUI shotTracker;

    public float timeToReturn;
    public Transform respawnPosition;
    void Start()
    {
        ReturnToPos();
    }

    // Update is called once per frame
    void Update()
    {
        shotTracker.text = shotsLanded.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Basketball")
        {
            psm.AddPoints(rewardPoints);
            shotsLanded++;
            Debug.Log(psm.points);
            Invoke(nameof(ReturnToPos), timeToReturn);
        }
    }
    public void ReturnToPos()
    {
        basketball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        basketball.transform.position = respawnPosition.position;
    }

}
