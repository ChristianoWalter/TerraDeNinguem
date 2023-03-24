using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [Header("Inventory")]
    public Transform itemSlot;
    public Transform itemDetailSlot;
    private GameObject itemDetail;
    public List<ItemInventory> inventory = new List<ItemInventory>();
    public List<GameObject> itemPrefabs = new List<GameObject>();


    protected virtual void Start()
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Inventory")))
        {
            var inventory = PlayerPrefs.GetString("Inventory").Split(';');
            foreach(var nameItem in inventory)
            {
                foreach (var item in itemPrefabs)
                {
                    if(item.name == nameItem)
                    {
                        AddInventoryItem(item);
                    }
                }
            }
        }
    }


    public void ShowItemDetails(GameObject _itemDetail)
    {
        if(itemDetail) Destroy(itemDetail);

        itemDetail = Instantiate(_itemDetail, itemDetailSlot);
    }

    public void AddInventoryItem(GameObject _itemPrefab)
    {
        inventory.Add(Instantiate(_itemPrefab, itemSlot).GetComponent<ItemInventory>());
    }   
    
}
