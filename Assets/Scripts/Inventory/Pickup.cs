using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// TODO - implement IInteractable
public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField]
    SOPickup _data;

    [SerializeField]
    SpriteRenderer _sprite;

    public TextMeshPro _text { get; private set; }

    [SerializeField]
    int count { get; set; }

    bool _isVisible = false;

    public Pickup(Pickup other)
    {
        _data = other._data;
        count = other.count;
    }

    private void Awake()
    {
        if (!_data)
        {
            Debug.LogError("Pickup item requires a SOPickup, but it's null!");
            return;
        }
        _text = GetComponentInChildren<TextMeshPro>();
        _text.text = _data.itemName;
        _text.enabled = false;
        _sprite.sprite = _data.sprite;  
    }

    public SOPickup GetData() { return _data; }

    /*
     *  SetVisibility - indicates whether or not this item is visible in the game.
     *  SetShowItemName - same thing, but for the name bar
     *  I'm not entire sure this bool is necessary - might be an easier way to do this.
     *  Basically trying to distinguish inventory visibility vs. world visibility.
     */
    public void SetVisibility()
    {
        _isVisible = !_isVisible;
        ToggleVisibility();
    }
    public void SetVisibility(bool val)
    {
        _isVisible = val;
        ToggleVisibility();
    }
    public void SetShowItemName()
    {
        _text.enabled = !_text.enabled;
    }
    public void SetShowItemName(bool val)
    {
        _text.enabled = val;
    }

    /*
     *  ToggleVisibility - updates the visibility of the sprite
     */
    private void ToggleVisibility()
    {
        if (_sprite.enabled != _isVisible) _sprite.enabled = _isVisible;
    }

    /*
     *  AddToStack - adds a number of items to this stack of items.
     *  returns the number of items that couldn't be added due to stack size limits.
     */
    public int AddToStack(int quantity)
    {
        int newQuantity = count + quantity;
        int remainder = Mathf.Abs(_data.stackLimit - newQuantity);
        count = remainder == 0 ? newQuantity : _data.stackLimit;
        return remainder;
    }

    /*
     * RemoveFromStack - removes a number of items from this stack of items. 
     * returns true if the number requested is <= number in stack.
     */
    public bool RemoveFromStack(int quantity)
    {
        if (quantity <= count)
        {
            count -= quantity;
            return true;
        }

        return false;
    }

    /*
     * GetCount - returns the number of items in this stack
     */
    public int GetCount() { return count; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) return;
        SetShowItemName(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) return;
        SetShowItemName(false);
    }

    // These is meant to be the implementation of the IInteractable
    public void HandleInteraction()
    {
        int remainder = Inventory.Instance.AddItem(this);
        if (remainder > 0) count = remainder;
        else this.gameObject.SetActive(false);
    }

    public bool IsInteractable() { return true; }
}
