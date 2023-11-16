using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject telaGameOver;
    [SerializeField] private AudioSource somGameOver;
    [SerializeField] private AudioSource somMusica;
    [SerializeField] private GameObject botaoReiniciar;

    private GameObject ultimoBotaoSelecionado;

    private void Awake()
    {
        somMusica.volume = PlayerPrefs.GetFloat("audio");
        telaGameOver.SetActive(false);
    }

    private void Update()
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetJoystickNames().Length > 0)
        {
            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;
        }
        
        if(jogador != null)
        {
            if (jogador.GetComponent<VidaJogador>().Morreu)
            {
                telaGameOver.SetActive(true);
                somMusica.Pause();
                somGameOver.enabled = true;
                somGameOver.volume = PlayerPrefs.GetFloat("audio");
                menu.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if(Input.GetJoystickNames().Length > 0)
                {
                    EventSystem.current.SetSelectedGameObject(botaoReiniciar);
                }
                else
                {
                    if (ultimoBotaoSelecionado != null)
                    {
                        ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);
                    }
                }
            }
        }

    }
    public void ReiniciarFase()
    {
        menu.GetComponent<ControleDeSalvamento>().CarregarJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
    }
}
