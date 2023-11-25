using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InstanciarJogador : MonoBehaviour
{
    //array de todos os prefabs dos personagens
    [SerializeField] private GameObject[] Personagens;

    //personagem a ser instanciado como jogador
    private GameObject jogador;

    //câmera que irá seguir o jogador selecionado
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        //no começo irá definir dentro do array o personagem selecionado pelo número clicado no menu caso o número seja válido
        if (Personagens[PlayerPrefs.GetInt("personagemSelecionado")])
        {
            //define onde será instanciado o personagem na cena
            jogador = Instantiate(Personagens[PlayerPrefs.GetInt("personagemSelecionado")], transform.position, Quaternion.identity);
        }

    }

    private void Start()
    {
        //quando estiver carregando as informações, definirá a câmera para seguir o jogador instanciado
        virtualCamera.Follow = jogador.transform;
        virtualCamera.LookAt = jogador.transform;
    }

}
