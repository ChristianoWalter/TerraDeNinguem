using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigoController : MonoBehaviour
{
    public int vidaTotal = 3;

    public GameObject efeitoDaMorte;

    public void DanoAoInimigo(int damageAmount)
    {
        vidaTotal -= damageAmount;

        if (vidaTotal <= 0)
        {
            if (efeitoDaMorte != null)
            {
                Instantiate(efeitoDaMorte, transform.position, transform.rotation);
            }

            Destroy(gameObject);

            AudioManager.instance.PlaySfx(4);
        }
    }
}
