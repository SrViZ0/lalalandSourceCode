using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public TppCharacterController tppCharacterController;
    public AnimatorManager animatorManager;
    public CameraManager cameraManager;
    public HealthSystem healthSystem;
    public Transform invSlots;
    WeaponScript weaponEquip;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float zoomDamping;
    public bool aim_Input;
    public bool shoot_Input;

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
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        tppCharacterController = GetComponent<TppCharacterController>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        if (playerControls == null && !uiManager.isPausing)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Aim.performed += i => aim_Input = true;
            playerControls.PlayerActions.Aim.canceled += i =>  aim_Input = false;

            playerControls.PlayerActions.Shoot.performed += i => shoot_Input = true;
            playerControls.PlayerActions.Shoot.canceled += i => shoot_Input = false;
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
        HandleSprintingInput();
        HandleJumpingInput();
        HandleVaultingInput();
        HandleShootingInput();
        //HandleZooming();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, tppCharacterController.isSprinting);
    }
    private void HandleSprintingInput()
    {
        if (sprint_Input && moveAmount > 0.5f)
        {
            tppCharacterController.isSprinting = true;
        }
        else
        {
            tppCharacterController.isSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            //Debug.Log("Jumped");
            jump_Input = false;
            tppCharacterController.HandleJumping();
        }
        else
        {
            jump_Input = false;
        }
    }

    private void HandleVaultingInput()
    {
        if (vault_Input)
        {
            tppCharacterController.HandleVaulting();
        }
    }

    private void HandleShootingInput()
    {
        foreach (Transform child in invSlots)
        {
            if(child.gameObject.activeSelf)
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
    //private void HandleZooming()
    //{
    //    float lastScroll = 0;
    //    if (scrollAmount != lastScroll)
    //    {
    //        float finalZooming = scrollAmount / zoomDamping;
    //        CameraManager.defaultPosition = Mathf.Clamp(CameraManager.defaultPosition, -10f, 1f);
    //        CameraManager.defaultPosition += finalZooming;
    //        if (CameraManager.defaultPosition >= 1)
    //        {
    //            CameraManager.defaultPosition = 1;
    //        }
    //    }
    //    lastScroll = scrollAmount;
    //}
}
