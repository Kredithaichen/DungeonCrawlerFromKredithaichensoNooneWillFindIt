using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private GameObject elementPrefab;
    [SerializeField]
    private GameObject elementsParent;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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

    public void SetTitle(string text)
    {
        title.text = text;
    }

    public void UpdateUI(PlayerCharacter player, ItemContainer itemContainer)
    {
        for (int i = 0; i < elementsParent.transform.childCount; i++)
            Destroy(elementsParent.transform.GetChild(i).gameObject);

        foreach (var item in itemContainer.Collection.Items)
            AddItem(item, player, itemContainer);
    }

    private void AddItem(Item item, PlayerCharacter player, ItemContainer itemContainer)
    {
        if (item == null)
            return;

        var obj = Instantiate(elementPrefab);
        obj.transform.SetParent(elementsParent.transform, false);
        obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { player.TakeItemOutOfContainer(item); UpdateUI(player, itemContainer); });
        obj.transform.GetChild(1).GetComponent<Image>().sprite = item.Icon;
        obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.ItemName;
    }
}
