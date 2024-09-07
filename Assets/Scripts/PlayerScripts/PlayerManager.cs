using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;

    InputManager inputManager;
    CameraManager cameraManager;
    TppCharacterController tppCharacterController;

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        tppCharacterController = GetComponent<TppCharacterController>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

    }
    private void FixedUpdate()
    {

        tppCharacterController.HandleAllMovement();

    }

    private void LateUpdate()
    { 
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        tppCharacterController.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", tppCharacterController.isGrounded);
    }
}
