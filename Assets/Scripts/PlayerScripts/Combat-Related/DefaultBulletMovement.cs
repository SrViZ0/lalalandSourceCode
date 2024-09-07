using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBulletMovement : DamageScript
{
    public GameObject muzzlePoint;
    public float bulletSpeed;
    Rigidbody rb;
    void Start()
    {
        muzzlePoint = GameObject.Find("ShootPoint");
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * bulletSpeed;
        rb.AddForce(muzzlePoint.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((LayerMask.GetMask("Ground") & 1 << collision.gameObject.layer) > 0 || (LayerMask.GetMask("Default") & 1 << collision.gameObject.layer) > 0)
        {
            this.rb.isKinematic = true;
            this.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 15);
        }

        if (collision.collider.tag == "Enemy")
        {
            DamageTarget(collision.gameObject.GetComponent<DeathChecker>(), collision.gameObject, this.gameObject);
            StunTarget(collision.gameObject.GetComponent<DeathChecker>(), collision.gameObject, this.gameObject);
            Destroy(this.gameObject);
            //Debug.Log("Hit" + this.gameObject);
        }
    }
}
