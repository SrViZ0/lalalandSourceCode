using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInputManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public PlayerMovement playerMovement;
    //public AnimatorManager animatorManager;
    public NewCameraManager newcameraManager;
    public NewHealthSystem newhealthSystem;
    public WeaponScript weaponEquip;
    public Transform invSlots;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float zoomDamping;
    public bool aim_Input;
    public bool shoot_Input;

    public bool isMoving;

    public bool sprint_Input;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool b_input;
    public bool jump_Input;
    public bool vault_Input;

    private void Start()
    {

    }
    private void Awake()
    {
        //animatorManager = GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
        newhealthSystem = GetComponent<NewHealthSystem>();
        weaponEquip = GameObject.Find("InvSlots").GetComponent<WeaponScript>();
    }

    private void OnEnable()
    {
        if (playerControls == null && !uiManager.isPausing)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Aim.performed += i => aim_Input = true;
            playerControls.PlayerActions.Aim.canceled += i => aim_Input = false;

            playerControls.PlayerActions.Shoot.performed += i => shoot_Input = true;

            playerControls.PlayerActions.Sprinting.performed += i => sprint_Input = true;
            playerControls.PlayerActions.Sprinting.canceled += i => sprint_Input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

            playerControls.PlayerActions.Jump.performed += i => vault_Input = true;
        }

        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        //HandleSprintingInput();
        HandleJumpingInput();
        HandleVaultingInput();
        HandleShootingInput();
        //HandleZooming();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        if (verticalInput != 0f || horizontalInput != 0f)
            isMoving = true;
        else
            isMoving = false;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        //animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isSprinting);
    }
    private void HandleVaultingInput()
    {
        if (vault_Input)
        {
            playerMovement.HandleVaulting();
        }
    }
    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            //Debug.Log("Jumped");
            jump_Input = false;
            playerMovement.HandleJumping();
        }
        else
        {
            jump_Input = false;
        }
    }
    private void HandleShootingInput()
    {
        foreach (Transform child in invSlots)
        {
            if (child.gameObject.activeSelf)
            {
                weaponEquip = child.GetComponent<WeaponScript>();
            }
        }

        if (shoot_Input && aim_Input)
        {
            shoot_Input = false;
            weaponEquip.HandleShooting();
        }
        else
        {
            shoot_Input = false;
        }
    }
}