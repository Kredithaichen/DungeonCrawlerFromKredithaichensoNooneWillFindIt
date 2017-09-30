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
