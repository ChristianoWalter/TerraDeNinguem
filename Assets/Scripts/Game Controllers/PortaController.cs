using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaController : MonoBehaviour
{
    public Animator anim;

    public float distanciaParaAbrir;

    private PlayerController thePlayer;

    private bool playerSaindo;

    public Transform pontoDeSaida;

    public float movePlayerSpeed;

    public string cenaACarregar;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, thePlayer.transform.position) < distanciaParaAbrir)
        {
            anim.SetBool("portaAberta", true);
        }
        else
        {
            anim.SetBool("portaAberta", false);
        }

        if (playerSaindo)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, pontoDeSaida.position, movePlayerSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!playerSaindo)
            {
                thePlayer.canMove = false;

                StartCoroutine(UsaPortaCo());
            }
        }   
    }

    IEnumerator UsaPortaCo()
    {
        playerSaindo = true;

        //thePlayer.anim.enabled = false;

        UIController.instance.ComecaAEscurecer();

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(pontoDeSaida.position);
        thePlayer.canMove = true;
        //thePlayer.anim.enabled = true;

        UIController.instance.ComecaAClarear();


        PlayerPrefs.SetString("ContinueLevel", cenaACarregar);
        PlayerPrefs.SetFloat("PositionX", pontoDeSaida.position.x);
        PlayerPrefs.SetFloat("PositionY", pontoDeSaida.position.y);
        PlayerPrefs.SetFloat("PositionY", pontoDeSaida.position.z);


        SceneManager.LoadScene(cenaACarregar);
    }
}
