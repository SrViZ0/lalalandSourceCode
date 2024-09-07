using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(DestroyOnTrigger))]
public class TutorialTriggerBoxScript : MonoBehaviour
{
    TutorialManager tutoMgmnt;
    [SerializeField] bool addVal;
    bool touched;
    GameObject wayPoint;
    Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        tutoMgmnt = MonoBehaviour.FindAnyObjectByType<TutorialManager>();
        try
        {
            if (transform.childCount > 0) wayPoint = transform.GetChild(0).gameObject;
            wayPoint.SetActive(false);
        }
        catch(NullReferenceException)
        {
            return;
        }
    }

    private void Update()
    {
        if (wayPoint is not null && collider.enabled)
        {
            wayPoint.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            touched = true;
        }
    }
    private void OnDisable()
    {
        if (touched && addVal) 
        {
            tutoMgmnt.textIndex++;
        }
    }


}
