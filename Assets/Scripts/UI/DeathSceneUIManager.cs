using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DeathSceneUIManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        Debug.Log("PlayGame");
        SceneManager.LoadScene("TestScene");
    }
    public void GoMainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
