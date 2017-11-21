using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeHolderParent = transform;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeHolderParent == transform)
        {
            d.placeHolderParent = d.parentToReturnTo;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            Destroy(d.gameObject);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
        }
    }
}