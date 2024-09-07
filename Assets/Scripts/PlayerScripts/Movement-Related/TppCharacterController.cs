using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TppCharacterController : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;

    Vector3 moveDirection;
    public Transform cameraObject;
    Rigidbody rb;

    [Header("Falling")]
    public float maxDistance = 1f;
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float shootingSpeed;
    public float walkingSpeed;
    public float runningSpeed;
    public float sprintingSpeed;
    public float rotationSpeed;

    [Header("Jump Speeds")]
    public float jumpHeight;
    public float gravityIntensity;


    [Header("Vault Variables")]
    public bool isVaulting;
    public Camera vaultCamera;
    public float playerRadius;
    public float playerHeight;
    public LayerMask vaultLayer;

    [Header("Misc")]
    public Transform aimTransform;

    [Header("Audio Variables")]
    public AudioSource sfxSource;
    public AudioClip jumpSFX;
    public AudioClip runningSfx;

    private void Awake()
    {
        isGrounded = true;
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        if (!isVaulting)
        {
            HandleMovement();
            HandleRotation();
        }
        HandleFallingAndLanding();
        //HandleVaulting();
    }
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (inputManager.aim_Input)
        {
            moveDirection = moveDirection * shootingSpeed;
        }
        else
        {
            if (isSprinting)
            {
                moveDirection = moveDirection * sprintingSpeed;
            }
            else
            {
                if (inputManager.moveAmount >= 0.5f)
                {
                    moveDirection = moveDirection * runningSpeed;
                }
                else
                {
                    moveDirection = moveDirection * walkingSpeed;
                }
            }
        }

        Vector3 movementVelocity = moveDirection;

        if (!isGrounded)
        {
            rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
        }
        else if (isGrounded && !isJumping)
        {
            rb.velocity = movementVelocity;
        }
    }

    private void HandleRotation()
    {
        if (inputManager.aim_Input != true)
        {
            if (isJumping)
                return;

            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
        else
        {
            transform.rotation = aimTransform.rotation;
        }
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPos;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;
        targetPos = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.05f, Vector3.down, out hit, maxDistance, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            Vector3 rayCastHitPoint = hit.point;
            targetPos.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPos;
            }
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);
            sfxSource.PlayOneShot(jumpSFX);
            float jumpingVelocity = Mathf.Sqrt(-2 * -gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            rb.velocity = playerVelocity;
        }
    }
    public void HandleVaulting()
    {
        if (Physics.Raycast(vaultCamera.transform.position, vaultCamera.transform.forward, out var firstHit, 1f, vaultLayer))
        {
            Debug.Log("Vaultable in front");
            if (Physics.Raycast(firstHit.point + (vaultCamera.transform.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight), Vector3.down, out var secondHit, playerHeight))
            {
                StartCoroutine(LerpVault(secondHit.point, 0.5f));
                inputManager.vault_Input = false;
            }
        }
    }

    IEnumerator LerpVault (Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            isVaulting = true;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isVaulting = false;
    }
}
