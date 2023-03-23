using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnLocation;

    private void Start()
    {
        if (PlayerController.Instance.gameObject == null)
        {
            /*float x = PlayerPrefs.GetFloat("StartPositionX", spawnLocation.position.x);
            float y = PlayerPrefs.GetFloat("StartPositionY", spawnLocation.position.y);
            */
            Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        }
    }

}
