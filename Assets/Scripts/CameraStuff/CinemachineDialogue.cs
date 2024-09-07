using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinemachineDialogue : MonoBehaviour
{
    public AudioSource cmSource;

    public static AudioClip audioToPlay;

    public static string textToDisplay;

    public static bool playDialogue;

    public GameObject dialogueDisplay;
    public TextMeshProUGUI textDisplay;

    void Start()
    {
        cmSource = GetComponent<AudioSource>();
        dialogueDisplay.SetActive(false);
    }

    void Update()
    {
        if (playDialogue)
        {
            cmSource.clip = audioToPlay;
            dialogueDisplay.SetActive(true);
            textDisplay.text = textToDisplay;
            dialogueDisplay.SetActive(true);
            cmSource.Play();
            playDialogue = false;
            Invoke("ResetDialogue", 6f);
        }
    }

    private void ResetDialogue()
    {
        dialogueDisplay.SetActive(false);
        playDialogue = false;
    }
}
