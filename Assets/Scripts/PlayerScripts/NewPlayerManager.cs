using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerManager : MonoBehaviour
{
    Animator animator;

    NewInputManager newinputManager;
    NewCameraManager newcameraManager;
    PlayerMovement playerMovement;

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        newinputManager = GetComponent<NewInputManager>();
        newcameraManager = FindObjectOfType<NewCameraManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        newinputManager.HandleAllInputs();

    }
    private void FixedUpdate()
    {

        //playerMovement.HandleAllMovement();

    }

    private void LateUpdate()
    { 
        newcameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        playerMovement.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMovement.isGrounded);
    }
}
