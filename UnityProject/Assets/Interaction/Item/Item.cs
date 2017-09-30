using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Interactable/Item")]
public class Item : ScriptableObject, IStatSource
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string description;

    [SerializeField]
    private Sprite icon;
    [SerializeField]
    protected SkinnedMeshRenderer meshRenderer;

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public SkinnedMeshRenderer MeshRenderer
    {
        get { return meshRenderer; }
        set { meshRenderer = value; }
    }

    public bool SourceIsEqualTo(IStatSource source)
    {
        if (!(source is Item))
            return false;

        var item = source as Item;
        if (item.itemName == itemName)
            return true;

        return false;
    }
}
