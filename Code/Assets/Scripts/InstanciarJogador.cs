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

    //c�mera que ir� seguir o jogador selecionado
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        //no come�o ir� definir dentro do array o personagem selecionado pelo n�mero clicado no menu caso o n�mero seja v�lido
        if (Personagens[PlayerPrefs.GetInt("personagemSelecionado")])
        {
            //define onde ser� instanciado o personagem na cena
            jogador = Instantiate(Personagens[PlayerPrefs.GetInt("personagemSelecionado")], transform.position, Quaternion.identity);
        }

    }

    private void Start()
    {
        //quando estiver carregando as informa��es, definir� a c�mera para seguir o jogador instanciado
        virtualCamera.Follow = jogador.transform;
        virtualCamera.LookAt = jogador.transform;
    }

}
