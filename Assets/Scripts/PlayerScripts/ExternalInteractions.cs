using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalInteractions : MonoBehaviour
{
    NewUiManager newUiManager;

    [Header("References")]
    public Canvas uiCanvas;
    public Camera cam;

    [Header("Weapon Display")]
    public GameObject weaponRack;
    public GameObject mainCamera;
    public GameObject weaponRackCamera;

    [Header("Achievement Display")]
    public GameObject achievementRackCamera;

    [Header("Interaction Stuf")]
    public GameObject holderObj;
    public GameObject interactedObject;

    public bool inRange;
    public  bool interactPressed;
    public  bool isInteracting;
    bool intTracker;
    void Start()
    {
        achievementRackCamera.SetActive(false);
        weaponRackCamera.SetActive(false);
        interactedObject = holderObj;
        weaponRack = GameObject.FindGameObjectWithTag("WeaponRack");
    }

    // Update is called once per frame
    void Update()
    {
        intTracker = isInteracting;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!interactPressed)
            {
                interactPressed = true;
                //inRange = false;
                if (isInteracting) 
                {
                    Debug.Log("isInteracting");//Add this to achivemnt UI too
                }
            }
            else
            {
                interactPressed = false;
                isInteracting = false;
                mainCamera.SetActive(true);
                weaponRackCamera.SetActive(false);
                achievementRackCamera.SetActive(false);
                uiCanvas.enabled = true;
            }
        }

        if (interactPressed && isInteracting)
        {
            Cursor.lockState = CursorLockMode.Confined;
            if (interactedObject.name == "WeaponDisplay")
            {
                mainCamera.SetActive(false);
                achievementRackCamera.SetActive(false);
                weaponRackCamera.SetActive(true);
                uiCanvas.enabled = false;
                Debug.Log("WD");
                GameEventsManager.instance.pointsEvent.UnlockSkins();
            }
            else if (interactedObject.name == "AchievementDisplay")
            {
                mainCamera.SetActive(false);
                weaponRackCamera.SetActive(false);
                achievementRackCamera.SetActive(true);
                uiCanvas.enabled = false;
                Debug.Log("AD");
                GameEventsManager.instance.pointsEvent.UnlockSkins();
            }
            cam.cullingMask = LayerMask.GetMask("UI", "Default", "Ground");
        }
        else
        {
            cam.cullingMask = ~14;
        }
    }

    public void PickUpAmmo(ICollisonAmmo iCollisionAmmo,GameObject ammoPickUp)
    {
        iCollisionAmmo.PickUpAmmo(ammoPickUp);
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.name == "WeaponDisplay" || other.gameObject.name == "AchievementDetect")
        //{
        //    interactedObject = other.gameObject;
        //}
        //else
        //{
        //    interactedObject = holderObj;
        //}

        if (other.gameObject.tag == "Ammo")
        {
            foreach (Transform child in GameObject.Find("InvSlots").transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    PickUpAmmo(child.GetComponent<WeaponScript>(), other.gameObject);
                }
            }      
        }

        if (interactedObject.name == "WeaponDisplay" || interactedObject.name == "AchievementDisplay")
        {
            isInteracting = true;
        }



        if (interactedObject.tag == "Interactable" || interactedObject.name == "WeaponDisplay" || interactedObject.name == "AchievementDisplay")
        {
            inRange = true;
        }

        if (other.gameObject.name == "WeaponDisplay" || other.gameObject.name == "AchievementDisplay" || other.gameObject.tag == "Interactable")
        {
            interactedObject = other.gameObject;
        }
        else if (!isInteracting)
        {
            interactedObject = holderObj;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "WeaponDisplay" || other.gameObject.name == "AchievementDisplay")
        {
            isInteracting = false;
            interactPressed = false;
            //Cursor.lockState = CursorLockMode.Locked;
            mainCamera.SetActive(true);
            weaponRackCamera.SetActive(false);
            achievementRackCamera.SetActive(false);
            uiCanvas.enabled = true;

            interactedObject = holderObj;
        }

        if (other.gameObject.tag == "Interactable")
        {
            inRange = false;
            interactPressed = false;
            interactedObject = holderObj;
        }
    }
}
