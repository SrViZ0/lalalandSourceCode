using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFireSFX : MonoBehaviour
{
    public AudioSource Fire;

    public bool Stove1 = false;
    public bool Stove2 = false;
    public bool Stove3 = false;
    public bool Stove4 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Stove1 || Stove2 || Stove3 || Stove4)
        {
            Fire.Play();
        }
        else if (!Stove1 || !Stove2 || !Stove3 || !Stove4)
        {
            Fire.Stop();
        }
    }
}
