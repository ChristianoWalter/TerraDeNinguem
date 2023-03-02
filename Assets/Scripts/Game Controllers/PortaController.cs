using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaController : MonoBehaviour
{

    private PlayerController thePlayer;

    private bool playerExiting;

    public Transform exitPoint;

    public float movePlayerSpeed;

    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerExiting)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!playerExiting)
            {
                thePlayer.canMove = false;

                StartCoroutine(UseNextSceneCo());
            }
        }   
    }

    IEnumerator UseNextSceneCo()
    {
        playerExiting = true;

        UIController.instance.Fading();

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(exitPoint.position);
        thePlayer.canMove = true;

        UIController.instance.Brighting();


        PlayerPrefs.SetString("ContinueLevel", sceneToLoad);
        PlayerPrefs.SetFloat("PositionX", exitPoint.position.x);
        PlayerPrefs.SetFloat("PositionY", exitPoint.position.y);
        PlayerPrefs.SetFloat("PositionY", exitPoint.position.z);


        SceneManager.LoadScene(sceneToLoad);
    }
}
