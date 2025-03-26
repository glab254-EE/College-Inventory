using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashTile : ATile
{ 
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData,true);
    }
}