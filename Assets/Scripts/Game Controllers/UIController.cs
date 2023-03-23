using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public List<GameObject> lifes = new List<GameObject>();

    public GameObject lifeSprite;

    public GameObject lifeGroup;

    //transição de cena
    public Image blackScreen;
    public float fadeSpeed = 2f;
    private bool fading, brighting;

    public string mainMenuScene;

    public GameObject pauseScreen;

    public GameObject inventory;

    [Header("Inventario")]

    [SerializeField] GameObject screensObject;

    [SerializeField] GameObject notesScreen;
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] GameObject evidencesScreen;
    [SerializeField] GameObject masksScreen;

    public enum Screens
    {
        Notes,
        Inventory,
        Evidences,
        Masks,
        InGame
    }

    public Screens currentScreen;

    
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }    

    private void Start()
    {
        for (int i = 0; i < PlayerHealthController.instance.maxHealth; i++)
        {
            GameObject _life = Instantiate(lifeSprite, lifeGroup.transform);
            lifes.Add(_life);
        }
        UpdateSkulls(PlayerHealthController.instance.maxHealth);


        ShowScreen(Screens.InGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            
            if(blackScreen.color.a == 1f)
            {
                fading = false;
            }

        }else if (brighting) 
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                brighting = false;
            }
        }

        


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        } 
        
        if (Input.GetKeyDown(KeyCode.I) && PlayerAbilityTracker.instance.chainShield)
        {
            TheInventory();
        }
    }

    public void UpdateSkulls(int life)
    {
        foreach (var l in lifes)
        {
            l.transform.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < life; i++)
        {
            lifes[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Fading()
    {
        fading = true;
        brighting = false;
    }

    public void Brighting()
    {
        brighting = true;
        fading = false;
    }


    public void PauseUnPause()
    {
        if (!pauseScreen.activeSelf && !inventory.activeSelf)
        {
            pauseScreen.SetActive(true);
            PlayerController.Instance.canMove = false;
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            PlayerController.Instance.canMove = true;
            Time.timeScale = 1f;
        }
    } 
    
    public void TheInventory()
    {
        if (!pauseScreen.activeSelf && !inventory.activeSelf)
        {
            ShowInventory();
            PlayerController.Instance.canMove = false;
        }
        else if (inventory.activeSelf)
        {
            ShowScreen(Screens.InGame);
            PlayerController.Instance.canMove = true;
        }
    }

    public void ExtraLife()
    {
        GameObject _life = Instantiate(lifeSprite, lifeGroup.transform);
        lifes.Add(_life);
        UpdateSkulls(PlayerHealthController.instance.currentHealth);
        
    }

    public void MainMenu()
    {
        instance = null;
        Destroy(gameObject);

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        PlayerController.Instance.SavePosition();


        SceneManager.LoadScene(mainMenuScene);
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
}
