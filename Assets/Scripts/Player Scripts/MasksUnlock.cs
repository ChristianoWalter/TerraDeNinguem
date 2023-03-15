using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksUnlock : MonoBehaviour
{
    public bool unlockChainShield, unlockAgroMask, unlockFrogMask, unlockTrueMask;

    //efeito de coleta
    public GameObject efeitoColeta;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockAgroMask)
            {
                player.agroMask = true;

                PlayerPrefs.SetInt("MascaraAgriculturaDesbloqueada", 1);
            }
            
            if (unlockChainShield)
            {
                player.chainShield = true;

                PlayerPrefs.SetInt("EscudoCorrenteDesbloqueado", 1);

            } 
            
            if (unlockFrogMask)
            {
                player.frogMask = true;

                PlayerPrefs.SetInt("MascaraGulaDesbloqueada", 1);
            }

            if (unlockTrueMask)
            {
                player.trueMask = true;

                PlayerPrefs.SetInt("MascaraMentiraDesbloqueada", 1);
            }

            //efeito de coleta
            Instantiate(efeitoColeta, transform.position, transform.rotation);

            Destroy(gameObject);

            //AudioManager.instance.PlaySfx(5);
        }
    }
}
