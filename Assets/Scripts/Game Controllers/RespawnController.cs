using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

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

    private Vector3 spawnPoint;
    public float esperaParaRespawn;

    private GameObject thePlayer;

    public GameObject efeitoMorte;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;

        spawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(efeitoMorte != null)
        {
            Instantiate(efeitoMorte, thePlayer.transform.position, thePlayer.transform.rotation);
        }
        
        yield return new WaitForSeconds(esperaParaRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        thePlayer.transform.position = spawnPoint;
        thePlayer.SetActive(true);

        PlayerHealthController.instance.FillHealth();
    }
}
