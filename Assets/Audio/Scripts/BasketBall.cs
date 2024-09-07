using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    public Rigidbody rb;

    public float neededForce;
    public AudioSource audioSource;

    public AudioClip bounce1;
    public AudioClip bounce2;
    public AudioClip bounce3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > neededForce)
        {
            float random = Random.Range(1, 4);
            switch (random)
            {
                case 1:
                    audioSource.PlayOneShot(bounce1);
                    break;
                case 2:
                    audioSource.PlayOneShot(bounce2);
                    break;
                case 3:
                    audioSource.PlayOneShot(bounce3);
                    break;

            }
        }
    }
}
