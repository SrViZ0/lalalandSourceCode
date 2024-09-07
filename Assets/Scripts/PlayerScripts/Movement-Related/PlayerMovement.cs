using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{
    public NewPlayerManager newplayerManager;
    public NewInputManager newinputManager;
    public NewAnimatorManager newanimatorManager;
    public WeaponScript weaponScript;
    public TestGrapple testGrapple;

    [Header("Bools")]
    public bool freeMouse;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public bool isSprinting;

    public float groundDrag;
    
    [Header("Jumping")]
    public float jumpForce;
    public bool isJumping;
    public float fallMultiplier;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public bool hasJumped;

    [Header("Coyote")]
    public float runTime;
    public float execTime;

    public float ledgeRunTime;
    public float ledgeExecTime;

    public bool isCoyoteAir;
    public bool isCoyoteLedge;

    [Header("Grappling")]
    public GameObject invSlots;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float maxDistance = 1f;
    public float inAirTimer;
    public float playerHeight;
    public LayerMask ground;
    public bool isGrounded;
    public float rayCastHeightOffSet;

    [Header("Slope Handling")]
    public GameObject playerObj;
    public CapsuleCollider capsuleCollider;
    public PhysicMaterial nonCollision;
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    public bool exitingSlope;

    [Header("Moving Platform")]
    public bool onMoving;
    private RaycastHit platformHit;
    public LayerMask movingPlatform;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public bool onSlope;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        walking,
        sprinting,
        air
    }

    public bool freeze;

    public bool activeGrapple;

    [Header("Camera Stuff")]
    public Transform aimTransform;
    public bool isColliding;
    public static bool isCinemachining;
    public RaycastHit colHit;
    public LayerMask colliderLayers;

    [Header("Vault Variables")]
    public bool isVaulting;
    public Camera vaultCamera;
    public Transform vaultSource;
    public float playerRadius;
    public LayerMask vaultLayer;
    public float vaultForce;

    [Header("Audio Stuff")]
    public AudioSource sfxSource;
    public AudioClip jumpSFX;


    //[Header("References")]
    //public InputManager inputManager;
    //public AnimatorManager animatorManager;
    private void Awake()
    {
        newplayerManager = GetComponent<NewPlayerManager>();
        newanimatorManager = GetComponent<NewAnimatorManager>();
        invSlots = GameObject.Find("InvSlots");
        weaponScript = invSlots.GetComponent<WeaponScript>();
    }
    private void Start()
    {
        capsuleCollider = playerObj.GetComponent<CapsuleCollider>();
        nonCollision = capsuleCollider.material;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //CheckGrounded();
        MyInput();
        //SpeedControl();
        StateHandler();
        //CoyoteTime();
        //CheckCollision();

        if (isGrounded && !activeGrapple)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            hasJumped = true;
        }
    }
    private void FixedUpdate()
    {
        CheckGrounded();
        SpeedControl();
        CoyoteTime();
        CheckCollision();

        MovePlayer();
        Falling();
        OnMoving();

        if (!OnSlope())
        {
            //Debug.Log(OnSlope());
            capsuleCollider.material = nonCollision;
            //jumpForce = 6.5f;
        }
        else
        {
            //Debug.Log(OnSlope());
            //jumpForce = 30f;
            capsuleCollider.material = null;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            freeMouse = true;
        }
        else
        {
            freeMouse = false;
        }
    }

    private void MyInput()
    {
        //WASD Movement
        horizontalInput = newinputManager.horizontalInput;
        verticalInput = newinputManager.verticalInput;

    }

    private void StateHandler()
    {
        //Mode - Freeze
        if (freeze)
        {
            state = MovementState.freeze;
            moveSpeed = 0f;
            rb.velocity = Vector3.zero;
        }
        // Mode - Sprinting
        else if (rb.velocity.magnitude >= 1 && isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        else if (isGrounded)
        {
            state = MovementState.walking;
            isSprinting = false;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if (activeGrapple) return;

        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded && !OnSlope())
        {
            readyToJump = true;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            GetComponent<Collider>().material = nonCollision;
        }
        else if (isGrounded && OnMoving())
        {
            readyToJump = true;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            GetComponent<Collider>().material = null;
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection().normalized * moveSpeed * 13f, ForceMode.Force);
            GetComponent<Collider>().material = nonCollision;
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            GetComponent<Collider>().material = nonCollision;
        }
    }
    private void SpeedControl()
    {
        if (activeGrapple) return;

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f - 0.5f, ground);
        if (isGrounded)
        {
            runTime = 0;
            isCoyoteAir = false;
            ledgeRunTime = 0;
            isCoyoteLedge = false;
            if (hasJumped == true && rb.velocity.y < 0)
            {
                hasJumped = false;
            }
        }
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }

    private void CheckCollision()
    {
        if (Physics.SphereCast(transform.position, 10f, Vector3.forward, out colHit , 3f, colliderLayers))
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
    }
   
    public void HandleJumping()
    {
        if (readyToJump && OnMoving())
        {
            gameObject.transform.parent = null;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            newanimatorManager.animator.SetBool("isJumping", true);
            //animatorManager.PlayTargetAnimation("Jumping", false);
            sfxSource.PlayOneShot(jumpSFX);
            exitingSlope = true;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce * 1.1f, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else if (readyToJump && isGrounded)
        {
            newanimatorManager.animator.SetBool("isJumping", true);
            //animatorManager.PlayTargetAnimation("Jumping", false);
            sfxSource.PlayOneShot(jumpSFX);
            exitingSlope = true;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void CoyoteTime()
    {
        if (rb.velocity.y < 0 && !isGrounded && !isJumping && !hasJumped)
        {
            ledgeRunTime += Time.deltaTime;
            if (ledgeRunTime < ledgeExecTime && Input.GetKeyDown(KeyCode.Space))
            {
                newanimatorManager.animator.SetBool("isJumping", true);
                //animatorManager.PlayTargetAnimation("Jumping", false);
                sfxSource.PlayOneShot(jumpSFX);
                exitingSlope = true;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                rb.AddForce(transform.up * jumpForce/1.2f, ForceMode.Impulse);

                Invoke(nameof(ResetJump), jumpCooldown);

                ledgeRunTime = 0;
            }

        }

    }
        public void CoyoteJump()
    {

    }

    public void Falling()
    {
        if (rb.velocity.y < 0 && !isGrounded)
        {
            if (!newplayerManager.isInteracting && !isGrounded)
            {
                //animatorManager.PlayTargetAnimation("Falling", true);
            }
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
            Debug.Log("Falling");
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool enableMovementOnNextTouch;

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);


    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
        Debug.Log("Set Velocity");

    }
    public void ResetRestrictions()
    {
        activeGrapple = false;
        Debug.Log("Reset Restrictions");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
            GameObject.FindGameObjectWithTag("Grapple").GetComponent<TestGrapple>().StopGrapple();
        }
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    private bool OnMoving()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out platformHit, playerHeight * 0.5f - 0.5f, movingPlatform))
        {
            gameObject.transform.parent = platformHit.transform;
            rb.interpolation = RigidbodyInterpolation.None;
            onMoving = true;
            return true;
        }
        else
        {
            gameObject.transform.parent = null;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            onMoving = false;
            return false;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
            //float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            //return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }
    public void HandleVaulting()
    {
        bool vaultFront;
        if (vaultFront = Physics.Raycast(vaultCamera.transform.position, vaultCamera.transform.forward, out var firstHit, 0.5f, vaultLayer))
        {
            Debug.Log("Vaultable in front");
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            isCoyoteAir = false;
            runTime = 0;
            isCoyoteLedge = false;
            ledgeRunTime = 0;
            isVaulting = true;

            rb.AddForce(transform.up * vaultForce, ForceMode.Impulse);
            rb.useGravity = true;
            newinputManager.vault_Input = false;
        }
        else
        {
            rb.useGravity = true;
            isVaulting = false;
        }
        //if (Physics.Raycast(vaultCamera.transform.position, vaultCamera.transform.forward, out var firstHit, 1f, vaultLayer))
        //{
        //    Debug.Log("Vaultable in front");
        //    if (Physics.Raycast(firstHit.point + (vaultCamera.transform.forward * playerRadius) + (Vector3.up * 10f * playerHeight), Vector3.down, out var secondHit))
        //    {
        //        StartCoroutine(LerpVault(secondHit.point, 0.5f));
        //        newinputManager.vault_Input = false;
        //    }
        //}
    }

    //IEnumerator LerpVault(Vector3 targetPosition, float duration)
    //{
    //    float time = 0;
    //    Vector3 startPosition = transform.position;

    //    while (time < duration)
    //    {
    //        isVaulting = true;
    //        transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = targetPosition;
    //    isVaulting = false;
    //}
}
