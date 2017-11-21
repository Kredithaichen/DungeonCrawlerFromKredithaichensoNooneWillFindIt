using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IdleState : CharacterState
{
    public IdleState(Character character)
        : base("Idle", character)
    { }

    public override void Update()
    {
    }

    public override void HandleInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                stateManager.ChangeState(character.WalkState);
            else
                stateManager.ChangeState(character.RunState);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            stateManager.ChangeState(character.SidestepState);
    }

    public IdleState(StateManager stateManager, string name) : base(stateManager, name)
    {
    }

    public IdleState(StateManager stateManager) : base(stateManager)
    {
    }
}
