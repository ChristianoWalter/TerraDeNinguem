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
            string inventory;
            if (string.IsNullOrEmpty(PlayerPrefs.GetString("Inventory")))
            {
                inventory = itemInventoryPrefab.name;
            }
            else
            {
                inventory = PlayerPrefs.GetString("Inventory") + ";" + itemInventoryPrefab.name;
            }
            PlayerPrefs.SetString("Inventory", inventory);

            PlayerPrefs.SetString(gameObject.name, "true");

            SaveGameController.Instance.SaveGame();

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
                string inventory;
                if (string.IsNullOrEmpty(PlayerPrefs.GetString("Inventory")))
                {
                    inventory = itemInventoryPrefab.name;
                }
                else
                {
                    inventory = PlayerPrefs.GetString("Inventory") + ";" + itemInventoryPrefab.name;
                }
                PlayerPrefs.SetString("Inventory", inventory);

                PlayerPrefs.SetString(gameObject.name, "true");
                Destroy(gameObject);
                break;

            case Tipo.Evidences:
                interactButton.SetActive(true);
                break;

            case Tipo.Masks:
                Masks.Instance.AddInventoryItem(itemInventoryPrefab);
                string mask;
                if (string.IsNullOrEmpty(PlayerPrefs.GetString("Inventory")))
                {
                    mask = itemInventoryPrefab.name;
                }
                else
                {
                    mask = PlayerPrefs.GetString("Inventory") + ";" + itemInventoryPrefab.name;
                }
                PlayerPrefs.SetString("Inventory", mask);

                PlayerPrefs.SetString(gameObject.name, "true");
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
