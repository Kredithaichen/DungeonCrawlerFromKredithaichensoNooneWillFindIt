using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : CharacterState
{
    public WalkState(Character character) 
        : base("Walk", character)
    {
    }

    public override void HandleInput()
    {
        var speed = character.Speed;
        var moveSpeed = stats.CurrentMoveSpeed + stats.CurrentDashSpeed;

        var eulerAngle = Quaternion.Euler(0, character.transform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.W))
        {
            speed += 120f * Time.deltaTime;
            character.transform.position += (eulerAngle * Vector3.forward) * moveSpeed * Time.deltaTime * character.Speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed += 120f * Time.deltaTime;
            character.transform.position += (eulerAngle * Vector3.back) * moveSpeed * Time.deltaTime * character.Speed;
        }
        else if (speed > 0f)
            speed -= 10f * Time.deltaTime;
        else if (Math.Abs(speed) < 0.0001f)
        {
            character.Speed = 0f;
            stateManager.ChangeState(character.IdleState);
        }

        character.Speed = Mathf.Clamp(speed, 0, stats.CurrentMoveSpeed / 2);

        if (!Input.GetKey(KeyCode.LeftShift))
            stateManager.ChangeState(character.RunState);
    }

    public WalkState(StateManager stateManager, string name) : base(stateManager, name)
    {
    }

    public WalkState(StateManager stateManager) : base(stateManager)
    {
    }
}
