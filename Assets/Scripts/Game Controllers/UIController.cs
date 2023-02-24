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

    public GameObject fullScreenMap;

    public GameObject inventory;

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
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory();
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

    public void ComecaAEscurecer()
    {
        fading = true;
        brighting = false;
    }

    public void ComecaAClarear()
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
        }
        else
        {
            pauseScreen.SetActive(false);
            PlayerController.Instance.canMove = true; 
        }
    } 
    
    public void Inventory()
    {
        if (!pauseScreen.activeSelf && !inventory.activeSelf)
        {
            inventory.SetActive(true);
            PlayerController.Instance.canMove = false;
        }
        else if (pauseScreen.activeSelf && !inventory.activeSelf)
        {
            return;
        }
        else if (inventory.activeSelf)
        {
            inventory.SetActive(false);
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
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        /*Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        Destroy(MapController.instance.gameObject);
        MapController.instance = null;*/

        instance = null;
        Destroy(gameObject);
        
        SceneManager.LoadScene(mainMenuScene);
    }
}
