using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTrigger : MonoBehaviour
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
                target.SetActive(false);
                active = false;
            }
        }
    }
}
