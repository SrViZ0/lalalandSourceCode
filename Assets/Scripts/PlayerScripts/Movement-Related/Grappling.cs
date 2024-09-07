using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public float pullSpeed = 0.5f;
    public float stopDistance = 4f;
    public GameObject hookPrefab;
    public Transform grappleSource;

    Hook hook;
    public bool pulling;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pulling = false;
    }

    private void Update()
    {
        if (hook == null && Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            pulling = false;
            hook = Instantiate(hookPrefab, grappleSource.transform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Initialize(this, grappleSource);
            StartCoroutine(DestroyHookAfterLifetime());
        }
        else if (hook != null && Input.GetKeyUp(KeyCode.R))
        {
            DestroyHook();
        }

        if (!pulling || hook == null) return;

        if (Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            DestroyHook();
        }
        else
        {
            rb.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
        }
    }

    public void StartPull()
    {
        pulling = true;
    }
    private void DestroyHook()
    {
        if (hook == null)
            return;
        pulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }    

private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(8f);

        DestroyHook();
    }

}
