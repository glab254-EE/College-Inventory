using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Items/Item")]
public class Item : ScriptableObject
{
    [field:SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public Rarity rarity { get; private set; } = Rarity.common;
    [field: SerializeField] public string Itemname { get; private set; }
    [field: SerializeField] public string ItemType { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public List<attribute> attributes { get; private set; }
    public string GetStats()
    {
        string outp = string.Empty;
        if (attributes.Count > 0)
        {

            outp += "Attributes: \n";
            foreach (var attribute in attributes)
            {
                outp += $"{attribute.Name} : {attribute.Ammount.ToString()} \n";
            }
        }
        else outp = "No attributes";
        return outp;
    }
    public string GetClass()
    {
        string outp = $"{rarity.ToString()} {ItemType}";
        return outp;
    }
    public string GetFullDescription()
    {
        string outp = $"\"{Itemname}\"\n";
        outp += $"An {rarity.ToString()} {ItemType}\n";
        outp += $"Sells for: {cost}";
        if (attributes.Count > 0) {

            outp += "Attributes: \n";
            foreach (var attribute in attributes)
            {
                outp += $"{attribute.Name} : {attribute.Ammount.ToString()}";
            } 
        }

        return outp;
    }
}
[Serializable]
public class attribute
{
    public string Name;
    public int Ammount;
}