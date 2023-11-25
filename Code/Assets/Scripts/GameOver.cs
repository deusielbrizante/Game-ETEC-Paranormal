using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //variáveis do menu, da tela, dos sons e do botão de reiniciar
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject telaGameOver;
    [SerializeField] private AudioSource somGameOver;
    [SerializeField] private AudioSource somMusica;
    [SerializeField] private GameObject botaoReiniciar;

    //variável privada do último botão selecionado
    private GameObject ultimoBotaoSelecionado;

    private void Awake()
    {
        //define no começo o volume da música igual ao áudio e deixa a tela de game over inativa
        somMusica.volume = PlayerPrefs.GetFloat("audio");
        telaGameOver.SetActive(false);
    }

    private void Update()
    {
        //variável do jogador
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        //verifica se há controle conectado
        if (Input.GetJoystickNames().Length > 0)
        {
            //se houver conectado um controle ele irá selecionar o último botão que estava selecionado
            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;
        }
        
        //verifica se o jogador não é nulo
        if(jogador != null)
        {
            //verifica se ele morreu
            if (jogador.GetComponent<VidaJogador>().Morreu)
            {
                //se morreu ativa a tela de game over e liga a música e define o áudio
                telaGameOver.SetActive(true);
                somMusica.Pause();
                somGameOver.enabled = true;
                somGameOver.volume = PlayerPrefs.GetFloat("audio");

                //desativa o menu para ele não pausar morto e ativa o mouse
                menu.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                //se houver controle conectado ele irá selecionar o botão reiniciar de começo
                if(Input.GetJoystickNames().Length > 0)
                {
                    EventSystem.current.SetSelectedGameObject(botaoReiniciar);
                }
                else
                {
                    //senão ele desseleciona todos os botões da cena atual
                    if (ultimoBotaoSelecionado != null)
                    {
                        ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);
                    }
                }
            }
        }

    }

    //caso ele clique em reiniciar a fase, irá recarregar a cena atual
    public void ReiniciarFase()
    {
        menu.GetComponent<ControleDeSalvamento>().CarregarJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
    }
}
