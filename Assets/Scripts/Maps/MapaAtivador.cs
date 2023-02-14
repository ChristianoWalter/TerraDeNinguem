using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaAtivador : MonoBehaviour
{
    public string mapaASerAtivado;

    // Start is called before the first frame update
    void Start()
    {
        MapController.instance.AtivaMapa(mapaASerAtivado);
    }

}
