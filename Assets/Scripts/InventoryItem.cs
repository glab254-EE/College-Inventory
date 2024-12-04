using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [field:SerializeField] public Item currentitem { get; internal set; }
    [field: SerializeField] public Image image { get; internal set; }
    void Start()
    {
        if (image == null) image = GetComponent<Image>();
        if (currentitem != null && currentitem.sprite != null)
        {
            image.sprite = currentitem.sprite;
        }
    }
}
