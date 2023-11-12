using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOver : MonoBehaviour
{

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject telaGameOver;
    [SerializeField] private AudioSource somGameOver;
    [SerializeField] private AudioSource somMusica;
    [SerializeField] private GameObject botaoReiniciar;

    private void Awake()
    {
        somMusica.volume = PlayerPrefs.GetFloat("audio");
    }

    private void Update()
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

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
                EventSystem.current.SetSelectedGameObject(botaoReiniciar);

            }
        }

    }
    public void ReiniciarFase()
    {
        menu.GetComponent<ControleDeSalvamento>().CarregarJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
    }
}
