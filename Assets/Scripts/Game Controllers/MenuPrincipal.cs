using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject continueBotao;

    public PlayerAbilityTracker player;

    void Start()
    {
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueBotao.SetActive(true);
        }


        AudioManager.instance.PlayMainMenu();
    }

    public void Continue()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), PlayerPrefs.GetFloat("PositionZ"));

        if (PlayerPrefs.HasKey("PuloDuploDesbloqueado"))
        {
            if(PlayerPrefs.GetInt("PuloDuploDesbloqueado") == 1)
            {
                player.podePuloDuplo = true;
            }
        }
        
        if (PlayerPrefs.HasKey("DashDesbloqueado"))
        {
            if(PlayerPrefs.GetInt("DashDesbloqueado") == 1)
            {
                player.podeDash = true;
            }
        } 
        
        if (PlayerPrefs.HasKey("BolaDesbloqueado"))
        {
            if(PlayerPrefs.GetInt("BolaDesbloqueado") == 1)
            {
                player.podeVirarBola = true;
            }
        }
        
        if (PlayerPrefs.HasKey("BombaDesbloqueado"))
        {
            if(PlayerPrefs.GetInt("BombaDesbloqueado") == 1)
            {
                player.podeSoltarBombas = true;
            }
        }

            SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));

    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("Area 1");
    }
}
