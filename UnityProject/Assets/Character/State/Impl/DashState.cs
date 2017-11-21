using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : CharacterState
{
    private float enterSpeed;

    public DashState(Character character) : base("Dash", character)
    {
    }

    public float EnterSpeed
    {
        get { return enterSpeed; }
        set { enterSpeed = value; }
    }

    public override void Enter()
    {
        stats.CurrentDashSpeed = stats.FullDashSpeed;
        character.CharacterAnimator.StopMoving();
        character.CharacterAnimator.DoDash();
        stats.CurrentDashCooldown = stats.DashCooldown;
    }

    public override void Update()
    {
        if (stats.CurrentDashSpeed > 0f)
        {
            stats.CurrentDashSpeed -= stats.DashReduction * Time.deltaTime;

            var eulerAngle = Quaternion.Euler(0, character.transform.eulerAngles.y, 0);

            if (Input.GetKey(KeyCode.W))
                character.transform.position += (eulerAngle * Vector3.forward) * stats.CurrentDashSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S))
                character.transform.position += (eulerAngle * Vector3.back) * stats.CurrentDashSpeed * Time.deltaTime;
        }
        else
        {
            stats.CurrentDashSpeed = 0f;
            stateManager.ContinueInterruptedState();
        }
    }

    public DashState(StateManager stateManager, string name) : base(stateManager, name)
    {
    }

    public DashState(StateManager stateManager) : base(stateManager)
    {
    }
}
