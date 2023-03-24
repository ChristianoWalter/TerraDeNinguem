using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameController : MonoBehaviour
{
    public static SaveGameController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("ContinueLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PositionX", PlayerController.Instance.playerPosition.position.x);
        PlayerPrefs.SetFloat("PositionY", PlayerController.Instance.playerPosition.position.y);
        PlayerPrefs.SetFloat("PositionZ", PlayerController.Instance.playerPosition.position.z);
    }

}
