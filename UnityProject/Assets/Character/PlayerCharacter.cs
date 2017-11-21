using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(CameraController))]
public class PlayerCharacter : EditableMonoBehaviour
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private CharacterStats stats;
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private bool allowCameraMovement = true;

    private bool containerUIShown;
    private bool characterUIShown;

    [SerializeField]
    private CharacterViewUIHandler characterViewUIHandler;
    [SerializeField]
    private InventoryUIHandler inventoryUIHandler;

    [SerializeField]
    private ContainerUIHandler containerUIHandler;
    [SerializeField]
    private ContainerViewOnInventory containerViewOnInventory;

    [SerializeField]
    private float dashCoolDown;
    [SerializeField]
    private float currentDashCooldown;

    public Character Character
    {
        get { return character; }
        set { character = value; }
    }

    public override void Initialize()
    {
        // get required components
        character = GetComponent<Character>();
        cameraController = GetComponent<CameraController>();

        stats = character.Stats;

        // handle player death
        character.OnDie += OnDie;

        // get menus
        characterViewUIHandler = GameObject.FindGameObjectWithTag("CharacterViewUI").GetComponent<CharacterViewUIHandler>();
        inventoryUIHandler = GameObject.FindGameObjectWithTag("InventoryUI").GetComponent<InventoryUIHandler>();

        var obj = GameObject.FindGameObjectWithTag("ItemContainerUI");
        containerUIHandler = obj.GetComponent<ContainerUIHandler>();
        containerViewOnInventory = obj.GetComponentInChildren<ContainerViewOnInventory>();

        base.Initialize();
    }

    private void OnDie()
    {
        inventoryUIHandler.Hide();
        characterUIShown = false;

        containerUIHandler.Hide();
        containerUIShown = false;
    }

    void LateUpdate()
    {
        if (allowCameraMovement)
            cameraController.UpdateCameraPosition(transform);
    }

    void Update()
    {
        if (stats.Alive)
        {
            HandleMovement();
            HandleUIInput();
            HandleInteraction();
            HandleUIUpdates();

            character.StateManager.HandleInput();
        }
    }

    void OnGUI()
    {
        if (characterUIShown || containerUIShown)
            return;

        if (character.FocusedObject != null)
        {
            if (character.FocusedObject.IsInteractableOfType<ItemPickup>())
            {
                var pickup = character.FocusedObject.AsInteractableOfType<ItemPickup>();
                var position = Camera.main.WorldToScreenPoint(pickup.transform.position + Vector3.up);
                GUI.Label(new Rect(position.x - 50f, Screen.height - position.y, 300, 100), pickup.Item.ItemName + "\n[Press E to pick up]");
            }
            else if (character.FocusedObject.IsInteractableOfType<ItemContainer>())
            {
                var container = character.FocusedObject.AsInteractableOfType<ItemContainer>();
                var position = Camera.main.WorldToScreenPoint(container.transform.position + Vector3.up);
                GUI.Label(new Rect(position.x - 50f, Screen.height - position.y, 300, 100), container.ContainerName + "\n[Press E to open]");
            }
            else if (character.FocusedObject.IsInteractableOfType<DestructibleContainer>())
            {
                var container = character.FocusedObject.AsInteractableOfType<DestructibleContainer>();
                var position = Camera.main.WorldToScreenPoint(container.transform.position + Vector3.up);
                GUI.Label(new Rect(position.x - 50f, Screen.height - position.y, 300, 100), "[Press E to destroy]");
            }
        }
    }

    private void HandleMovement()
    {
        if (characterUIShown || containerUIShown)
            return;
        /*
        // control player movement
        var speed = 0f;
        var moveSpeed = stats.CurrentMoveSpeed + stats.CurrentDashSpeed;

        var eulerAngle = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        var hSpeed = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            speed += 120f * Time.deltaTime;
            transform.position += (eulerAngle * Vector3.forward) * moveSpeed * Time.deltaTime * character.Speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed += 120f * Time.deltaTime;
            transform.position += (eulerAngle * Vector3.back) * moveSpeed * Time.deltaTime * character.Speed;
        }
        else if (speed > 0f)
            speed -= 10f * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            hSpeed += 120f * Time.deltaTime;
            transform.position += (eulerAngle * Vector3.left) * moveSpeed * Time.deltaTime * character.HSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hSpeed -= 120f * Time.deltaTime;
            transform.position -= (eulerAngle * Vector3.right) * moveSpeed * Time.deltaTime * character.HSpeed;
        }
        else if (Math.Abs(hSpeed) > 0.001f)
            hSpeed -= Mathf.Sign(hSpeed) * 120f * Time.deltaTime;
        else
            hSpeed = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
            character.Speed = Mathf.Clamp(speed, 0, stats.CurrentMoveSpeed / 2);
        else
            character.Speed = Mathf.Clamp(speed, 0, stats.CurrentMoveSpeed);

        character.HSpeed = Mathf.Clamp(hSpeed, -1, 1);

        // dash forward
        if (character.Stats.CurrentDashSpeed > 0f)
            stats.CurrentDashSpeed -= stats.DashReduction * Time.deltaTime;
        else
            stats.CurrentDashSpeed = 0f;

        if (currentDashCooldown > 0f)
            currentDashCooldown -= Time.deltaTime;
        else
            currentDashCooldown = 0f;

        if (Input.GetKey(KeyCode.Space) && Math.Abs(stats.CurrentDashSpeed) < 0.0001f && Math.Abs(currentDashCooldown) < 0.0001f)
        {
            stats.CurrentDashSpeed = stats.FullDashSpeed;
            character.Speed = 0f;
            character.CharacterAnimator.StopMoving();
            character.CharacterAnimator.DoDash();
            currentDashCooldown = dashCoolDown;
        }
        */
        // turn with horizontal mouse axis
        if (allowCameraMovement)
            character.Turn(Input.GetAxis("Mouse X"));
    }

    private void HandleInteraction()
    {
        if (characterUIShown || containerUIShown)
            return;

        var obj = character.FocusedObject;

        // interact with objects
        if (Input.GetKeyDown(KeyCode.E) && obj != null)
        {
            if (obj.IsInteractableOfType<ItemPickup>())
                character.PickUpItem(obj.AsInteractableOfType<ItemPickup>());
            else if (obj.IsInteractableOfType<ItemContainer>())
            {
                OpenContainer(obj.AsInteractableOfType<ItemContainer>());
                character.Speed = 0f;
            }
            else if (obj.IsInteractableOfType<DestructibleContainer>())
            {
                character.CharacterAnimator.BreakSomething();
                StartCoroutine(BrakingCrateAnimationWait(obj));
            }
        }
    }

    private IEnumerator BrakingCrateAnimationWait(Interactable obj)
    {
        var targetPostition = new Vector3(obj.transform.position.x, transform.position.y, obj.transform.position.z);
        transform.LookAt(targetPostition);

        yield return new WaitForSeconds(0.2f);
        obj.AsInteractableOfType<DestructibleContainer>().DestroyObject();
    }

    private void HandleUIInput()
    {
        if (containerUIShown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseContainer();

            if (Input.GetKeyDown(KeyCode.E))
                TakeAllItems();
        }

        if (!containerUIShown)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                characterUIShown = !characterUIShown;

                if (characterUIShown)
                {
                    inventoryUIHandler.Display();
                    inventoryUIHandler.UpdateUI(this);
                    allowCameraMovement = false;
                }
                else
                {
                    inventoryUIHandler.Hide();
                    allowCameraMovement = true; 
                }
            }
        }
    }

    private void HandleUIUpdates()
    {
        if (characterUIShown)
            UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        characterViewUIHandler.UpdateUI(character.Stats.StatEffects);
        characterViewUIHandler.UpdateHealth(character.Stats.CurrentHealth.ToString("####") + "/" + character.Stats.BaseHealth.ToString("####"));
        // ...
    }

    public void TakeAllItems()
    {
        if (character.FocusedObject == null)
            return;

        var container = character.FocusedObject.AsInteractableOfType<ItemContainer>();
        var inventory = character.Inventory;

        while (container.Collection.ItemCount > 0)
        {
            var item = container.Collection.Items[0];
            inventory.AddItemToInventory(item);
            container.Collection.RemoveItemFromCollection(item);
        }

        containerUIHandler.UpdateUI(this, container);
        containerViewOnInventory.UpdateUI(this);
    }

    public void CloseContainer()
    {
        containerUIShown = false;
        containerUIHandler.Hide();
    }

    public void RemoveEquipment(int equipmentSlot)
    {
        var equipment = character.Inventory.EquipmentLoadout.EquippedItems[equipmentSlot];

        character.Inventory.UnequipItem(equipment);
        inventoryUIHandler.UpdateUI(this);
    }

    public void RemoveWeaponFromEqipment(int weaponSlot)
    {
        var loadOut = character.Inventory.EquipmentLoadout;
        Equipment equipment;

        if (weaponSlot == 0)
            equipment = loadOut.EquippedItems[loadOut.PrimaryWeaponSlot];
        else
            equipment = loadOut.EquippedItems[loadOut.SecondaryWeaponSlot];

        character.Inventory.UnequipItem(equipment);
        inventoryUIHandler.UpdateUI(this);
    }

    private void OpenContainer(ItemContainer container)
    {
        containerUIShown = true;
        containerUIHandler.Display();
        containerUIHandler.SetTitle(container.ContainerName);
        containerUIHandler.UpdateUI(this, container);

        containerViewOnInventory.UpdateUI(this);
    }

    public void PlaceItemInContainer(Item item)
    {
        if (character.FocusedObject == null)
            return;

        var container = character.FocusedObject.AsInteractableOfType<ItemContainer>();

        container.Collection.AddItemToCollection(item);
        character.Inventory.RemoveItemFromInventory(item);

        containerUIHandler.UpdateUI(this, container);
    }

    public void TakeItemOutOfContainer(Item item)
    {
        if (character.FocusedObject == null)
            return;

        var container = character.FocusedObject.AsInteractableOfType<ItemContainer>();

        container.Collection.RemoveItemFromCollection(item);
        character.Inventory.AddItemToInventory(item);

        containerUIHandler.UpdateUI(this, container);
        containerViewOnInventory.UpdateUI(this);
    }
}
