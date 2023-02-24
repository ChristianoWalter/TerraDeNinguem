using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject[] mapas;

    public GameObject mapaCheioCam;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject mapa in mapas)
        {
            if(PlayerPrefs.GetInt("mapa_" + mapa.name) == 1)
            {
                mapa.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!UIController.instance.fullScreenMap.activeInHierarchy)
            {
                UIController.instance.fullScreenMap.SetActive(true);
                Time.timeScale = 0f;
                mapaCheioCam.SetActive(true);
            }
            else
            {
                UIController.instance.fullScreenMap.SetActive(false);
                Time.timeScale = 1f;
                mapaCheioCam.SetActive(false);
            }
        }
    }

    public void AtivaMapa(string mapaASerAtivado)
    {
        foreach(GameObject mapa in mapas)
        {
            if (mapa.name == mapaASerAtivado)
            {
                mapa.SetActive(true);
                PlayerPrefs.SetInt("Mapa_" + mapaASerAtivado, 1);
            }
        }
    }
}
