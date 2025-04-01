using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashTile : ATile
{ 
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
    private void FixedUpdate()
    {
        if (transform.childCount > 0){
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }        
    }
}