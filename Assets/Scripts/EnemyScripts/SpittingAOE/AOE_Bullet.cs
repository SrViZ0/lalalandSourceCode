using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Bullet : MonoBehaviour
{

    public GameObject AOEarea;
    public Transform AOEcontact;

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);

            Instantiate(AOEarea, AOEcontact.position, AOEcontact.rotation);

            Debug.Log("BULLET BOOM FART");
        }
    }



}
