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
    public Image telaPreta;

    public float fadeSpeed = 2f;

    private bool escurecendo, clareando;

    public string mainMenuScene;

    public GameObject pauseScreen;

    public GameObject mapaTelaCheia;

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
        if (escurecendo)
        {
            telaPreta.color = new Color(telaPreta.color.r, telaPreta.color.g, telaPreta.color.b, Mathf.MoveTowards(telaPreta.color.a, 1f, fadeSpeed * Time.deltaTime));
            
            if(telaPreta.color.a == 1f)
            {
                escurecendo = false;
            }

        }else if (clareando) 
        {
            telaPreta.color = new Color(telaPreta.color.r, telaPreta.color.g, telaPreta.color.b, Mathf.MoveTowards(telaPreta.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (telaPreta.color.a == 0f)
            {
                clareando = false;
            }
        }

        


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
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
        escurecendo = true;
        clareando = false;
    }

    public void ComecaAClarear()
    {
        clareando = true;
        escurecendo = false;
    }


    public void PauseUnPause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            PlayerControl.Instance.canMove = false;
        }
        else
        {
            pauseScreen.SetActive(false);
            PlayerControl.Instance.canMove = true; 
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
