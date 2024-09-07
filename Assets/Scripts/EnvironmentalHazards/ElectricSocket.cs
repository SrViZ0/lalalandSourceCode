using Unity.VisualScripting;
using UnityEngine;

public class ElectricSocket : DamageScript //Change class inheritance to DamageScript
{
    public NewHealthSystem newHealthSystem; //Replace later health system gonna inherit form Death checker too

    public ParticleSystem Electric;

    public AudioSource electricBuzz;

    

    private void Start()
    {
        newHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<NewHealthSystem>();
    }

    public void Update()
    {
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            newHealthSystem.TakeDamage(damage.ConvertTo<int>());
        }

        if (other.gameObject.tag == "Enemy")
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); //Call this to deal dmg 
            StunTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); // Call this if you want to stun
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Electric.Play();
            electricBuzz.Play();
        }

        if (other.gameObject.tag == "Enemy")
        {
            Electric.Play();
        }
    }

}
