using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    ExternalInteractions externalInteractions;

    public Transform targetTransform; // Object camera follows
    public Transform cameraPivot; //Pivot Object
    public Transform aimPivot;
    public Transform cameraTransform; //Camera Transform in Scene
    public Transform aimTransform;
    public LayerMask collisionLayers;
    public float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public static float lookSensitivity;
    public float defaultSens;
    public float cameraCollisionOffset;
    public float minimumCollisionOffSet;
    public float cameraCollisionRadius;
    public float cameraFollowSpeed;
    public float cameraLookSpeed;
    public float cameraPivotSpeed;

    public float lookAngle; //Up Down
    public float pivotAngle; // Left Right
    public float minPivotAngle;
    public float maxPivotAngle;

    public GameObject mainCamera;
    public GameObject aimCamera;
    public bool isAiming;
    public GameObject crosshair;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        externalInteractions = GameObject.FindGameObjectWithTag("Player").GetComponent<ExternalInteractions>();
        cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        lookSensitivity = defaultSens;
    }

    public void HandleAllCameraMovement()
    {
        //FollowTarget();
        RotateCamera();
        //HandleCameraCollisions();
        HandleAiming();
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation = Vector3.zero;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * (cameraLookSpeed * lookSensitivity));
        pivotAngle = pivotAngle - (inputManager.cameraInputY * (cameraPivotSpeed * lookSensitivity));
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
        aimPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
    public void HandleAiming()
    {
        if (inputManager.aim_Input && externalInteractions.isInteracting == false)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            crosshair.SetActive(true);
            isAiming = true;
        }
        else
        {
            if (!externalInteractions.isInteracting)
            {
                mainCamera.SetActive(true);
            }
            aimCamera.SetActive(false);
            crosshair.SetActive(false);
            isAiming = false;
        }
    }
}
