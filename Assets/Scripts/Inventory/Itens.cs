using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : Colectable
{
    [SerializeField] GameObject itemInventoryPrefab;

    public GameObject interactButton;

    public enum Tipo
    {
        Inventory,
        Evidences,
        Masks
    }

    public Tipo tipo;

    private void Awake()
    {
        if(PlayerPrefs.GetString(gameObject.name, "false") == "false")
        {
            gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (interactButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            Evidences.Instance.AddInventoryItem(itemInventoryPrefab);
            PlayerPrefs.SetString(gameObject.name, "true");
            Destroy(gameObject);
        }
    }

    protected override void ActionOnCollected()
    {
        base.ActionOnCollected();
        switch (tipo)
        {
            case Tipo.Inventory:
                PlayerItems.Instance.AddInventoryItem(itemInventoryPrefab);
                Destroy(gameObject);
                break;

            case Tipo.Evidences:
                interactButton.SetActive(true);
                break;

            case Tipo.Masks:
                Masks.Instance.AddInventoryItem(itemInventoryPrefab);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactButton != null)
            {
                interactButton.SetActive(false);
            }
        }
    }


}
