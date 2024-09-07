using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject menuPanel;
    public GameObject mmBG;
    public GameObject MMSettings;
    public GameObject MMControls;

    [Header("Settings")]
    public TMP_Dropdown displayDrop;
    public GameObject volumeScrollObj;
    public Scrollbar volumeScroll;
    public GameObject sensScrollObj;
    public Scrollbar sensScroll;
    public GameObject dispDropObj;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //sceneParent.SetActive(true);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        mmBG.SetActive(false);
        menuPanel.SetActive(true);
        MMSettings.SetActive(false);
        MMControls.SetActive(false);
    }
    public void PlayGame()
    {
        Debug.Log("PlayGame");
        SceneManager.LoadScene("TestScene");
    }
    public void GoMMSettings()
    {
        Debug.Log("GoMMSettings");
        menuPanel.SetActive(false);
        mmBG.SetActive(true);
        MMSettings.SetActive(true);
        volumeScrollObj.SetActive(true);
        sensScrollObj.SetActive(true);
        dispDropObj.SetActive(true);
    }
    public void BackMM()
    {
        MMSettings.SetActive(false);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void ControlsMM()
    {
        MMSettings.SetActive(false);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        MMControls.SetActive(true);
    }
    public void BackControlsMM()
    {
        MMControls.SetActive(false);
        MMSettings.SetActive(true);
        volumeScrollObj.SetActive(true);
        sensScrollObj.SetActive(true);
        dispDropObj.SetActive(true);
    }
    public void UpdateAudio()
    {
        AudioListener.volume = volumeScroll.value;
        Debug.Log("Audio Changed");
    }
    public void UpdateSensitivity()
    {
        NewCameraManager.lookSensitivity = sensScroll.value;
    }
    public void UpdateDisplay()
    {
        if (displayDrop.value == 0)
        {
            Debug.Log("Fullscreen");
        }
        else if (displayDrop.value == 1)
        {
            Debug.Log("Windowed");
        }
        else if (displayDrop.value == 2)
        {
            Debug.Log("Borderless");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
