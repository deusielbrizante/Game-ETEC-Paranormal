using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleVidaBoss : MonoBehaviour
{
    //define os objetos de vida do Boss que são definidos no inspetor
    [SerializeField] private Slider barraVida;
    [SerializeField] private VidaInimigo vidaBoss;

    void Update()
    {
        //atualiza constatemente os valores de vida do boss
        barraVida.maxValue = vidaBoss.vidaMaxima;
        barraVida.value = vidaBoss.vidaAtual;

    }
}
