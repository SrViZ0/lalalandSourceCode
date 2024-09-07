using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private void OnEnable()
    {
        this.transform.GetChild(0).localEulerAngles = Vector3.zero;
        this.transform.GetChild(0).localEulerAngles += new Vector3 (Random.Range(-80,80), -90f, 0f);
    }

    private void Update()
    {
        //transform.position += new Vector3(;
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<NewHealthSystem>().TakeDamage(3);
        }
    }
}
