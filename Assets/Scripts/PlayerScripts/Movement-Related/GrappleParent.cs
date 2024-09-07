using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleParent : MonoBehaviour
{
    public Transform grappleObj;
    public TestGrapple testGrapple;

    [Header("Checks")]
    public GameObject parentObj;
    public GameObject gParentObj;
    public GameObject invSlots;
    void Start()
    {
        invSlots = GameObject.Find("InvSlots");
        grappleObj = gameObject.transform.GetChild(0);
        testGrapple = grappleObj.GetComponent<TestGrapple>();

    }

    void Update()
    {
        parentObj = transform.parent.gameObject;

        if (parentObj != GameObject.Find("HUB Slot 4"))
        {
            gParentObj = transform.parent.transform.parent.gameObject;
        }

        if (gParentObj != invSlots)
        {
            testGrapple.enabled = false;
        }
        else if (gParentObj == invSlots)
        {
            grappleObj.tag = "Grapple";
            testGrapple.enabled = true;
        }
        return;
    }
}
