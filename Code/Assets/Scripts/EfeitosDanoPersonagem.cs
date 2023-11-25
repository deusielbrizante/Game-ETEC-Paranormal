using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDanoPersonagem : MonoBehaviour
{
    //vari�veis para dar efeito ao jogador
    public bool fogoAtivo = false;
    public bool danoAtivo = false;
    public bool geloAtivo = false;

    //vari�veis de velocidade do jogador
    private float velocidadeDeAtaqueJogador;
    private float velocidadeDeMovimento;

    //vari�vel de som do jogador
    private AudioSource somDanoJogador;

    private void Update()
    {
        //verifica se o gelo ativo da vari�vel global est� como true e se o componente dele est� true
        if(GeloAtivo.Ativo == true && geloAtivo == true)
        {
            StartCoroutine(AdicionaEfeitoGelo());
        }

        //verifica se o gelo ativo da vari�vel global est� como true para remover o efeito do gelo
        if (GeloAtivo.Ativo)
        {
            StartCoroutine(RemoveEfeitoGelo());
        }

        //verifica se o fogo ativo da vari�vel global est� como true e se o componente dele est� true
        if (FogoAtivo.Ativo == true && fogoAtivo == true)
        {

            StartCoroutine(DanoFogo());
        }

        //verifica se o jogador recebeu dano para ativar o efeito
        if (danoAtivo)
        {
            StartCoroutine(AnimacaoDano());
        }

    }

    //corrotina com o efeito do gelo
    public IEnumerator AdicionaEfeitoGelo()
    {
        //define como falso o gelo do jogador ap�s ser chamada
        geloAtivo = false;

        //pega os componentes atuais e os seus valores que est�o no jogador
        SistemaArma arma = GetComponent<SistemaArma>();
        Personagem movimentoJogador = GetComponent<Personagem>();
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        //define as vari�veis locais como a do estado atual do jogador antes de alterar
        velocidadeDeAtaqueJogador = arma.velocidadeDeAtaque;
        velocidadeDeMovimento = movimentoJogador.velocidadeMovimento;

        //altera os componentes, diminuindo a velocidade de ataque e movimento do jogador, al�m de mudar a cor dele para um azul claro e ativar as part�culas
        arma.velocidadeDeAtaque *= 3;
        movimentoJogador.velocidadeMovimento /= 3;
        spriteJogador.color = new Color(0f, 0.9f, 1f, 1f);
        GetComponent<EfeitoGelo>().AtivaEfeito();

        //espera 1,5s para sair
        yield return new WaitForSeconds(1.5f);
    }

    //corrotina para remover o efeito de gelo do jogador
    public IEnumerator RemoveEfeitoGelo()
    {
        //pega os componentes do jogador
        SistemaArma velocidadeDeAtaqueDoJogador = GetComponent<SistemaArma>();
        Personagem movimentoJogador = GetComponent<Personagem>();
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        //espera 1,5s para continuar
        yield return new WaitForSeconds(1.5f);

        //retorna ao normal os atributos do jogador
        velocidadeDeAtaqueDoJogador.velocidadeDeAtaque = velocidadeDeAtaqueJogador;
        movimentoJogador.velocidadeMovimento = velocidadeDeMovimento;

        //verifica se o fogo est� como falso para retornar a cor normal do jogador
        if (FogoAtivo.Ativo == false)
        {
            spriteJogador.color = Color.white;
        }

        //define a vari�vel global de gelo como falsa
        GeloAtivo.Ativo = false;
    }

    //corrotina com o efeito de jogo
    public IEnumerator DanoFogo()
    {
        //define o fogo local como falso
        fogoAtivo = false;

        //instancia o som do jogador para a vari�vel de som
        somDanoJogador = GetComponent<AudioSource>();

        //pega os componentes do jogador
        VerificacaoDeDano verificaDano = GetComponent<VerificacaoDeDano>();
        VidaJogador vidaJogador = GetComponent<VidaJogador>();
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        //espera 1s para come�ar a dar o dano
        yield return new WaitForSeconds(1f);

        //faz o dano repetir 5 vezes
        for (int i = 1; i <= 5; i++)
        {

            //espera 1s como intervalo entre os danos
            yield return new WaitForSeconds(1f);

            //retira a vida do jogador
            vidaJogador.AtualizaVida(-1f);

            //mostra o dano causado ao jogador
            verificaDano.VerificaDano("-1");

            //faz a troca de cores para receber o dano
            spriteJogador.color = Color.red;

            //ativa o som do jogador
            somDanoJogador.Play();

            //espera 0,2s para fazer a anima��o
            yield return new WaitForSeconds(0.2f);

            //verifica se o gelo est� ativo
            if (GeloAtivo.Ativo == false)
            {
                //se n�o estiver, volta a cor normal do jogador
                spriteJogador.color = Color.white;
            }
            else
            {
                //se estiver ativo, ele volta para as cores do gelo
                spriteJogador.color = new Color(0f, 0.9f, 1f, 1f);
            }
        }

        //define as vari�veis locais como falso
        FogoAtivo.Ativo = false;
        fogoAtivo = false;

    }

    //corrotina com a anima��o do jogador de tomar dano
    public IEnumerator AnimacaoDano()
    {

        //instancia a sprite do jogador
        SpriteRenderer spriteJogador = GetComponent<SpriteRenderer>();

        //verifica se o objeto cont�m um SpriteRenderer 
        if (spriteJogador != null)
        {
            //altera e volta as cores do jogador ap�s 0.2s
            spriteJogador.color = Color.red;

            //espera 0,2s para a anima��o
            yield return new WaitForSeconds(0.2f);

            //retorna as cores ap�s o tempo e verifica se ele est� congelado ou n�o
            if (GeloAtivo.Ativo == false)
            {
                //se n�o estiver congelado volta a sprite ao normal
                spriteJogador.color = Color.white;
            }
            else
            {
                //se estiver congelado volta a cor de gelo
                spriteJogador.color = new Color(0f, 0.9f, 1f, 1f);
            }

            //define o dano local como falso
            danoAtivo = false;
        }
        else
        {
            //se for falso a sprite ir� somente voltar
            danoAtivo = false;
            yield return null;
        }

    }
}
