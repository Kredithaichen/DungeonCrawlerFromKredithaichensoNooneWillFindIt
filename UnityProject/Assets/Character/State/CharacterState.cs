using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CharacterState : State
{
    protected Character character;
    protected CharacterStats stats;

    public CharacterState(string name, Character character)
        : base(character.StateManager, name)
    {
        this.character = character;
        stats = character.Stats;
    }

    protected CharacterState(StateManager stateManager, string name) 
        : base(stateManager, name)
    {
    }

    protected CharacterState(StateManager stateManager) 
        : base(stateManager)
    {
    }

    public virtual void HandleInput()
    {
    }

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }
}
