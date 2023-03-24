using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    private Vector3 spawnPoint;
    public float waitToSpawn;

    private GameObject thePlayer;

    public GameObject deathEffect;

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


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;

        spawnPoint = thePlayer.transform.position;
    }

    public void SetSpawn(Vector3 newPosition)
    {
        spawnPoint = newPosition;
    }
    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        thePlayer.SetActive(false);
        if(deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }
        
        yield return new WaitForSeconds(waitToSpawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        thePlayer.transform.position = spawnPoint;
        thePlayer.SetActive(true);

        PlayerHealthController.instance.FillHealth();

        //SaveGameController.Instance.SaveGame();
    }
}
