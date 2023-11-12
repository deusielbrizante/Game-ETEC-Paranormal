using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDanoPersonagem : MonoBehaviour
{

    public bool fogoAtivo = false;
    public bool danoAtivo = false;
    public bool geloAtivo = false;

    private float velocidadeDeAtaqueJogador;
    private float velocidadeDeMovimento;

    private AudioSource somDanoJogador;

    private void Update()
    {
        if(GeloAtivo.Ativo == true && geloAtivo == true)
        {

            StartCoroutine(AdicionaEfeitoGelo());
        }

        if (GeloAtivo.Ativo)
        {
            StartCoroutine(RemoveEfeitoGelo());
        }

        if(FogoAtivo.Ativo == true && fogoAtivo == true)
        {

            StartCoroutine(DanoFogo());
        }

        if (danoAtivo)
        {
            StartCoroutine(AnimacaoDano());
        }

    }

    public IEnumerator AdicionaEfeitoGelo()
    {

        geloAtivo = false;
        //pega os componentes do jogador
        SistemaArma arma = GetComponent<SistemaArma>();
        Personagem movimentoJogador = GetComponent<Personagem>();
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        velocidadeDeAtaqueJogador = arma.velocidadeDeAtaque;
        velocidadeDeMovimento = movimentoJogador.velocidadeMovimento;

        //altera os componentes, diminuindo a velocidade de ataque e movimento do jogador, além de mudar a cor dele para um azul claro
        arma.velocidadeDeAtaque *= 3;
        movimentoJogador.velocidadeMovimento /= 3;
        spriteJogador.color = new Color(0f, 0.9f, 1f, 1f);
        GetComponent<EfeitoGelo>().AtivaEfeito();

        yield return new WaitForSeconds(1.5f);

    }

    public IEnumerator RemoveEfeitoGelo()
    {

        //pega os componentes do jogador
        SistemaArma velocidadeDeAtaqueDoJogador = GetComponent<SistemaArma>();
        Personagem movimentoJogador = GetComponent<Personagem>();
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        yield return new WaitForSeconds(1.5f);

        //retorna ao normal os atributos do jogador após 1s e meio
        velocidadeDeAtaqueDoJogador.velocidadeDeAtaque = velocidadeDeAtaqueJogador;
        movimentoJogador.velocidadeMovimento = velocidadeDeMovimento;

        if (FogoAtivo.Ativo == false)
        {

            spriteJogador.color = Color.white;

        }

        GeloAtivo.Ativo = false;

    }

    public IEnumerator DanoFogo()
    {
        fogoAtivo = false;
        somDanoJogador = GetComponent<AudioSource>();

        //pega os componentes do jogador
        VerificacaoDeDano verificaDano = GetComponent<VerificacaoDeDano>();
        VidaJogador vidaJogador = GetComponent<VidaJogador>();
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        yield return new WaitForSeconds(1.0f);

        //faz o dano repetir 5 vezes
        for (int i = 1; i <= 5; i++)
        {

            yield return new WaitForSeconds(1f);

            //retira a vida do jogador
            vidaJogador.AtualizaVida(-1f);

            //mostra o dano causado ao jogador
            verificaDano.VerificaDano("-1");


            //faz a troca de cores para receber o dano
            spriteJogador.color = Color.red;

            somDanoJogador.Play();

            yield return new WaitForSeconds(0.2f);

            if (GeloAtivo.Ativo == false)
            {

                spriteJogador.color = Color.white;

            }
            else
            {

                spriteJogador.color = new Color(0f, 0.9f, 1f, 1f);

            }

        }

        FogoAtivo.Ativo = false;
        fogoAtivo = false;

    }

    public IEnumerator AnimacaoDano()
    {

        //pega o sprite do jogador
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        //verifica se o objeto contêm um SpriteRenderer 
        if (spriteJogador != null)
        {

            //altera e volta as cores do jogador após 0.2s
            spriteJogador.color = Color.red;
            yield return new WaitForSeconds(0.2f);

            //retorna as cores após o tempo e verifica se ele está congelado ou não
            if (GeloAtivo.Ativo == false)
            {

                spriteJogador.color = Color.white;

            }
            else
            {

                spriteJogador.color = new Color(0f, 0.9f, 1f, 1f);

            }

            danoAtivo = false;

        }
        else
        {

            danoAtivo = false;
            yield return null;

        }

    }
}
