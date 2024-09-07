using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float hookForce = 25f;

    Grappling grappling;
    Rigidbody rb;
    LineRenderer lr;
    public void Initialize(Grappling grappling, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.grappling = grappling;
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        rb.AddForce(transform.forward * hookForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[]
        {
            transform.position, grappling.transform.position
        };

        lr.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.GetMask("Ground") & 1 << other.gameObject.layer) > 0)
        {
            rb.useGravity = false;
            rb.isKinematic = true;

            grappling.StartPull();
        }
    }
}
