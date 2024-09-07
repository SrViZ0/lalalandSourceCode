using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggplantWTF : MonoBehaviour
{
    public ToasterActivator Activator;

    public EnemyCheckerL Left;
    public EnemyCheckerR Right;

    public WaffleActivator Waffle;

    public GameObject FrzEggplant;

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
            Activator.ToastEggplant1 = true;
            Activator.enemyPresentL = true;

        }
        if (other.gameObject.tag == "ToasterCheckR")
        {
            Activator.ToastEggplant2 = true;
            Activator.enemyPresentR = true;

        }

        if (other.gameObject.name == "WaffleIronHitbox")
        {
            Waffle.Eggplant_W = true;
            Waffle.enemyPresent = true;

        }

        if (other.gameObject.name == "FridgeHazard")
        {
            Instantiate(FrzEggplant, this.transform.position, Quaternion.identity);

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ToasterCheckL")
        {
            Activator.ToastEggplant1 = false;
            Activator.enemyPresentL = true;


        }
        if (other.gameObject.tag == "ToasterCheckR")
        {
            Activator.ToastEggplant2 = false;
            Activator.enemyPresentR = true;

        }

        if (other.gameObject.name == "WaffleIronHitbox")
        {
            Waffle.Eggplant_W = false;
            Waffle.enemyPresent = false;

        }

    }

}
