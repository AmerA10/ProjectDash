using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    List<SOPickup> _inventory;
    List<int> _quantity;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        _inventory = new List<SOPickup>();
        _quantity = new List<int>();
        // TODO - replace this with loading save data when we get to that point
    }

    /*
     *  UseItem - uses inventory item
     *  if item can be used, it is removed from the inventory.
     *  returns true if item can be used.
     *  
     *  Overload - takes in a number of items to remove.
     */
    public bool UseItem(Pickup item)
    {
        int index = _inventory.IndexOf(item.GetData());

        if (index == -1) return false;

        int foundCount = _quantity[index];
        if (foundCount == 1)
        {
            _inventory.RemoveAt(index);
            _quantity.Remove(index);
        }
        else _quantity[index] -= 1;

        return true;
    }
    public bool UseItem(Pickup item, int quantity)
    {
        int index = _inventory.IndexOf(item.GetData());

        if (index == -1) return false;

        int foundCount = _quantity[index];

        if (foundCount < quantity) return false;

        if (foundCount == quantity)
        {
            _inventory.RemoveAt(index);
            _quantity.RemoveAt(index);
        }
        else _quantity[index] -= quantity;

        return true;
    }

    /*
     *  AddItem - adds an item to the inventory
     *  
     *  returns number of Pickup to leave behind.
     */
    public int AddItem(Pickup item)
    {
        int index = _inventory.IndexOf(item.GetData());
        if (index == -1)
        {
            _inventory.Add(item.GetData());
            _quantity.Add(1);
            return 0;
        }

        SOPickup found = _inventory[index];
        int foundCount = _quantity[index];
        int total = foundCount + item.GetCount();
        if (total <= found.stackLimit)
        {
            _quantity[index] = total;
            return 0;
        }
        _quantity[index] = found.stackLimit;
        return Mathf.Abs(found.stackLimit - total);
    }

    /*
     *  SortByName - sorts the list by name
     */
    public void SortByName(bool reverse = false)
    {
        _inventory = _inventory.OrderBy(i => i.name).ToList();
        if (reverse) _inventory.Reverse();
    }

    /*
     * SortByQuantity - sorts the list by quantity
     */
    //public void SortByQuantity(bool reverse = false)
    //{
    //    _inventory = _inventory.OrderBy(i => i.GetCount()).ToList();
    //    if (reverse) _inventory.Reverse();
    //}

    public bool IsInInventory(SOPickup other)
    {
        return _inventory.Find(i => i.itemName.Equals(other.itemName)) != null;
    }

    public void PrintInventory()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            Debug.Log($"Item: {_inventory[i].itemName} ( {_quantity[i]} ) ");
        }
    }
}
