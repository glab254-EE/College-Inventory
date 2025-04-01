using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler
{
    [field:SerializeField] public Item currentitem { get; internal set; } 
    [field: SerializeField] public Image image { get; internal set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySys.instance.ItemAction(currentitem);
        if (currentitem.GetType().Equals(typeof(VoidChest)))
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (image == null) image = GetComponent<Image>();
        if (currentitem != null && currentitem.sprite != null)
        {
            image.sprite = currentitem.sprite;
        }
    }
}
