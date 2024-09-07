using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    public BallerGame ballerGame;

    public bool PlayerInRange;
    public bool isActivated;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ballerGame.ReturnToPos();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = false;
        }
    }
}
