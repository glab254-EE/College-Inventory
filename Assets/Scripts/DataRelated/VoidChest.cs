using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New chest",menuName ="Items/Chest")]
public class VoidChest : Item
{
    [field: SerializeField] public float RemovedItems { get; private set; } = 0.5f;
}