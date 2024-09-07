using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilWTF : MonoBehaviour
{
    public ToasterActivator Activator;

    public EnemyCheckerL Left;
    public EnemyCheckerR Right;

    public WaffleActivator Waffle;

    // Start is called before the first frame update
    void Start()
    {
        Activator = GameObject.Find("ToasterButton").GetComponent<ToasterActivator>();

        Left = GameObject.Find("EnemyChecker(L)-Toaster").GetComponent<EnemyCheckerL>();
        Right = GameObject.Find("EnemyChecker(R)-Toaster").GetComponent<EnemyCheckerR>();

        Waffle = GameObject.Find("WaffleButton").GetComponent<WaffleActivator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ToasterCheckL")
        {
            Activator.ToastPencil1 = true;
            Activator.enemyPresentL = true;

        }
        if (other.gameObject.tag == "ToasterCheckR")
        {
            Activator.ToastPencil2 = true;
            Activator.enemyPresentR = true;

        }

        if (other.gameObject.name == "WaffleIronHitbox")
        {
            Waffle.Pencil_W = true;
            Waffle.enemyPresent = true;


        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ToasterCheckL")
        {
            Activator.ToastPencil1 = false;
            Activator.enemyPresentL = true;


        }
        if (other.gameObject.tag == "ToasterCheckR")
        {
            Activator.ToastPencil2 = false;
            Activator.enemyPresentR = true;

        }

        if (other.gameObject.name == "WaffleIronHitbox")
        {
            Waffle.Pencil_W = false;
            Waffle.enemyPresent = false;

        }

    }

}


