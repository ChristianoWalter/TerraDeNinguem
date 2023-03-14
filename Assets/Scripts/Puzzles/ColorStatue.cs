using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStatue : InteractablesController
{
    public enum Cor
    {
        Azul,
        Amarelo,
        Verde,
        Vermelho
    }

    public Cor cor;

   public void Interact()
    {
        //Disco.Instance.Interact(cor);
    }
}
