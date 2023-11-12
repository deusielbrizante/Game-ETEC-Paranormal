using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDaCamera : MonoBehaviour
{
    //controla a velocidade em que a camera irá chegar ao jogador
    public float VelocidadeDaCamera;

    //variáveis para ajustar a câmera do jogador
    public Vector3 DistanciaDaCameraAoJogador = Vector3.up;
    private Transform Jogador;


    private void Start()
    {
        Jogador = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

        transform.position = Vector3.Lerp(Jogador.transform.position, transform.position, 0.1f);

    }
}
