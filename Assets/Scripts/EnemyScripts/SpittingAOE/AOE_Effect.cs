using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Effect : MonoBehaviour
{

    public GameObject AOEarea;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(AOEarea, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            Debug.Log("Target was Smelling the Fart!");

        }
    }

}
