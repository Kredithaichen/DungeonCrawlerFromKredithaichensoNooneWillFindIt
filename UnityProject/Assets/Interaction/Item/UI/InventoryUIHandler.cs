using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject elementPrefab;
    [SerializeField]
    private GameObject elementsParent;
    [SerializeField]
    private GameObject equipmentHandle;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    public void Display()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private void AddItem(Item item, PlayerCharacter player)
    {
        if (item == null)
            return;

        var inventory = player.Character.Inventory;

        var obj = Instantiate(elementPrefab);
        obj.transform.SetParent(elementsParent.transform, false);
        obj.transform.GetChild(1).GetComponent<Image>().sprite = item.Icon;
        obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.ItemName;

        obj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { inventory.DropItem(item); UpdateUI(player); });

        if (item is Equipment)
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { inventory.EquipItem(item); SetEquipment(item as Equipment); UpdateUI(player); });
        else if (item is Consumable)
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { inventory.ConsumeItem(item); UpdateUI(player); });
    }

    public void UpdateUI(PlayerCharacter player)
    {
        for (int i = 0; i < elementsParent.transform.childCount; i++)
            Destroy(elementsParent.transform.GetChild(i).gameObject);

        foreach (var item in player.Character.Inventory.Items)
            AddItem(item, player);

        for (int i = 0; i < equipmentHandle.transform.childCount; i++)
        {
            RemoveEquipment(i);

            var equipment = player.Character.Inventory.EquipmentLoadout.EquippedItems[i];
            if (equipment != null)
                SetEquipment(equipment);
        }
    }

    private void SetEquipment(Equipment equipment)
    {
        var obj = equipmentHandle.transform.GetChild((int) equipment.TargetSlot);

        obj.gameObject.SetActive(true);
        obj.GetChild(0).GetComponent<Image>().sprite = equipment.Icon;
    }

    private void RemoveEquipment(int index)
    {
        var obj = equipmentHandle.transform.GetChild(index);

        obj.gameObject.SetActive(false);
        obj.GetChild(0).GetComponent<Image>().sprite = null;
    }

    public void RemoveEquipment(Equipment equipment)
    {
        RemoveEquipment((int)equipment.TargetSlot);
    }
}
