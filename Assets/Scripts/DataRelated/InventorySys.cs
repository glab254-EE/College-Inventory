using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySys : MonoBehaviour
{
    public static InventorySys instance;
    [SerializeField] private int Inv_Size = 28;
    [SerializeField] private int Inv_Max_Size = 28;
    [SerializeField] private int MaxLineCount = 5;
    [SerializeField] private GameObject SlotPrefab;
    [SerializeField] private GameObject itempref;
    /*
    [SerializeField] private Button chestbutton;
    [SerializeField] private int QueueCoun = 4;
    [SerializeField] private Queue<UnityEngine.Object> Spawnqueue = new Queue<UnityEngine.Object>();*/
    private List<GameObject> ininventory = new();
    private List<GameObject> SlotList;
    internal GameObject[,] _slots;
    internal SavableItem[,] _ItemInSlots;
    private int takenspots = 0;
    internal void AddItem(Vector2Int position)
    {
        if (itempref != null && takenspots < Inv_Size)
        {
            if(position.x>=_slots.GetLength(0) || position.y>=_slots.GetLength(1)) return;
            Transform Parent = _slots[position.x,position.y].transform;
            if (Parent.childCount > 0) return;
            UnityEngine.Object[] items = Resources.LoadAll("Items", typeof(Item));
            int randomnum = UnityEngine.Random.Range(0, items.Length);
            UnityEngine.Object @object = items[randomnum];
            GameObject newobject = Instantiate(itempref, Parent);
            ininventory.Add(newobject);
            if (@object != null && @object.GetType() == typeof(Item) && newobject.TryGetComponent<InventoryItem>(out InventoryItem item) == true)
            {
                Item item1 = @object as Item;
                item.currentitem = item1;
                _ItemInSlots[position.x,position.y] = new(item1.name,new Vector2Int(position.x,position.y));
            }
            takenspots = ininventory.Count;
        }
    }
    internal void AddItem(Vector2Int position, Item item)
    {
        if (itempref != null && takenspots < Inv_Size)
        {
            if(position.x>=_slots.GetLength(0) || position.y>=_slots.GetLength(1)) return;
            Transform Parent = _slots[position.x,position.y].transform;
            if (Parent.childCount > 0) return;
            GameObject newobject = Instantiate(itempref, Parent);
            ininventory.Add(newobject);
            if (newobject.TryGetComponent<InventoryItem>(out InventoryItem _item) == true)
            {
                _item.currentitem = item;
            }
            takenspots = ininventory.Count;
            _ItemInSlots[position.x,position.y] = new(item.name,new Vector2Int(position.x,position.y));
        }
    }
    internal void AddItemInRandomPos()
    {
        takenspots = ininventory.Count;
        if (takenspots < Inv_Size)
        {
            GameObject Randslot;
            do
            {
                int raandompos = UnityEngine.Random.Range(0, SlotList.Count);
                Randslot = SlotList[raandompos];
            }
            while (Randslot.transform.childCount > 0);
            int randomx = UnityEngine.Random.Range(0,_slots.GetLength(0));
            int randomy = UnityEngine.Random.Range(0,_slots.GetLength(1));
            AddItem(new Vector2Int(randomx,randomy));
        }
    }
    internal void AddItemInRandomPos(Item item)
    {
        
        if (takenspots < Inv_Size)
        {
            GameObject Randslot;
            do
            {
                int raandompos = UnityEngine.Random.Range(0, SlotList.Count);
                Randslot = SlotList[raandompos];
            }
            while (Randslot.transform.childCount > 0);
            int randomx = UnityEngine.Random.Range(0,_slots.GetLength(0));
            int randomy = UnityEngine.Random.Range(0,_slots.GetLength(1));
            AddItem(new Vector2Int(randomx,randomy),item);
        }
    }
    private void InventorySetUp()
    {
        SlotList = new List<GameObject>();
        int linecount = 0;
        int currentLine = 0;
        for (int i = 0; i < Inv_Size; i++)
        {
            GameObject slot = Instantiate(SlotPrefab, transform);
            SlotList.Add(slot);
            slot.name = i.ToString();
            if (_slots.GetLength(1) > currentLine && _slots.GetLength(0) > linecount)
            {
                _slots[linecount,currentLine] = slot;
            }
            linecount++;
            if (linecount>=MaxLineCount){
                linecount=0;
                currentLine++;
            }
        }
    }   
    void Awake()
    {
        if (instance != null){
            Destroy(gameObject);
        }
        instance = this;
        int MaxLines = Mathf.CeilToInt(Inv_Max_Size/MaxLineCount);
        _slots = new GameObject[MaxLineCount,MaxLines];
        _ItemInSlots = new SavableItem[MaxLineCount,MaxLines];
    }
    internal bool TryGetItemFromString(string input, out Item output)
    {
        Item[] allitems = Resources.LoadAll<Item>("Items");
        Item founditem = allitems.SingleOrDefault(a=>a.name==input);
        output = null;
        if(allitems.Length > 0 && founditem != default)
        {
            output = founditem;
            return true;
        }
        return false;
    }
    void Start()
    {
        if (Inv_Size > Inv_Max_Size) Inv_Size = Inv_Max_Size; // clamp
        if (SlotPrefab != null)
        {
            InventorySetUp();
            SavableInventory Loadeditems = DataBase.instance.Load();
            if (Loadeditems == null || Loadeditems.Items.Count <=0) return;
            foreach(SavableItem loadedItem in Loadeditems.Items)
            {
                if (loadedItem != null && TryGetItemFromString(loadedItem.Itemname,out Item Found))
                {
                    AddItem(new Vector2Int(loadedItem.posX,loadedItem.posY),Found);
                }
            }
        }
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
    void OnApplicationQuit()
    {
        SavableInventory ListToSave = new();
        foreach (SavableItem item in _ItemInSlots)
        {
            if (item != null)ListToSave.Items.Add(item);
        }
        DataBase.instance.Save(ListToSave);
    }

}
[System.Serializable]
public class SavableInventory // used ONLY for loading and saving.
{   
    public List<SavableItem> Items = new();
}
[System.Serializable]
public class SavableItem // i hate how we forced to do public variables :sob:
{
    public string Itemname = "";
    public int posX = 1;
    public int posY = 1;
    public SavableItem(){}
    public SavableItem(string name)
    {
        Itemname = name;
    }
    public SavableItem(string name,Vector2Int position)
    {
        Itemname = name;
        posX = position.x;
        posY = position.y;
    }
}
public enum Rarity {
    common=0,
    uncommon=1,
    rare=2,
    epic=3,
    legendary=4,
}