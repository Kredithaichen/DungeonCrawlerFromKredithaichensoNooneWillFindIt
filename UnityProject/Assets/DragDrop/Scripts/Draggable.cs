using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private DropZone homeDropZone;

    [HideInInspector]
    public Transform parentToReturnTo;
    [HideInInspector]
    public Transform placeHolderParent;

    private GameObject placeHolder;

    public DropZone HomeDropZone
    {
        get { return homeDropZone; }
        set { homeDropZone = value; }
    }

    void Start()
    {
        homeDropZone = GetComponentInParent<DropZone>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(homeDropZone.MoveSpace.transform); // transform.parent

        var layoutElement = placeHolder.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        layoutElement.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        layoutElement.flexibleWidth = 0;
        layoutElement.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentToReturnTo = transform.parent;
        placeHolderParent = parentToReturnTo;
        transform.SetParent(homeDropZone.MoveSpace.transform.parent); // transform.parent.parent

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        //if (placeHolder.transform.parent != placeHolderParent)
        //    placeHolder.transform.SetParent(placeHolderParent);

        if (placeHolderParent.GetComponent<DropZone>() != null)
        {
            var newSiblingIndex = placeHolderParent.childCount;

            for (int i = 0; i < placeHolderParent.childCount; i++)
            {
                if (transform.position.x < placeHolderParent.GetChild(i).position.x && transform.position.y < placeHolderParent.GetChild(i).position.y)
                {
                    newSiblingIndex = i;
    
                    if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;
                    
                    break;
                }
            }
    
            placeHolder.transform.SetSiblingIndex(newSiblingIndex);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }
}
