using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int limit = 100;
    private int count;

    [SerializeField]
    private GameObject moveSpace;

    public int ItemLimit
    {
        get { return limit; }
        set { limit = value; }
    }

    public int ItemCount
    {
        get { return count; }
        set { count = value; }
    }

    public GameObject MoveSpace
    {
        get { return moveSpace; }
        set { moveSpace = value; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
            d.placeHolderParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeHolderParent == transform)
            d.placeHolderParent = d.parentToReturnTo;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            if (limit > 0 && limit > count)
            {
                d.parentToReturnTo = transform;
                d.HomeDropZone = this;
                count++;
            }
        }
    }
}
