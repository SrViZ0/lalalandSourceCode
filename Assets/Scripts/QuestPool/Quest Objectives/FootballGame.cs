using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballGame : MonoBehaviour
{
    public PointsSystemMaster psm;
    public int rewardPoints;

    public GameObject football;

    public Transform respawnPosition;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Soccerball")
        {
            psm.AddPoints(rewardPoints);
            ReturnToPos();
            Debug.Log(psm.points);
        }
    }
    public void ReturnToPos()
    {
        football.GetComponent<Rigidbody>().velocity = Vector3.zero;
        football.transform.position = respawnPosition.position;
    }

}
