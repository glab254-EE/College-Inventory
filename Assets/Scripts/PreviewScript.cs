using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreviewScript : MonoBehaviour
{
    [SerializeField] protected Image Itemimge;
    [SerializeField] protected TMP_Text ItemName;
    [SerializeField] protected TMP_Text ItemClass;
    [SerializeField] protected TMP_Text ItemAttributes;
    [SerializeField] protected TMP_Text ItemCost;
    InventoryItem currentitem = null;

    private void UpdateGUI()
    {
        if (currentitem != null)
        {
            Item item = currentitem.currentitem;
            Itemimge.sprite = currentitem.image.sprite;
            ItemName.text = item.Itemname;
            ItemClass.text = item.GetClass();
            ItemAttributes.text = item.GetStats();
            ItemCost.text = $"{item.cost} $";
        }
        else
        {
            Itemimge.sprite =null;
            ItemName.text = "Item Name";
            ItemClass.text = "Item Class";
            ItemAttributes.text = "Item Atts";
            ItemCost.text = "x $";
        }
    }
    GraphicRaycaster gr;
    private void Check()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        if (gr == null) gr = transform.parent.gameObject.GetComponent<GraphicRaycaster>();
        PointerEventData pe = new PointerEventData(null);
        pe.position = Input.mousePosition;
        gr.Raycast(pe, results);
        if (results!=null)
        {
            bool found = false;
            foreach (RaycastResult r in results)
            {
                if (r.gameObject != null && r.gameObject.TryGetComponent<InventoryItem>(out InventoryItem item) == true)
                {
                    found = true;
                    currentitem = item;
                    break;
                }
            }
            if (!found) currentitem = null;
        } else
        {
            currentitem = null;
        }

        UpdateGUI();
    }
    public void FixedUpdate()
    {
        Check();
    }
}
