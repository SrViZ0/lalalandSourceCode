using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float totalTime = 10;

    // Update is called once per frame
    void Update()
    {
        if (totalTime > 0)
        {

            // Subtract elapsed time every frame
            totalTime -= 1 * Time.deltaTime;


        }
        else
        {
            totalTime = 0;

        }

        if (totalTime == 0)
        {

            Destroy(this.gameObject);

        }
    }
}


