using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragEndTile : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool Delete = false;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject drag = eventData.pointerDrag;
            if (drag != null && drag.TryGetComponent<DragableItem>(out DragableItem tempDragItem) == true)
            {
                tempDragItem.New_Parent = transform;
                if (Delete)
                {
                    DestroyImmediate(drag);
                }
            }
        }
    }
}
