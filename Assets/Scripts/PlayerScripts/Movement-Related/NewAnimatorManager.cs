using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAnimatorManager : MonoBehaviour
{
    private PlayerMovement pm;
    private NewInputManager nim;

    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        pm = GetComponent<PlayerMovement>();
        nim = GetComponent<NewInputManager>();
    }

    void LateUpdate()
    {
        if (pm.hasJumped)
        {
            animator.SetBool("isGrounded", false);
        }
        else
        {
            animator.SetBool("isGrounded", pm.isGrounded);
        }
        animator.SetBool("isJumping", pm.hasJumped);
        animator.SetBool("isVaulting", pm.isVaulting);

        //Jumping
        //if (nim.jump_Input)
        //{
        //    animator.SetBool("canJump", true);
        //}

        //Movement States
        if (!nim.isMoving) //Idle
        {
            animator.SetBool("canMove", false);
        }
        else if (nim.isMoving) //Moving
        {
            animator.SetBool("canMove", true);
        }

        if (nim.sprint_Input && nim.isMoving) //Sprint
        {
            animator.SetBool("canSprint", true);
        }
        else
        {
            animator.SetBool("canSprint", false);
        }

    }
}
