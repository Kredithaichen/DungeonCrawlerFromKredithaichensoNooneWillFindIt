using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private Character character;

    private float animationSmoothTime = 0.1f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<Character>();
    }

    void Update()
    {
        var speed = character.Speed / character.Stats.CurrentMoveSpeed;
        animator.SetFloat("WalkSpeed", speed, animationSmoothTime, Time.deltaTime);

        if (character.HSpeed > 0f)
        {
            animator.SetFloat("StepLeft", character.HSpeed, animationSmoothTime, Time.deltaTime);
            animator.SetFloat("StepRight", 0f, animationSmoothTime, Time.deltaTime);
            character.Speed = 0f;
        }
        else if (character.HSpeed < 0f)
        {
            animator.SetFloat("StepLeft", 0f, animationSmoothTime, Time.deltaTime);
            animator.SetFloat("StepRight", -character.HSpeed, animationSmoothTime, Time.deltaTime);
            character.Speed = 0f;
        }
        else
        {
            animator.SetFloat("StepLeft", 0f, animationSmoothTime, Time.deltaTime);
            animator.SetFloat("StepRight", 0f, animationSmoothTime, Time.deltaTime);
        }

        character.ResetRotation();
    }

    public void UpdateWalkSpeed()
    {
        
    }

    void LateUpdate()
    {
        // stop braking crates
        if (animator.GetBool("BreakingSomething"))
            animator.SetBool("BreakingSomething", false);

        // stop dashing
        if (animator.GetBool("Dashing"))
            animator.SetBool("Dashing", false);
    }

    public void ConsumeSomething()
    {
        
    }

    public void StopMoving()
    {
        //animator.SetFloat("WalkSpeed", 0f);
    }

    public void DoDash()
    {
        animator.SetBool("Dashing", true);
    }

    public void BreakSomething()
    {
        animator.SetBool("BreakingSomething", true);
    }

    public void UpdateTurningAnimation(float horizontal)
    {
        /*if (horizontal < 0 && animator.GetInteger("CurrentAction") == 0)
        {
            if (Math.Abs(Input.GetAxis("Vertical")) < 0.001f && Math.Abs(Input.GetAxis("Horizontal")) < 0.001f)
                animator.SetBool("TurningLeft", true);
        }
        else
            animator.SetBool("TurningLeft", false);

        if (horizontal > 0 && animator.GetInteger("CurrentAction") == 0)
        {
            if (Math.Abs(Input.GetAxis("Vertical")) < 0.001f && Math.Abs(Input.GetAxis("Horizontal")) < 0.001f)
                animator.SetBool("TurningRight", true);
        }
        else
            animator.SetBool("TurningRight", false);*/
    }
}
