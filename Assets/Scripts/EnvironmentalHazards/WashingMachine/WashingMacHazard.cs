using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WashingMacHazard : DamageScript
{
    public bool goWash = false;
    [HideInInspector] NewHealthSystem newHealthSystem;

    [SerializeField]
    float rotateSpeed = 35;


    [SerializeField]
    Vector3 rotationDirection = new Vector3(0f, 0f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<NewHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goWash) 
        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); //Call this to deal dmg 
        }

        if (other.gameObject.tag == "player")
        {
            newHealthSystem.TakeDamage(damage.ConvertTo<int>());
        }
    }

    
}
