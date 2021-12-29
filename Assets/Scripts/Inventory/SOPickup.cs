using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item", menuName = "ScriptableObjects/PickupObjects", order = 1)]
public class SOPickup : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite sprite;
    public int stackLimit;
}
