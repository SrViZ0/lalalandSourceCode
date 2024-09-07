using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltedGlueProjectile : DamageScript
{
    public float Upforce;
    public GameObject hardGluePrefab;
    public GameObject muzzlePoint;
    public float bulletSpeed;
    Rigidbody rb;
    void Start()
    {
        muzzlePoint = GameObject.Find("ShootPoint");
        rb = GetComponent<Rigidbody>();


        rb.AddForce(muzzlePoint.transform.forward * bulletSpeed, ForceMode.Impulse);
        rb.AddForce(muzzlePoint.transform.right * Random.Range(-1,1), ForceMode.Impulse);
        rb.AddForce(muzzlePoint.transform.up * (Upforce+ Random.Range(-2,2)), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 4f);
        this.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if ((LayerMask.GetMask("Ground") & 1 << collision.gameObject.layer) > 0 || (LayerMask.GetMask("Default") & 1 << collision.gameObject.layer) > 0)
        {
            GameObject harderned = Instantiate(hardGluePrefab, this.transform.position, this.transform.localRotation);
            harderned.transform.localScale = this.transform.localScale;
            Destroy(harderned, 30f);

        }


        if (collision.tag == "Enemy")
        {
            DamageTarget(collision.gameObject.GetComponent<DeathChecker>(), collision.gameObject, this.gameObject); //Change this to a Monobehaviour with a IDamagable check
            StunTarget(collision.gameObject.GetComponent<DeathChecker>(), collision.gameObject, this.gameObject);

            Destroy(this.gameObject);
            Debug.Log("Hit" + this.gameObject);
        }
    }
}
