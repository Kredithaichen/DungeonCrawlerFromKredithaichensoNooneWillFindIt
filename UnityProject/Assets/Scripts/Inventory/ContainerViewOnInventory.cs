using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerViewOnInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject elementPrefab;
    [SerializeField]
    private GameObject elementsParent;

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

        var obj = Instantiate(elementPrefab);
        obj.transform.SetParent(elementsParent.transform, false);
        obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { player.PlaceItemInContainer(item); UpdateUI(player); });
        obj.transform.GetChild(1).GetComponent<Image>().sprite = item.Icon;
        obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.ItemName;
        obj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { player.Character.Inventory.DropItem(item); UpdateUI(player); });
    }

    public void UpdateUI(PlayerCharacter player)
    {
        for (int i = 0; i < elementsParent.transform.childCount; i++)
            Destroy(elementsParent.transform.GetChild(i).gameObject);

        foreach (var item in player.Character.Inventory.Items)
            AddItem(item, player);
    }
}
