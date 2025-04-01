using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IDragHandler , IBeginDragHandler, IEndDragHandler
{
    internal Transform New_Parent;
    internal Image image;
    private Camera _camera;
    private float posZ = 90f;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(image==null) image = GetComponent<Image>();
        posZ = transform.position.z;
        New_Parent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newpos = _camera.ScreenToWorldPoint(eventData.position);
        newpos.z = posZ;
        transform.position = newpos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent (New_Parent);
        image.raycastTarget = true;
    }
    private void Start()
    {
        _camera = Camera.main;
    }
}
