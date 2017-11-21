﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CharacterAnimator))]
public class Character : EditableMonoBehaviour
{
    [SerializeField]
    protected string characterName;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float hSpeed;
    [SerializeField]
    private float turnSpeed = 120f;

    [SerializeField]
    protected Animator animator;
    [SerializeField]
    private CharacterAnimator characterAnimator;

    [SerializeField]
    protected CharacterStateManager stateManager;

    private IdleState idleState;
    private RunState runState;
    private WalkState walkState;
    private SidestepState sidestepState;
    private DashState dashState;

    [SerializeField]
    private SkinnedMeshRenderer targetMesh;

    [SerializeField]
    protected CharacterStats stats;
    [SerializeField]
    protected Inventory inventory;

    [SerializeField]
    protected Interactable focusedObject;

    public delegate void OnDieHandler();
    public delegate void OnNoticeSomethingHandler(Interactable noticedObject);
    public delegate void OnFocussingHandler(Interactable focusedObject);

    public event OnDieHandler OnDie;
    public event OnNoticeSomethingHandler OnNoticeSomething;
    public event OnFocussingHandler OnFocussing;

    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public CharacterStats Stats
    {
        get { return stats; }
        set { stats = value; }
    }

    public Interactable FocusedObject
    {
        get { return focusedObject; }
        set { focusedObject = value; }
    }

    public SkinnedMeshRenderer TargetMesh
    {
        get { return targetMesh; }
        set
        {
            targetMesh = value;
            inventory.EquipmentLoadout.TargetMesh = value;
        }
    }

    public CharacterAnimator CharacterAnimator
    {
        get { return characterAnimator; }
        set { characterAnimator = value; }
    }

    public float HSpeed
    {
        get { return hSpeed; }
        set { hSpeed = value; }
    }

    public CharacterStateManager StateManager
    {
        get { return stateManager; }
        set { stateManager = value; }
    }

    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }

    public IdleState IdleState
    {
        get { return idleState; }
    }

    public RunState RunState
    {
        get { return runState; }
    }

    public WalkState WalkState
    {
        get { return walkState; }
    }

    public SidestepState SidestepState
    {
        get { return sidestepState; }
    }

    public DashState DashState
    {
        get { return dashState; }
    }

    public override void Initialize()
    {
        characterAnimator = GetComponent<CharacterAnimator>();

        stats = GetComponent<CharacterStats>();
        inventory = GetComponent<Inventory>();

        base.Initialize();
    }

    public override void OnStartInitialize()
    {
        stateManager = new CharacterStateManager();
        idleState = new IdleState(this);
        runState = new RunState(this);
        walkState = new WalkState(this);
        sidestepState = new SidestepState(this);
        dashState = new DashState(this);

        stateManager.ChangeState(idleState);
    }

    void Update()
    {
        // update the stats of the character
        stats.UpdateStats(this);

        // handle states
        stateManager.Update();
    }

    public void Turn(float horizontal)
    {
        // turn the character
        transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);

        // play animation along with it
        characterAnimator.UpdateTurningAnimation(horizontal);
    }

    public void ResetRotation()
    {
        var obj = transform.GetChild(0).transform;
        transform.GetChild(0).transform.rotation = new Quaternion(obj.rotation.x, transform.rotation.y, obj.rotation.z, obj.rotation.w);
    }

    protected virtual void UpdateCharacter()
    {
        stats.UpdateStats(this);
    }

    #region Movement

    

    #endregion

    #region StatEffects
    public void AddStatEffect(StatEffect effect, IStatSource source)
    {
        var copy = Instantiate(effect);
        copy.StatSource = source;
        stats.AddStatEffect(copy);
    }

    public void RemoveAllStatEffects(IStatSource source)
    {
        foreach (var statEffect in stats.StatEffects)
        {
            if (statEffect.StatSource.SourceIsEqualTo(source))
                statEffect.Remove = true;
        }
    }

    public void RemoveStatEffect(StatEffect statEffect, IStatSource source)
    {
        if (statEffect.StatSource.SourceIsEqualTo(source))
            statEffect.Remove = true;
    }
    
    #endregion

    public void Die()
    {
        stats.Alive = false;
        animator.SetBool("Alive", false);

        if (OnDie != null)
            OnDie.Invoke();
    }

    #region Items & Equipment

    public virtual void PickUpItem(ItemPickup item)
    {
        inventory.AddItemToInventory(item.Item);
        Destroy(item.gameObject);

        focusedObject = null;
    }

    public virtual void EquipItem(Item item)
    {
        if (!(item is Equipment))
            return;

        inventory.EquipItem(item);
    }

    public virtual void UnequipItem(Item item)
    {
        if (!(item is Equipment))
            return;

        inventory.UnequipItem(item);
    }

    #endregion

    #region Focus

    public void SetFocus(Interactable obj)
    {
        focusedObject = obj;
    }

    public void Unfocus()
    {
        focusedObject = null;
    }

    public void UnfocusThis(Interactable obj)
    {
        if (obj.Equals(focusedObject))
            focusedObject = null;
    }

    #endregion

    #region Trigger

    void OnTriggerEnter(Collider other)
    {
        // check if object is something to interact with
        var obj = other.gameObject.GetComponentInParent<Interactable>();
        if (obj == null)
            return;

        if (other.name == Interactable.NoticeTriggerArea)
        {
            if (OnNoticeSomething != null)
                OnNoticeSomething.Invoke(obj);
        }
        else if (other.name == Interactable.InteractionTriggerArea)
        {
            SetFocus(obj);

            if (OnFocussing != null)
                OnFocussing.Invoke(obj);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // check if object is something to interact with
        var obj = other.gameObject.GetComponentInParent<Interactable>();
        if (obj == null)
            return;

        if (other.name == Interactable.InteractionTriggerArea)
            UnfocusThis(obj);
    }

    void OnTriggerStay(Collider other)
    {
        // check if object is something to interact with
        var obj = other.gameObject.GetComponentInParent<Interactable>();
        if (obj == null)
            return;

        if (other.name == Interactable.InteractionTriggerArea)
        {
            if (focusedObject == null)
            {
                SetFocus(obj);

                if (OnFocussing != null)
                    OnFocussing.Invoke(obj);
            }
        }
    }

    #endregion

    public void KneelDown()
    {
        StopMoving();
        animator.SetBool("Kneeling", true);
    }

    public void StandUp()
    {
        StopMoving();
        animator.SetBool("Kneeling", false);
    }

    public void StopMoving()
    {
        animator.SetFloat("VSpeed", 0f);
        animator.SetFloat("HSpeed", 0f);
        animator.SetBool("TurningRight", false);
        animator.SetBool("TurningLeft", false);
    }
}
