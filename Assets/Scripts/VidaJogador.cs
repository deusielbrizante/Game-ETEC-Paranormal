using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaJogador : MonoBehaviour
{

    //controle de vida do personagem
    [SerializeField] private float vidaAtual;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private Text coracaoTexto;
    [SerializeField] private Image coracaoImagem;
    [SerializeField] private Sprite coracaoCheio;
    [SerializeField] private Sprite coracaoQuaseCheio;
    [SerializeField] private Sprite coracaoMetade;
    [SerializeField] private Sprite coracaoQuaseVazio;
    [SerializeField] private Sprite coracaoVazio;

    public bool Morreu = false;

    private void Start()
    {

        //instancia a vida atual e maxima do jogador
        if (PlayerPrefs.HasKey("vidaAtual"))
        {
            vidaAtual = PlayerPrefs.GetFloat("vidaAtual");
        }

        if (PlayerPrefs.HasKey("vidaMaxima"))
        {
            vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima");
        }

        SalvarVida();

        if(coracaoTexto != null)
        {
            coracaoTexto.text = vidaAtual.ToString();
        }

    }

    private void FixedUpdate()
    {

        AjustaVida();

        float porcentagemVida = (vidaAtual / vidaMaxima) * 100f;

        if(coracaoImagem != null)
        {

            if (porcentagemVida >= 81)
            {
                coracaoImagem.sprite = coracaoCheio;
            }
            else if (porcentagemVida <= 80 && porcentagemVida >= 61)
            {
                coracaoImagem.sprite = coracaoQuaseCheio;
            }
            else if (porcentagemVida <= 60 && porcentagemVida >= 41)
            {
                coracaoImagem.sprite = coracaoMetade;
            }
            else if (porcentagemVida <= 40 && porcentagemVida >= 1)
            {
                coracaoImagem.sprite = coracaoQuaseVazio;
            }

        }

    }

    //usando a função de atualizar vida e se morrer para parar o jogo
    public void AtualizaVida(float dano)
    {

        vidaAtual += dano;
        coracaoTexto.text = vidaAtual.ToString();

        AjustaVida();

    }

    private void AjustaVida()
    {
        if (vidaAtual > vidaMaxima)
        {

            vidaAtual = vidaMaxima;
            coracaoTexto.text = vidaAtual.ToString();

        }
        else if (vidaAtual <= 0)
        {

            vidaAtual = 0;
            coracaoTexto.text = vidaAtual.ToString();
            Debug.Log("Você Perdeu!!!");

            //zera o tempo quando morre
            coracaoImagem.sprite = coracaoVazio;
            Morreu = true;
            Time.timeScale = 0;

        }
    }

    public void SalvarVida()
    {
        if (PlayerPrefs.HasKey("vidaAtual"))
        {
            PlayerPrefs.DeleteKey("vidaAtual");
            PlayerPrefs.SetFloat("vidaAtual", vidaAtual);
            Debug.Log($"salvou no script vida atual: {PlayerPrefs.GetFloat("vidaAtual")}");

        }
        else
        {

            PlayerPrefs.SetFloat("vidaAtual", vidaAtual);
            Debug.Log($"salvou no script vida atual: {PlayerPrefs.GetFloat("vidaAtual")}");

        }

        if (PlayerPrefs.HasKey("vidaMaxima"))
        {

            PlayerPrefs.DeleteKey("vidaMaxima");
            PlayerPrefs.SetFloat("vidaMaxima", vidaMaxima);
            Debug.Log($"salvou no script vida maxima: {PlayerPrefs.GetFloat("vidaMaxima")}");

        }
        else
        {

            PlayerPrefs.SetFloat("vidaMaxima", vidaMaxima);
            Debug.Log($"salvou no script vida maxima: {PlayerPrefs.GetFloat("vidaMaxima")}");
        
        }

        PlayerPrefs.Save();
    }

    public void Cura()
    {
        vidaAtual += 15;
        coracaoTexto.text = vidaAtual.ToString();
    }

    public void SubiuDeNivel()
    {
        vidaMaxima += 15;
        vidaAtual = vidaMaxima;
        coracaoTexto.text = vidaAtual.ToString();
    }

}
