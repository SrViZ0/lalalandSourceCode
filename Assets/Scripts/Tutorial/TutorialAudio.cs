using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip playingVoiceLine;
    public AudioClip[] voiceLines;

    public int lastNum;

    public TutorialManager tutorialManager;

    private void Update()
    {
        playingVoiceLine = voiceLines[tutorialManager.textIndex];
        audioSource.clip = playingVoiceLine;
        if (lastNum != tutorialManager.textIndex)
        audioSource.Play();
        lastNum = tutorialManager.textIndex;
;    }

}
