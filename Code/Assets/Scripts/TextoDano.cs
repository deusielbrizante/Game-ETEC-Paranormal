using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoDano : MonoBehaviour
{

    //vari�vel de texto
    public Text TextoDeDano;

    void Start()
    {
        Destroy(gameObject, 0.4f);
    }

    public void TextoSelecionado (string dano){
        TextoDeDano.text = dano;
    }

}
