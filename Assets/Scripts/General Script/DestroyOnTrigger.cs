using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DestroyOnTrigger : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float delay = 0;
    bool active;
    private void Awake()
    {
        if (target == null)
        {
            target = this.gameObject;
        }
        this.GetComponent<Collider>().isTrigger = true; //I forgor why we need this. can remove if not needed.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            active = true;
        }
    }

    private void Update()
    {
        if (active) 
        { 
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                Destroy(target.gameObject); //I could just destroy and time but eh.
            }
        }
    }
}
