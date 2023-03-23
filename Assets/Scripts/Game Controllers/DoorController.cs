using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] Vector2 nextStartPosition;
    [SerializeField] string nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("StartPositionX",nextStartPosition.x);
            PlayerPrefs.SetFloat("StartPositionY",nextStartPosition.y);
            SceneManager.LoadScene(nextScene);
        }
    }
}
