using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{

    public enum Tipo
    {
        Inventory,
        Evidences,
        Masks
    }

    public Tipo tipo;

    [SerializeField] GameObject itemDetailPrefab;




    public void Inspect()
    {
        switch (tipo)
        {
            case Tipo.Inventory:
                PlayerItems.Instance.ShowItemDetails(itemDetailPrefab); 
                break;

            case Tipo.Evidences:
                Evidences.Instance.ShowItemDetails(itemDetailPrefab);
                break;

            case Tipo.Masks:
                Masks.Instance.ShowItemDetails(itemDetailPrefab); 
                break;

        }
    }
}
