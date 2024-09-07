using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBGM : MonoBehaviour
{
    [SerializeField]AudioSource musicSource;

    public AudioClip mainMenuBGM;


    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = mainMenuBGM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
