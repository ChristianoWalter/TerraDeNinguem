using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Roots : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("RootsDestroyed", "false") == "false")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
