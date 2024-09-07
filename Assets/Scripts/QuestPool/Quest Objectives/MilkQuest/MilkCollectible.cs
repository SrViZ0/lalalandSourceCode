using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkCollectible : MonoBehaviour
{
    private Rigidbody rb;
    private MilkCollection mc;

    public string colPosName1;
    public string colPosName2;
    public string colPosName3;

    public AudioSource BottlePlacement;

    public bool isCollected;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        BottlePlacement = GameObject.Find("Collection Spots").GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == colPosName1|| other.gameObject.name == colPosName2|| other.gameObject.name == colPosName3)
        {
            mc = GameObject.Find("MilkQuestPF(Clone)").GetComponent<MilkCollection>();
            gameObject.transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            Destroy(rb);
            Destroy(other.gameObject);
            BottlePlacement.Play();
            isCollected = true;
            mc.ObjectiveCompeleteCheck();
        }
    }
}
