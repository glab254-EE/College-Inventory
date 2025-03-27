using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ATile : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject drag = eventData.pointerDrag;
            if (drag != null && drag.TryGetComponent<DragableItem>(out DragableItem tempDragItem) == true)
            {
                tempDragItem.New_Parent = transform;
            }
        }
    }
    public virtual void OnDrop(PointerEventData eventData,bool del)
    {
        if (transform.childCount == 0)
        {
            GameObject drag = eventData.pointerDrag;
            if (drag != null && drag.TryGetComponent<DragableItem>(out DragableItem tempDragItem) == true)
            {
                tempDragItem.New_Parent = transform;
                if (del) Destroy(drag);
            }
        }
    }
}