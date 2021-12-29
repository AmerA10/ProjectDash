using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    List<Pickup> _inventory;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        _inventory = new List<Pickup>();
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
        int index = _inventory.IndexOf(item);

        if (index == -1) return false;

        Pickup found = _inventory[index];
        if (found.GetCount() == 1) _inventory.RemoveAt(index);
        else found.RemoveFromStack(1);

        return true;
    }
    public bool UseItem(Pickup item, int quantity)
    {
        int index = _inventory.IndexOf(item);

        if (index == -1) return false;

        Pickup found = _inventory[index];
        int _quantity = found.GetCount();

        if (_quantity < quantity) return false;

        if (_quantity == quantity) _inventory.RemoveAt(index);
        else found.RemoveFromStack(quantity);

        return true;
    }

    /*
     *  AddItem - adds an item to the inventory
     *  
     *  returns number of Pickup to leave behind.
     */
    public int AddItem(Pickup item)
    {
        int index = _inventory.IndexOf(item);
        if (index == -1)
        {
            Pickup newItem = new Pickup(item);
            newItem.SetVisibility(false);
            newItem.SetShowItemName(false);
            _inventory.Add(newItem);
            return 0;
        }

        Pickup found = _inventory[index];
        int numberToAdd = found.GetCount();
        int remainder = _inventory[index].AddToStack(numberToAdd);
        return remainder;
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
    public void SortByQuantity(bool reverse = false)
    {
        _inventory = _inventory.OrderBy(i => i.GetCount()).ToList();
        if (reverse) _inventory.Reverse();
    }
}
