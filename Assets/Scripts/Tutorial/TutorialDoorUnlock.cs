using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoorUnlock : MonoBehaviour
{
    public bool canUnlock;

    // Update is called once per frame
    void Update()
    {
        if (canUnlock)
        {
            transform.rotation = Quaternion.Euler(-90, 90, -5.5f);
        }
    }
}
