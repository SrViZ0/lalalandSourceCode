using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform Orientation;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float DashForce;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float SlamGravity;
    [SerializeField] private float Gravity;
    [SerializeField] private float JumpForce;
    [Space]
    [SerializeField] public static bool canSlam;
    [SerializeField] public static float BounceForce;
    [SerializeField] public static bool canBounce;
    [Space]
    [SerializeField] private AudioSource dashing;
    [SerializeField] private AudioClip dashSFX;
    [SerializeField] private AudioSource slamming;
    [SerializeField] private AudioClip slamSFX;
    [SerializeField] private ParticleSystem slamVFX;

    public Collider[] slammedEnemies;


    private void Start()
    {
        canSlam = true;
        BounceForce = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Slam();
        SlamAttack();
        MovePlayer();
        Dash();
        MovePlayerCamera();
        Bounce();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere (FeetTransform.position, 0.1f, FloorMask))
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            Vector3 forceToApply = Orientation.forward * DashForce;
            PlayerBody.AddForce(forceToApply, ForceMode.Impulse);
            dashing.PlayOneShot(dashSFX);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            Vector3 forceToApply = -Orientation.right * DashForce;
            PlayerBody.AddForce(forceToApply, ForceMode.Impulse);
            dashing.PlayOneShot(dashSFX);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            Vector3 forceToApply = Orientation.forward * -DashForce;
            PlayerBody.AddForce(forceToApply, ForceMode.Impulse);
            dashing.PlayOneShot(dashSFX);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            Vector3 forceToApply = Orientation.right * DashForce;
            PlayerBody.AddForce(forceToApply, ForceMode.Impulse);
            dashing.PlayOneShot(dashSFX);
        }
    }

    private void Slam()
    {
        if (Input.GetKey(KeyCode.LeftControl) && canSlam)
        {
            PlayerBody.velocity += Vector3.down * SlamGravity * Time.deltaTime;
        }
        else
        {
            PlayerBody.velocity += Vector3.down * Gravity * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            canSlam = true;
        }
    }
    private void SlamAttack()
    {
        if (Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask) && PlayerBody.velocity.y < -20f && canSlam)
        {
            Debug.Log("Slammed");
            slamming.PlayOneShot(slamSFX);
            Instantiate(slamVFX, transform.position, Quaternion.identity);
            AmmoChecker.ammoCount -= 20;
            slammedEnemies = Physics.OverlapSphere(transform.position, 10, 11);
            if (slammedEnemies != null && slammedEnemies.Length > 0)
            {
                foreach (Collider col in slammedEnemies)
                {
                    if (col.GetComponent<Rigidbody>())
                    {
                        col.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    }
                }
            }
            canSlam = false;
        }
    }
    public void Bounce()
    {
        if (canBounce)
        {
            PlayerBody.AddForce(Vector3.up * BounceForce, ForceMode.Impulse);
        }
    }
}
