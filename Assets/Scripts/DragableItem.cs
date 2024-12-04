using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour , IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public Transform New_Parent { get; internal set; }
    protected Image image;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(image==null) image = GetComponent<Image>();
        New_Parent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, eventData.position.y,0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent (New_Parent);
        image.raycastTarget = true;
    }
}
