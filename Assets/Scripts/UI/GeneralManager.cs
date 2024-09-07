using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public GameObject[] managers;
    void Awake()
    {
        for (int i = 0; i < managers.Length; i++) 
        {
            managers[i].gameObject.SetActive(true);
            managers[i].GetComponent<MonoBehaviour>().enabled = true;
            //Debug.Log(managers[i].GetComponent<MonoBehaviour>().name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
