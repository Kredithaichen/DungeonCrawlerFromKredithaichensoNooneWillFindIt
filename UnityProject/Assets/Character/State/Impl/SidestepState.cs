using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidestepState : CharacterState
{
    public SidestepState(Character character) 
        : base("Sidestep", character)
    {
    }

    public override void HandleInput()
    {
        var eulerAngle = Quaternion.Euler(0, character.transform.eulerAngles.y, 0);
        var moveSpeed = stats.CurrentMoveSpeed;
        var hSpeed = character.HSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            hSpeed += 120f * Time.deltaTime;
            character.transform.position += (eulerAngle * Vector3.left) * moveSpeed * Time.deltaTime * character.HSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hSpeed -= 120f * Time.deltaTime;
            character.transform.position -= (eulerAngle * Vector3.right) * moveSpeed * Time.deltaTime * character.HSpeed;
        }
        else if (Math.Abs(hSpeed) > 0.001f)
            hSpeed -= Mathf.Sign(hSpeed) * 120f * Time.deltaTime;
        else
        {
            character.HSpeed = 0f;
            stateManager.ChangeState(character.IdleState);
        }

        character.HSpeed = Mathf.Clamp(hSpeed, -1, 1);
    }

    public SidestepState(StateManager stateManager, string name) : base(stateManager, name)
    {
    }

    public SidestepState(StateManager stateManager) : base(stateManager)
    {
    }
}
