using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Screen")] 
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void Play(string lvltoLoad)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(loadLevelAsync(lvltoLoad));
    }

    IEnumerator loadLevelAsync(string lvltoLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;

            yield return null;
        }
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        //Application.Quit();
        Debug.Log("GAME HAS QUIT");
    }
}
