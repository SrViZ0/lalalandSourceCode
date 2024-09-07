using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class uiManager : MonoBehaviour
{
    [Header("General Variables")]
    public GameObject backButton;
    public GameObject uiBackDrop;

    [Header("Pause Variables")]
    public GameObject pausePanel;
    public static bool isPausing;
    public float timeSpeed;

    [Header("Controls Variables")]
    public GameObject controlPanel;

    [Header("Settings Variables")]
    public GameObject settingsPanel;
    public GameObject volumeScrollObj;
    public Scrollbar volumeScroll;
    public AudioListener audioListener;
    public GameObject sensScrollObj;
    public Scrollbar sensScroll;
    public GameObject dispDropObj;
    public TMP_Dropdown displayDrop;

    //[Header("Menu Settings")]
    //public GameObject

    [Header("MainMenu Variables")]
    public GameObject menuPanel;
    public GameObject mmBG;
    public GameObject sceneParent;
    public GameObject loadingPanel;
    public GameObject MMSettings;
    public GameObject MMControls;
    public Slider loadingSlider;

    [Header("QuestUI Variables")]
    public GameObject generalManagers;
    public GameObject questPanel;
    public Transform defaultPos;
    public Transform targetPos;
    public float timeToLerp;
    public bool isViewing;

    [Header("HealthUI Variables")]
    public GameObject healthBar;

    [Header("End Variables")]
    public GameObject endPanel;

    [Header("Scripts")]
    GameObject player;
    InputManager inputManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputManager = player.GetComponent<InputManager>();
        sceneParent.SetActive(true);
        endPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        uiBackDrop.SetActive(false);
        controlPanel.SetActive(false);
        menuPanel.SetActive(false);
        mmBG.SetActive(false);
        MMSettings.SetActive(false);
        MMControls.SetActive(false);
        generalManagers.SetActive(true);
        healthBar.SetActive(true);
        inputManager.enabled = true;
        isPausing = false;
        isViewing = false;
        Time.timeScale = 1f;
        timeSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //volumeScroll.value = 
        Time.timeScale = timeSpeed;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPausing)
            {
                Cursor.lockState = CursorLockMode.Confined;
                inputManager.enabled = false;
                questPanel.SetActive(false);
                pausePanel.SetActive(true);
                uiBackDrop.SetActive(true);
                healthBar.SetActive(false);
                isPausing = true;
                timeSpeed = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.G)||Input.GetKeyDown(KeyCode.Tab))
        {
            if (isViewing)
            {
                Hide();
            }
            else
            {
                View();
            }
        }
        LerpingQuest();

    }
    public void PlayGame()
    {
        Debug.Log("PlayGame");
        Cursor.lockState = CursorLockMode.Locked;
        inputManager.enabled = true;
        questPanel.SetActive(true);
        sceneParent.SetActive(true);
        menuPanel.SetActive(false);
        uiBackDrop.SetActive(false);
        mmBG.SetActive(false);
        isPausing = false;
        timeSpeed = 1f;
        StartCoroutine(loadLevelAsync());
    }
    IEnumerator loadLevelAsync()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Syafiq's Scene");

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;

            yield return null;
        }
    }
    public void ContinueGame()
    {
        Debug.Log("ContinueGame");
        Cursor.lockState = CursorLockMode.Locked;
        inputManager.enabled = true;
        questPanel.SetActive(true);
        pausePanel.SetActive(false);
        uiBackDrop.SetActive(false);
        healthBar.SetActive(true);
        isPausing = false;
        timeSpeed = 1f;
    }
    public void GoSettings()
    {
        Debug.Log("GoSettings");
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
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
        CameraManager.lookSensitivity = sensScroll.value;
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
    public void MainMenu()
    {
        Debug.Log("MainMenu");
        menuPanel.SetActive(true);
        pausePanel.SetActive(false);
        uiBackDrop.SetActive(false);
        mmBG.SetActive(true);
        sceneParent.SetActive(false);
        inputManager.enabled = false;
    }
    public void EndScreen()
    {
        endPanel.SetActive(true);
        mmBG.SetActive(true);
        sceneParent.SetActive(false);
    }
    public void ESMainMenu()
    {
        endPanel.SetActive(false);
        MainMenu();
    }
    public void ESSettings()
    {
        endPanel.SetActive(false);
        MMSettings.SetActive(true);
    }
    public void ESRespawn()
    {
        endPanel.SetActive(false);
        Debug.Log("PlayGame");
        Cursor.lockState = CursorLockMode.Locked;
        inputManager.enabled = true;
        questPanel.SetActive(true);
        sceneParent.SetActive(true);
        menuPanel.SetActive(false);
        uiBackDrop.SetActive(false);
        mmBG.SetActive(false);
        isPausing = false;
        timeSpeed = 1f;
        StartCoroutine(loadLevelAsync());
    }
    public void GoMMSettings()
    {
        Debug.Log("GoMMSettings");
        menuPanel.SetActive(false);
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
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoControls()
    {
        Debug.Log("GoControls");
        settingsPanel.SetActive(false);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        controlPanel.SetActive(true);
    }
    public void BackSettings()
    {
        Debug.Log("BackSettings");
        settingsPanel.SetActive(false);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void BackControls()
    {
        Debug.Log("BackControls");
        controlPanel.SetActive(false);
        settingsPanel.SetActive(true);
        volumeScrollObj.SetActive(true);
        sensScrollObj.SetActive(true);
        dispDropObj.SetActive(true);
    }
    public void View()
    {
        isViewing = true;
    }
    public void Hide()
    {
        isViewing = false;
    }
    public void LerpingQuest()
    {
        if (isViewing)
        {
            questPanel.transform.position = Vector3.Lerp(questPanel.transform.position, targetPos.position, timeToLerp);
        }
        else
        {
            questPanel.transform.position = Vector3.Lerp(questPanel.transform.position, defaultPos.position, timeToLerp);
        }
    }
}
