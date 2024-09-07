using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject userInterface;
    public GameObject UIParent;
    PlayerMovement playerMovement;

    [Header("Camera Stuff")]
    public List<GameObject> cameraToEnable = new List<GameObject>();
    public GameObject moveCamera;
    public float stayTime;
    public int activeCamera;
    public bool canLoop;

    [Header("Booleans")]
    public bool hasDialgoue;
    public bool needsInteraction;
    public bool canInteract;

    [Header("Display")]
    public string dialogueText;
    public AudioClip dialogueAudio;

    void Start()
    {
        UIParent = GameObject.Find("User Interface");
        userInterface = GameObject.Find("Main UI");
        playerMovement = GameObject.Find("PlayerParent").GetComponent<PlayerMovement>();
        foreach (GameObject camera in cameraToEnable)
        {
            camera.SetActive(false);
        }
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            moveCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            userInterface.SetActive(false);
            playerMovement.enabled = false;
            UIParent.GetComponent<NewUiManager>().enabled = false;
            PlayerMovement.isCinemachining = true;
            GameObject.Find("PlayerParent").GetComponent<Rigidbody>().velocity = Vector3.zero;
            canLoop = true;
            canInteract = false;
        }

        for (int i = 0; i < cameraToEnable.Count && canLoop == true; i++)
        {
            Debug.Log(i);
            activeCamera = i;
            cameraToEnable[activeCamera].SetActive(true);
            if (cameraToEnable.Count > 1)
            {
                Invoke(nameof(EarlyEnable), stayTime * 0.99f);
            }
            Invoke(nameof(GoNext), stayTime);
            Debug.Log("False");
            canLoop = false;
        }
        if (cameraToEnable.Count == 0)
        {
            ResetCamera();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!needsInteraction)
        {
            if (other.gameObject.tag == "Player" && hasDialgoue)
            {
                moveCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
                userInterface.SetActive(false);
                playerMovement.enabled = false;
                UIParent.GetComponent<NewUiManager>().enabled = false;
                PlayerMovement.isCinemachining = true;
                GameObject.Find("PlayerParent").GetComponent<Rigidbody>().velocity = Vector3.zero;
                CinemachineDialogue.audioToPlay = dialogueAudio;
                CinemachineDialogue.textToDisplay = dialogueText;
                CinemachineDialogue.playDialogue = true;
                canLoop = true;
            }
            else if (other.gameObject.tag == "Player" && !hasDialgoue)
            {
                moveCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
                userInterface.SetActive(false);
                playerMovement.enabled = false;
                UIParent.GetComponent<NewUiManager>().enabled = false;
                PlayerMovement.isCinemachining = true;
                GameObject.Find("PlayerParent").GetComponent<Rigidbody>().velocity = Vector3.zero;
                canLoop = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && needsInteraction && canLoop == false)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && needsInteraction)
        {
            canInteract = false;
        }
    }
    private void EarlyEnable()
    {
        cameraToEnable[activeCamera + 1].SetActive(true);
    }
    private void GoNext()
    {
        cameraToEnable[0].SetActive(false);
        cameraToEnable.Remove(cameraToEnable[0]);
        canLoop = true;
        Debug.Log("True");
    }
    private void ResetCamera()
    {
        moveCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        userInterface.SetActive(true);
        playerMovement.enabled = true;
        UIParent.GetComponent<NewUiManager>().enabled = true;
        PlayerMovement.isCinemachining = false;
        Destroy(gameObject);
    }
}
