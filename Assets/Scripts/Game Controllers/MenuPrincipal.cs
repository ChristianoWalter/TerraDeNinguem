using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject continueButton;

    public PlayerAbilityTracker player;

    void Start()
    {
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }

        Time.timeScale = 1f;

        //AudioManager.instance.PlayMainMenu();
    }

    public void Continue()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), PlayerPrefs.GetFloat("PositionZ"));

        if (PlayerPrefs.HasKey("EscudoCorrenteDesbloqueado"))
        {
            if(PlayerPrefs.GetInt("EscudoCorrenteDesbloqueado") == 1)
            {
                player.chainShield = true;
            }
        }
        
        if (PlayerPrefs.HasKey("MascaraAgriculturaDesbloqueada"))
        {
            if(PlayerPrefs.GetInt("MascaraAgriculturaDesbloqueada") == 1)
            {
                player.agroMask = true;
            }
        } 
        
        if (PlayerPrefs.HasKey("MascaraGulaDesbloqueada"))
        {
            if(PlayerPrefs.GetInt("MascaraGulaDesbloqueada") == 1)
            {
                player.frogMask = true;
            }
        }
        
        if (PlayerPrefs.HasKey("MascaraMentiraDesbloqueada"))
        {
            if(PlayerPrefs.GetInt("MascaraMentiraDesbloqueada") == 1)
            {
                player.trueMask = true;
            }
        }

            SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));

    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("TutorialParte1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
