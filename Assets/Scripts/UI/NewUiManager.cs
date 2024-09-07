using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewUiManager : MonoBehaviour
{
    [Header("General Variables")]
    public GameObject backButton;
    public GameObject uiBackDrop;

    [Header("Pause Variables")]
    public KeyCode pauseButton;
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

    [Header("QuestUI Variables")]
    public GameObject tutorialDialogue;
    public GameObject generalManagers;
    public GameObject questPanel;
    public Transform defaultPos;
    public Transform targetPos;
    public float timeToLerp;
    public bool isViewing;

    [Header("HealthUI Variables")]
    public GameObject healthBar;

    [Header("Point Variables")]
    public GameObject points;

    //[Header("Boss Variables")]
    //public GameObject bossObj;
    public GameObject bossUI;

    [Header("Misc")]
    public GameObject interactPrompt;

    [Header("Weapon-related Variables")]
    public GameObject ammoUI;
    public GameObject weaponSlots;
    public GameObject crosshair;

    [Header("Scripts")]
    GameObject player;
    NewInputManager newinputManager;
    ExternalInteractions externalInteractions;
    PlayerMovement playerMovement;

    private void Awake()
    {
        pauseButton = KeyCode.Escape;

        volumeScroll.value = 0.2f;
        sensScroll.value = 0.2f;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
        newinputManager = player.GetComponent<NewInputManager>();
        externalInteractions = player.GetComponent<ExternalInteractions>();
        playerMovement = player.GetComponent<PlayerMovement>();
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        volumeScrollObj.SetActive(false);
        sensScrollObj.SetActive(false);
        dispDropObj.SetActive(false);
        uiBackDrop.SetActive(false);
        controlPanel.SetActive(false);
        generalManagers.SetActive(true);
        healthBar.SetActive(true);
        points.SetActive(true);
        tutorialDialogue.SetActive(true);
        crosshair.SetActive(true);
        
        newinputManager.enabled = true;
        isPausing = false;
        isViewing = false;
        Time.timeScale = 1f;
        timeSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (/*!bossObj.activeInHierarchy || */isPausing)
        //{
        //    bossUI.SetActive(false);
        //}
        //else
        //{
        //    bossUI.SetActive(true);
        //}
        //volumeScroll.value = 
        Time.timeScale = timeSpeed;
#if UNITY_EDITOR
        pauseButton = KeyCode.X;
#endif

        if (isPausing || (externalInteractions.isInteracting && externalInteractions.interactPressed) || playerMovement.freeMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }



        if (Input.GetKeyDown(pauseButton))
        {
            if (!isPausing)
            {
                newinputManager.enabled = false;
                ammoUI.SetActive(false);
                questPanel.SetActive(false);
                bossUI.SetActive(false);
                pausePanel.SetActive(true);
                uiBackDrop.SetActive(true);
                healthBar.SetActive(false);
                weaponSlots.SetActive(false);
                points.SetActive(false);
                tutorialDialogue.SetActive(false);
                crosshair.SetActive(false);
                timeSpeed = 0f;
                isPausing = true;
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
        isInteracting();
        LerpingQuest();

    }
    public void ContinueGame()
    {
        Debug.Log("ContinueGame");
        ammoUI.SetActive(false);
        newinputManager.enabled = true;
        if (BossSpawnScript.bossSpawned)
        {
            bossUI.SetActive(true);
        }
        Debug.Log(BossSpawnScript.bossSpawned);
        questPanel.SetActive(true);
        pausePanel.SetActive(false);
        uiBackDrop.SetActive(false);
        healthBar.SetActive(true);
        weaponSlots.SetActive(true);
        points.SetActive(true);
        tutorialDialogue.SetActive(true);
        crosshair.SetActive(true);
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
    public void EndScreen()
    {
        SceneManager.LoadScene("DeathScene");
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
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void View()
    {
        isViewing = true;
    }
    public void Hide()
    {
        isViewing = false;
    }
    public void isInteracting()
    {
        if (externalInteractions.inRange == true)
        {
            interactPrompt.SetActive(true);
        }
        else
        {
            interactPrompt.SetActive(false);
        }
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
