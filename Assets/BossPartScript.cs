using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartScript : MonoBehaviour
{
    [SerializeField] GameObject nextPartCamera;
    [SerializeField] CinemachineVirtualCamera playerCam;
    private void Awake()
    {
        nextPartCamera.SetActive(false);
        playerCam = GameObject.Find("Move Camera").GetComponent<CinemachineVirtualCamera>();
    }
    private void OnDestroy()
    {
        playerCam.enabled = true;
        nextPartCamera.transform.parent.transform.GetChild(0).gameObject.SetActive(true); //get parent's child 0 GO.
        Debug.Log(nextPartCamera.transform.parent.transform.GetChild(0).gameObject + "BX");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextPartCamera.SetActive(true);
            playerCam.enabled = false;
            Debug.Log(nextPartCamera.name + "Bruh");
        }
    }
}
