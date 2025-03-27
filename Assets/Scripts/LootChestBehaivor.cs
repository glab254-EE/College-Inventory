using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootChestBehaivor : MonoBehaviour
{
    private Button button;
    void Start()
    {
        if (!gameObject.TryGetComponent<Button>(out button)) Destroy(gameObject);
        button.onClick.AddListener(OnClick);
    }
    void OnClick(){
        InventorySys.instance.AddItemInRandomPos();
    }
}
