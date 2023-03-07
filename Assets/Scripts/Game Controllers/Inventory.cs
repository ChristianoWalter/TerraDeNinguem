using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public enum Screens
    {
        Notes,
        Inventory,
        Evidences,
        Masks,
        InGame
    }

    public Screens currentScreen;

    [SerializeField] GameObject screensObject;

    [SerializeField] GameObject notesScreen;
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] GameObject evidencesScreen;
    [SerializeField] GameObject masksScreen;

    public Transform itemDetailSlot;
    private GameObject itemDetail;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowScreen(Screens.InGame);
    }

    public void ShowNotes()
    {
        ShowScreen(Screens.Notes);
    } 
    
    public void ShowInventory()
    {
        ShowScreen(Screens.Inventory);
    } 
    
    public void ShowEvidences()
    {
        ShowScreen(Screens.Evidences);
    } 
    
    public void ShowMasks()
    {
        ShowScreen(Screens.Masks);
    }

    public void ShowScreen(Screens _screen)
    {
        notesScreen.SetActive(_screen == Screens.Notes);
        inventoryScreen.SetActive(_screen == Screens.Inventory);
        evidencesScreen.SetActive(_screen == Screens.Evidences);
        masksScreen.SetActive(_screen == Screens.Masks);

        screensObject.SetActive(_screen != Screens.InGame);
    }


    public void ShowItemDetails(GameObject _itemDetail)
    {
        if(itemDetail) Destroy(itemDetail);

        itemDetail = Instantiate(_itemDetail, itemDetailSlot);
    }
}
