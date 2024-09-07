using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterActivator : MonoBehaviour
{
    public bool PlayerInRange = false;

    public Transform button;

    public bool isActivated = false;

    public float speed = 1;

    public float lerpTime = 5;

    public float defaultTime;
    public float totalTime;
    public float ToastTime = 4;
    

    public bool on = false;
    public bool haveToast = false;

    public AudioSource Sizzling;

    public ToasterCook cook;

    public ParticleSystem Fire1;
    public ParticleSystem Fire2;
    public ParticleSystem SmokeRelease;

    public GameObject ToastEggplant;
    public GameObject ToastPencil;
    public GameObject ToastMantis;
    public GameObject ToastPumpkin;
    public GameObject ToastBear; 
    public GameObject ToastKamenTank;

    [Header("Left Slot")]
    public bool ToastEggplant1 = false;
    public bool ToastPencil1 = false;
    public bool ToastMantis1 = false;
    public bool ToastPumpkin1 = false;
    public bool ToastBear1 = false;
    public bool ToastKamenTank1 = false;

    [Header("Right Slot")]
    public bool ToastEggplant2 = false;
    public bool ToastPencil2 = false;
    public bool ToastMantis2 = false;
    public bool ToastPumpkin2 = false;
    public bool ToastBear2 = false;
    public bool ToastKamenTank2 = false;

    [Header("Purge Slot")]
    public Transform toastSpawnPointL;
    public Transform toastSpawnPointR;

    public bool enemyPresentL = false;
    public bool enemyPresentR = false;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = defaultTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isActivated)
            {
                //1.4f
                cook.isCooking = true;
                isActivated = false;
                
                button.transform.localPosition = new Vector3(0.00019f, 0.06137f, -0.01239f);

                on = true;

                Fire1.Play();
                Fire2.Play();

                Sizzling.Play();
            }
 
        }

        if (on)
        {
            ToasterCookTimer();
            SpawnToastTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;
            isActivated = true;

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

    void ToasterCookTimer()
    {
        if (0 <= totalTime && on)
        {
            totalTime -= Time.deltaTime;

            
        }
        else
        {
            totalTime = 0;
            if (totalTime == 0)
            {
                SpawnToastTimer();

                cook.ThrowForce();
                cook.isCooking = false;
                totalTime = defaultTime;
                on = false;

                isActivated = true;
                button.transform.localPosition = new Vector3(0.00019f, 0.06137f, -0.00139f);

                Fire1.Stop();
                Fire2.Stop();
                Sizzling.Stop();
                SmokeRelease.Play();
                
            }
        }
    }

    void SpawnToastTimer()
    {
        if (0 <= ToastTime && on)
        {
            ToastTime -= Time.deltaTime;


        }
        else
        {
            ToastTime = 0;
            if (ToastTime == 0)
            {
                ToastTime = 4;
                if (enemyPresentL)
                {
                    //Debug.Log("Spawned");
                    //Instantiate(Toast, toastSpawnPointL.transform.position, Quaternion.identity);
                    //haveToast = true;
                    enemyPresentL = false;
                    if (ToastEggplant1)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastEggplant, toastSpawnPointL.transform.position, Quaternion.identity);
                        ToastEggplant1 = false;
                    }
                    if (ToastPencil1)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastPencil, toastSpawnPointL.transform.position, Quaternion.identity);
                        ToastPencil1 = false;
                    }
                    if (ToastMantis1)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastMantis, toastSpawnPointL.transform.position, Quaternion.identity);
                        ToastMantis1 = false;
                    }
                    if (ToastBear1)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastBear, toastSpawnPointL.transform.position, Quaternion.identity);
                        ToastBear1 = false;
                    }
                    if (ToastPumpkin1)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastPumpkin, toastSpawnPointL.transform.position, Quaternion.identity);
                        ToastPumpkin1 = false;
                    }
                    if (ToastKamenTank1)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastKamenTank, toastSpawnPointL.transform.position, Quaternion.identity);
                        ToastKamenTank1 = false;
                    }
                    
                }
                if (enemyPresentR)
                {
                    //Debug.Log("Spawned");
                    //Instantiate(Toast, toastSpawnPointL.transform.position, Quaternion.identity);
                    //haveToast = true;
                    enemyPresentR = false;
                    if (ToastEggplant2)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastEggplant, toastSpawnPointR.transform.position, Quaternion.identity);
                        ToastEggplant2 = false;
                    }
                    if (ToastPencil2)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastPencil, toastSpawnPointR.transform.position, Quaternion.identity);
                        ToastPencil2 = false;
                    }
                    if (ToastMantis2)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastMantis, toastSpawnPointR.transform.position, Quaternion.identity);
                        ToastMantis2 = false;
                    }
                    if (ToastBear2)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastBear, toastSpawnPointR.transform.position, Quaternion.identity);
                        ToastBear2 = false;
                    }
                    if (ToastPumpkin2)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastPumpkin, toastSpawnPointR.transform.position, Quaternion.identity);
                        ToastPumpkin2 = false;
                    }
                    if (ToastKamenTank2)
                    {
                        Debug.Log("Spawned ToastEggplant");
                        Instantiate(ToastKamenTank, toastSpawnPointR.transform.position, Quaternion.identity);
                        ToastKamenTank2 = false;
                    }
                }
                
            }
        }
    }
}
