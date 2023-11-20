using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bau : MonoBehaviour
{
    //instancia as variáveis locais de baú, chave, clique e colisão
    public bool abriuBau = false;
    private int pegouChave;
    private bool estaPerto = false;
    private BarraDeXP xpJogador;
    private Personagem chaves;
    private bool clicou = false;

    //instancia as variáveis dos objetos que serão usadas pelo inspetor
    [SerializeField] private Animator animacao;
    [SerializeField] private GameObject coracao;
    [SerializeField] private GameObject chavePorta;

    private void Start()
    {
        //verifica se o baú já foi aberto e se foi, já ativa a animação e fala que já foi clicado
        if (abriuBau)
        {
            animacao.enabled = true;
            clicou = true;
        }
    }

    public void AbrirBau(InputAction.CallbackContext botao)
    {
        //verifica se as chave não são nulas
        if(chaves != null)
        {
            //define o números de chaves que o jogador tem
            pegouChave = chaves.TemChaveBau;

            //verifica se o botão foi clicado, se tem chave, se o clicou é falso e se o jogador está perto do baú
            if (botao.performed && pegouChave >= 1 && estaPerto && !clicou)
            {
                //ativa a animação do baú, define que foi aberto e clicado o baú e inicia as animações
                animacao.enabled = true;
                StartCoroutine(JogarCoracao());
                StartCoroutine(JogarChave());
                abriuBau = true;
                clicou = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D outro)
    {
        //verifica se não abriu o baú
        if (!abriuBau)
        {
            //verifica se é o jogador que está colidindo
            if (outro.gameObject.tag == "Player")
            {
                //pega as informações de experiência e de chaves do jogador e define que está perto
                chaves = outro.gameObject.GetComponent<Personagem>();
                xpJogador = outro.gameObject.GetComponent<BarraDeXP>();
                estaPerto = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    { 
        //caso o jogador saia de perto do baú define que está longe
        estaPerto = false;
    }

    private IEnumerator JogarCoracao()
    {
        //faz a animação de jogar o coração depois de 15ms e retira 1 chave do jogador
        yield return new WaitForSeconds(0.15f);
        coracao.SetActive(true);
        xpJogador.AdicionaXP(30f);
        chaves.TemChaveBau -= 1;

        //verifica se o jogador tem mais alguma chave, se não tiver desativa a chave da tela
        if(chaves.TemChaveBau == 0)
        {
            chaves.chaveBau.enabled = false;
        }

    }

    private IEnumerator JogarChave()
    {
        //faz a animação de jogar a chave depois de 15ms e define a chave da porta na tela
        yield return new WaitForSeconds(0.15f);
        chavePorta.SetActive(true);
    }
}
