using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStateManager : StateManager
{
    public override void ChangeState(State state)
    {
        base.ChangeState(state);

        //var cstate = state as CharacterState;
        //Debug.Log(cstate.Character.CharacterName + " changed state to " + state.Name);
    }

    public override void InterruptCurrentState(State state)
    {
        base.InterruptCurrentState(state);

        //var cstate = state as CharacterState;
        //Debug.Log(cstate.Character.CharacterName + " interrupted state to " + state.Name);
    }

    public void HandleInput()
    {
        if (runningStates.Count > 0)
        {
            var cstate = runningStates.Peek() as CharacterState;

            if (cstate != null)
                cstate.HandleInput();
        }
    }
}
