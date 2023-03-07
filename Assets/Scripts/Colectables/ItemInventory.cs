using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{

    [SerializeField] GameObject itemDetailPrefab;




    public void Inspect()
    {
        Inventory.Instance.ShowItemDetails(itemDetailPrefab);
    }
}
