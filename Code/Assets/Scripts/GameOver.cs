using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //vari�veis do menu, da tela, dos sons e do bot�o de reiniciar
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject telaGameOver;
    [SerializeField] private AudioSource somGameOver;
    [SerializeField] private AudioSource somMusica;
    [SerializeField] private GameObject botaoReiniciar;

    //vari�vel privada do �ltimo bot�o selecionado
    private GameObject ultimoBotaoSelecionado;

    private void Awake()
    {
        //define no come�o o volume da m�sica igual ao �udio e deixa a tela de game over inativa
        somMusica.volume = PlayerPrefs.GetFloat("audio");
        telaGameOver.SetActive(false);
    }

    private void Update()
    {
        //vari�vel do jogador
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        //verifica se h� controle conectado
        if (Input.GetJoystickNames().Length > 0)
        {
            //se houver conectado um controle ele ir� selecionar o �ltimo bot�o que estava selecionado
            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;
        }
        
        //verifica se o jogador n�o � nulo
        if(jogador != null)
        {
            //verifica se ele morreu
            if (jogador.GetComponent<VidaJogador>().Morreu)
            {
                //se morreu ativa a tela de game over e liga a m�sica e define o �udio
                telaGameOver.SetActive(true);
                somMusica.Pause();
                somGameOver.enabled = true;
                somGameOver.volume = PlayerPrefs.GetFloat("audio");

                //desativa o menu para ele n�o pausar morto e ativa o mouse
                menu.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                //se houver controle conectado ele ir� selecionar o bot�o reiniciar de come�o
                if(Input.GetJoystickNames().Length > 0)
                {
                    EventSystem.current.SetSelectedGameObject(botaoReiniciar);
                }
                else
                {
                    //sen�o ele desseleciona todos os bot�es da cena atual
                    if (ultimoBotaoSelecionado != null)
                    {
                        ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);
                    }
                }
            }
        }

    }

    //caso ele clique em reiniciar a fase, ir� recarregar a cena atual
    public void ReiniciarFase()
    {
        menu.GetComponent<ControleDeSalvamento>().CarregarJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
    }
}
