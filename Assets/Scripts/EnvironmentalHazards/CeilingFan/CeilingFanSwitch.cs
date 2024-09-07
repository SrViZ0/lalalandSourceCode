using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CeilingFanSwitch : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool fanPassive = false;

    public CeilingFan speed;

    public GameObject areaDeath;

    public GameObject switchHinge;

    // Update is called once per frame
    private void Start()
    {
        areaDeath.SetActive(false);
    }
    void Update()
    {
        if (PlayerInRange && !fanPassive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                speed.deadly = false;
                fanPassive = true;
                areaDeath.SetActive(false);

                //switchHinge.transform.localRotation.y(0, -30, 0);
                switchHinge.transform.localRotation = Quaternion.Euler(0, -30, 0);
            }
        }
        else if (PlayerInRange && fanPassive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                speed.deadly = true;
                fanPassive = false;
                areaDeath.SetActive(true);

                switchHinge.transform.localRotation = Quaternion.Euler(0, 50, 0);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = false;
        }
    }

}
