using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleVidaBoss : MonoBehaviour
{

    [SerializeField] private Slider barraVida;
    [SerializeField] private VidaInimigo vidaBoss;

    void Update()
    {

        barraVida.maxValue = vidaBoss.vidaMaxima;
        barraVida.value = vidaBoss.vidaAtual;

    }
}
