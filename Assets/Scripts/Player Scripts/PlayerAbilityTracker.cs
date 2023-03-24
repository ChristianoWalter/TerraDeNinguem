using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    public static PlayerAbilityTracker instance;

    public bool chainShield, agroMask, frogMask, trueMask;

    private void Awake()
    {
        instance = this;
    }

    public void UnlockChainShield()
    {
        chainShield = true;

        PlayerPrefs.SetInt("EscudoCorrenteDesbloqueado", 1);
    }
}
