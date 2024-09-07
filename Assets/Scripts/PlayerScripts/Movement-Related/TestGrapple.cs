using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrapple : MonoBehaviour
{
    [Header("References")]
    public GameObject playerObj;
    private PlayerMovement playerMovement;
    public GameObject cam;
    public GameObject muzzleTip;
    public LayerMask grappleable;

    public GameObject grapple;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;
    public int shotsFired;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    public float coolDown1;
    public float coolDown2;
    public float coolDown3;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.R;

    [Header("Tracking")]
    public float valToTrack;



    private bool grappling;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerObj.GetComponent<PlayerMovement>();

        cam = GameObject.Find("ThirdPersonCamera");
        muzzleTip = GameObject.Find("GunTip");

        grapple = GameObject.Find("Grapple");
        lr = grapple.GetComponent<LineRenderer>();

    }

    private void Update()
    {
        if (!isCooldown)
        {
            valToTrack = trackRunTime/grappleFrameTime;
            if (Input.GetKeyDown(grappleKey))
            {
                StartGrapple();
            }
        }
        else
        {
            valToTrack = cdRuntime/cdTime;
        }


        if (grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }

        GrapplingHookTimeSystem();
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, muzzleTip.transform.position);
        }
    }


    private void StartGrapple()
    {
        //if (grapplingCdTimer > 0) return;

        trackRunTime = 0f;

        shotsFired += 1;

        grappling = true;

        playerMovement.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxGrappleDistance, grappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.transform.position + cam.transform.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }
    private void ExecuteGrapple()
    {
        playerMovement.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0)
        {
            highestPointOnArc = overshootYAxis;
        }

        playerMovement.JumpToPosition(grapplePoint, highestPointOnArc);

        if (isCooldown)
        {
            Invoke(nameof(StopGrapple), 1f);
        }

    }
    public void StopGrapple()
    {
        playerMovement.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }

    public bool isCooldown;
    public float cdRuntime;
    public float cdTime;
    public float trackRunTime = 0f;
    public float grappleFrameTime = 5f;
    public void GrapplingHookTimeSystem()
    {

        switch (shotsFired)
        {
            case 1:
                if (isCooldown)
                {
                    trackRunTime = 0f;
                    cdTime = coolDown1;
                    if (cdRuntime < cdTime)
                    {
                        cdRuntime += Time.deltaTime;
                    }
                    else
                    {
                        isCooldown = false;
                        cdRuntime = 0;
                        shotsFired = 0;
                    }
                }
                else
                {
                    if (trackRunTime < grappleFrameTime)
                    {
                        trackRunTime += Time.deltaTime;
                        if (trackRunTime > grappleFrameTime)
                        {
                            isCooldown = true;
                        }
                    }
                }

                break;

            case 2:
                if (isCooldown)
                {
                    trackRunTime = 0f;
                    cdTime = coolDown2;
                    if (cdRuntime < cdTime)
                    {
                        cdRuntime += Time.deltaTime;
                    }
                    else
                    {
                        isCooldown = false;
                        cdRuntime = 0;
                        shotsFired = 0;
                    }
                }
                else
                {
                    if (trackRunTime < grappleFrameTime)
                    {
                        trackRunTime += Time.deltaTime;
                        if (trackRunTime > grappleFrameTime)
                        {
                            isCooldown = true;
                        }
                    }
                }
                break;

            case 3:
                isCooldown = true;
                if (isCooldown)
                {
                    trackRunTime = 0f;
                    cdTime = coolDown3;
                    if (cdRuntime < cdTime)
                    {
                        cdRuntime += Time.deltaTime;
                    }
                    else
                    {
                        isCooldown = false;
                        cdRuntime = 0;
                        shotsFired = 0;
                    }
                }
                break;

        }
    }
}
