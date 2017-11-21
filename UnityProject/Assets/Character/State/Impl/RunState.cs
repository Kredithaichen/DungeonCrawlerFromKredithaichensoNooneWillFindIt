using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunState : CharacterState
{
    public RunState(Character character) 
        : base("Run", character)
    {
    }

    public override void HandleInput()
    {
        var speed = character.Speed;
        var moveSpeed = stats.CurrentMoveSpeed;

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

        character.Speed = Mathf.Clamp(speed, 0, stats.CurrentMoveSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
            stateManager.ChangeState(character.WalkState);

        if (Input.GetKey(KeyCode.Space))
            stateManager.InterruptCurrentState(character.DashState);
    }

    public override void Continue(State previousState)
    {
        base.Continue(previousState);

        character.Speed = character.Stats.CurrentMoveSpeed;
    }

    protected RunState(StateManager stateManager, string name) : base(stateManager, name)
    {
    }

    protected RunState(StateManager stateManager) : base(stateManager)
    {
    }
}
