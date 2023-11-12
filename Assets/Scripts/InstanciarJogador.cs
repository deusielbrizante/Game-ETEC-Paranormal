using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InstanciarJogador : MonoBehaviour
{

    [SerializeField] private GameObject[] Personagens;
    private GameObject jogador;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {

        if (Personagens[PlayerPrefs.GetInt("personagemSelecionado")])
        {
            jogador = Instantiate(Personagens[PlayerPrefs.GetInt("personagemSelecionado")], transform.position, Quaternion.identity);
        }

    }

    private void Start()
    {
        virtualCamera.Follow = jogador.transform;
        virtualCamera.LookAt = jogador.transform;
    }

}
