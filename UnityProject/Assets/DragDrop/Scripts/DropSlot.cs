using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Draggable currentContent;

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
            d.parentToReturnTo = transform;
            if (currentContent != null)
                currentContent.gameObject.transform.SetParent(currentContent.HomeDropZone.transform);

            currentContent = d;
        }
    }
}