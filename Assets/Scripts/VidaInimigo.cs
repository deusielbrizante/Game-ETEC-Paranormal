using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{

    public bool PodeDropar;
    private bool rodouAudio = false;
    private bool salvouXP = false;

    //variáveis de vida
    public float vidaAtual;
    public float vidaMaxima;
    [SerializeField] private GameObject chaveBau;

    [SerializeField] private GameObject SlimeVerde;
    [SerializeField] private GameObject SlimeVermelho;
    [SerializeField] private GameObject SlimeAzul;
    [SerializeField] private GameObject AlienVerde;
    [SerializeField] private GameObject AlienVermelho;
    [SerializeField] private GameObject AlienAzul;
    [SerializeField] private GameObject SlimeVerdeChave;
    [SerializeField] private GameObject SlimeVermelhoChave;
    [SerializeField] private GameObject SlimeAzulChave;
    [SerializeField] private GameObject AlienVerdeChave;
    [SerializeField] private GameObject AlienVermelhoChave;
    [SerializeField] private GameObject AlienAzulChave;

    private void Start()
    {

        //coloca a vida máxima como atual quando inicia o jogo
        vidaAtual = vidaMaxima;

    }

    public void AtualizaVida(float dano)
    {

        vidaAtual += dano;

        if (vidaAtual > vidaMaxima)
        {

            vidaAtual = vidaMaxima;

        }
        else if (vidaAtual <= 0)
        {

            vidaAtual = 0;
            Audio();

            if(PodeDropar)
            {
                Instantiate(chaveBau, transform.position, Quaternion.identity);
            }

            XpJogador();

            if (rodouAudio && salvouXP)
            {
                Destruir();
            }
        }

    }

    private void XpJogador()
    {

        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        while(jogador == null)
        {
            jogador = GameObject.FindGameObjectWithTag("Player");
        }

        if (gameObject == SlimeVerde || gameObject == SlimeVerdeChave)
        {
            jogador.GetComponent<BarraDeXP>().AdicionaXP(10f);
            Debug.Log(SlimeVerde);
        }
        else if (gameObject == SlimeAzul || gameObject == SlimeAzulChave)
        {
            jogador.GetComponent<BarraDeXP>().AdicionaXP(15f);
        }
        else if (gameObject == SlimeVermelho || gameObject == SlimeVermelhoChave)
        {
            jogador.GetComponent<BarraDeXP>().AdicionaXP(20f);
        }
        else if (gameObject == AlienVerde || gameObject == AlienVerdeChave)
        {
            jogador.GetComponent<BarraDeXP>().AdicionaXP(30f);
        }
        else if (gameObject == AlienAzul || gameObject == AlienAzulChave)
        {
            jogador.GetComponent<BarraDeXP>().AdicionaXP(40f);
        }
        else if (gameObject == AlienVermelho || gameObject == AlienVermelhoChave)
        {
            jogador.GetComponent<BarraDeXP>().AdicionaXP(50f);
        }

        salvouXP = true;

    }

    private void Audio()
    {
        GetComponent<AudioSource>().Play();
        rodouAudio = true;
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }
}
