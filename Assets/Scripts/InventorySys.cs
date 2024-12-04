using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySys : MonoBehaviour
{
    [SerializeField] private int QueueCoun = 4;
    [SerializeField] private int Inv_Size = 48;
    [SerializeField] private int Inv_Max_Size = 48;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private List<GameObject> Slots;
    [SerializeField] private GameObject itempref;
    [SerializeField] private Button chestbutton;
    [SerializeField] private Queue<UnityEngine.Object> Spawnqueue = new Queue<UnityEngine.Object>();
    private List<GameObject> ininventory = new List<GameObject>();
    private int takenspots = 0;
    private void AddRandItem(Transform Parent)
    {
        if (itempref != null && takenspots < Inv_Size)
        {
            if (Spawnqueue.Count < QueueCoun)
            {
                UnityEngine.Object[] items = Resources.LoadAll("Items", typeof(Item));
                for (int i = 0; i < (QueueCoun - Spawnqueue.Count); i++)
                {
                    int randomnum = UnityEngine.Random.Range(0, items.Length);
                    UnityEngine.Object newitem = items[randomnum];
                    Spawnqueue.Enqueue(newitem);
                }
            }

            UnityEngine.Object @object = Spawnqueue.Dequeue();
            GameObject newobject = Instantiate(itempref, Parent);
            ininventory.Add(newobject);
            if (@object != null && @object.GetType() == typeof(Item) && newobject.TryGetComponent<InventoryItem>(out InventoryItem item) == true)
            {
                Item item1 = @object as Item;
                item.currentitem = item1;
            }
            takenspots = ininventory.Count;
        }
    }
    private void OnChestPress()
    {
        takenspots = ininventory.Count;
        if (takenspots < Inv_Size)
        {
            GameObject Randslot;
            do
            {
                int raandompos = UnityEngine.Random.Range(0, Slots.Count);
                Randslot = Slots[raandompos];
            }
            while (Randslot.transform.childCount > 0);
            AddRandItem(Randslot.transform);
        }
    }
    void Start()
    {
        if (Inv_Size > Inv_Max_Size) Inv_Size = Inv_Max_Size;
        if (Prefab != null)
        {
            Slots = new List<GameObject>();
            for (int i = 0; i < Inv_Size; i++)
            {
                GameObject slot = Instantiate(Prefab, transform);
                Slots.Add(slot);
                slot.name = i.ToString();
            }
            int raandompos = UnityEngine.Random.Range(0, Slots.Count);
            GameObject Randslot = Slots[raandompos];
            AddRandItem(Randslot.transform);
        }
        chestbutton.onClick.AddListener(OnChestPress);
    }
    private void FixedUpdate()
    {// checking for deleted items
        for (int i = 0; i < ininventory.Count; i++)
        {
            GameObject go = ininventory[i];
            if (go.IsDestroyed() == true || go == null)
            {
                ininventory.RemoveAt(i);
            }
        }
    }

}
public enum Rarity {
    common=0,
    uncommon=1,
    rare=2,
    epic=3,
    legendary=4,
}