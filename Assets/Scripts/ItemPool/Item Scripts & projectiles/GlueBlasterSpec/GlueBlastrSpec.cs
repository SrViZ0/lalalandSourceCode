using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class GlueBlastrSpec : MonoBehaviour
{
    public Material[] chargeMaterial;
    public float duration, timer;
    public KeyCode shootKey;
    public GameObject glueSprayPrefab;
    private void Awake()
    {
        NullCheck();
    }
    private void OnEnable()
    {
        timer = 0.01f;
    }

    private void OnDisable()
    {
        timer = 0.01f;
    }
    private void Update()
    {
        CheckActive();
        if (timer > 0) { timer -= Time.deltaTime; ShootInput(); }
    }
    public void CheckActive()
    {
        if (!NullCheck())
        {
            return;
        }


        if (this.transform.parent.GetComponent<WeaponScript>().GlueBlasterSpec())
        {
            if (!CountDown()) 
            { timer = duration; }
        }

        if (!this.transform.parent.gameObject.activeInHierarchy)
        {
            if (!CountDown())
            { timer = 0.01f; }
        }
    }
    private bool NullCheck()
    {
        if (this.transform.parent.GetComponent<WeaponScript>() != null)
        {
            this.GetComponent<GlueBlastrSpec>().enabled = true;
            return true;
        }
        this.GetComponent<GlueBlastrSpec>().enabled = false;
        return false;
    }
    public bool CountDown()
    {   
        if (!NullCheck())
        {
            return false;
        }
        bool returnVal = timer <= 0? returnVal = false : returnVal = true; //if timer is less than or equals 0, return false, else return true
        return returnVal;
    }

    private void ShootInput()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Spraying");
            //ToDo - Shoot here blah blah blah
            GameObject balls = Instantiate(glueSprayPrefab, this.transform.parent.GetComponent<WeaponScript>().shootPoint.transform.position, this.transform.parent.GetComponent<WeaponScript>().shootPoint.transform.rotation);
            Destroy(balls,4f);
        }
    }
}
