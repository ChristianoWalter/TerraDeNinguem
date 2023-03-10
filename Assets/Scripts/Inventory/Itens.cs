using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : Colectable
{
    [SerializeField] GameObject itemInventoryPrefab;

    public enum Tipo
    {
        Inventory,
        Evidences,
        Masks
    }

    public Tipo tipo;
    protected override void ActionOnCollected()
    {
        base.ActionOnCollected();
        switch (tipo)
        {
            case Tipo.Inventory:
                PlayerItems.Instance.AddInventoryItem(itemInventoryPrefab);
                break;

            case Tipo.Evidences:
                Evidences.Instance.AddInventoryItem(itemInventoryPrefab);
                break;

            case Tipo.Masks:
                Masks.Instance.AddInventoryItem(itemInventoryPrefab);
                break;
        }

        Destroy(gameObject);
    }
}
