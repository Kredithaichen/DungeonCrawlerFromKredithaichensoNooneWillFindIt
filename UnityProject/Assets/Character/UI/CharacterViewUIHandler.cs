using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterViewUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject elementPrefab;
    [SerializeField]
    private GameObject elementsParent;

    [SerializeField]
    private TextMeshProUGUI healthLabel;
    [SerializeField]
    private TextMeshProUGUI speedLabel;
    [SerializeField]
    private TextMeshProUGUI baseDamageLabel;
    [SerializeField]
    private TextMeshProUGUI weaponDamageLabel;
    [SerializeField]
    private TextMeshProUGUI defenseLabel;

    public void AddStatEffect(StatEffect effect)
    {
        if (effect == null)
            return;

        var obj = Instantiate(elementPrefab);
        obj.transform.SetParent(elementsParent.transform, false);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = effect.Icon;
        obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = effect.StatName;
    }

    public void UpdateUI(List<StatEffect> effects)
    {
        for (int i = 0; i < elementsParent.transform.childCount; i++)
            Destroy(elementsParent.transform.GetChild(i).gameObject);

        foreach (var item in effects)
            AddStatEffect(item);
    }

    public void UpdateHealth(string text)
    {
        if (healthLabel != null)
            healthLabel.text = text;
    }
}
