using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public GameObject player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public CameraStyle currentStyle;

    public NewInputManager newinputManager;
    public enum CameraStyle
    {
        Basic,
        Combat,
    }

    private void Start()
    {
        newinputManager = player.GetComponent<NewInputManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector3 viewDir = player.transform.position - new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (currentStyle == CameraStyle.Combat)
        { 
            Vector3 dirToCombatLookAt = combatLookAt.transform.position - new Vector3(transform.position.x, combatLookAt.transform.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }

    }
}
