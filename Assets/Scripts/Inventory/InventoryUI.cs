using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;

    [SerializeField] Color notCollected, collected;

    [SerializeField] SOPickup chalice;
    [SerializeField] SOPickup candleabra;
    [SerializeField] SOPickup mirror;

    [SerializeField] Image chaliceSprite;
    [SerializeField] Image candleabraSprite;
    [SerializeField] Image mirrorSprite;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CheckInventory();
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Inventory.Instance.PrintInventory();
        }
    }

    private void CheckInventory()
    {
        chaliceSprite.color = Inventory.Instance.IsInInventory(chalice) ? collected : notCollected;
        candleabraSprite.color = Inventory.Instance.IsInInventory(candleabra) ? collected : notCollected;
        mirrorSprite.color = Inventory.Instance.IsInInventory(mirror) ? collected : notCollected;
    }
}
