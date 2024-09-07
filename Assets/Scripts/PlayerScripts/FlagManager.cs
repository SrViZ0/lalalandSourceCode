using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private bool dropped;
    // Start is called before the first frame update
    void Start()
    {
       this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent == null)
        {
            dropped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.dropped) 
        { 
            if (other.tag == "Player")
            {
                dropped = false;
                this.transform.parent = GameObject.Find("FlagParent").gameObject.transform;
                this.transform.localPosition = new Vector3(0,0,0);
                this.transform.localEulerAngles = new Vector3 (0,-90,0);
            }
        }
    }
}
