using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HankHield : DialogMain
{
    public GameObject interactButton;
    public bool inDialog;
    public bool alive;
    public Animator anim;
    [SerializeField] GameObject itemInventoryPrefab;

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetString("RootsDestroyed", "false") == "false")
        {
            alive = true;
        }
        else
        {
            alive = false;
        }

        if (PlayerPrefs.HasKey(gameObject.name))
        {
            index = PlayerPrefs.GetInt(gameObject.name);
        }
    }

    private void Update()
    {
        if (!interactButton) return;
        if (interactButton.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            StartDialog();
        }

        if(!alive)
        {
            index = 2;
        }

        anim.SetBool("alive", alive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !inDialog)
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }

    public override void StartDialog()
    {
        base.StartDialog();
        inDialog = true;
    }

    public override void EndDialog()
    {
        base.EndDialog();
        inDialog = false;
    }

    public override void NextDialog()
    {
        base.NextDialog();
        inDialog = false;

        int currentDialog = PlayerPrefs.GetInt(gameObject.name, 0);
        currentDialog++;
        PlayerPrefs.SetInt(gameObject.name, currentDialog);
    }

    public void GetBunkerKey()
    {
        PlayerPrefs.SetString("BunkerOpen", "true");
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
    }

}
