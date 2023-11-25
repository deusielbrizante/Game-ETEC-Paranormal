using System.Collections;
using UnityEngine;

public class DanoSlime : MonoBehaviour
{
    
    //vari�veis para configurar o ataque
    [SerializeField] private float velocidadeDeAtaque;
    [SerializeField] private int menorDano;
    [SerializeField] private int maiorDano;
    private float danoDeAtaque;

    //objeto dos inimigos
    [SerializeField] private GameObject slimeAzul;
    [SerializeField] private GameObject slimeVermelho;

    //objeto do texto de dano exibido
    [SerializeField] private GameObject textoDano;

    //convers�o de int para string para mostrar o dano causado
    private string danoCausado;

    //tempo para poder atacar
    private float podeAtacar = 10f;

    private void Awake()
    {
        //define o estado como falso do gelo e do fogo
        GeloAtivo.Ativo = false;
        FogoAtivo.Ativo = false;

    }

    private void OnCollisionStay2D(Collision2D outro)
    {

        //verifica se � o jogador
        if (outro.gameObject.tag == "Player")
        {

            var somDanoJogador = outro.gameObject.GetComponent<AudioSource>();

            //verifica se o inimigo pode atacar
            if (velocidadeDeAtaque <= podeAtacar)
            {

                //verifica qual inimigo est� atacando
                if(gameObject == slimeAzul)
                {

                    //muda a cor do dano para azul
                    textoDano.GetComponent<TextoDano>().TextoDeDano.color = Color.blue;

                    //faz receber o dano e inicia a verifica��o para ver se j� est� com gelo
                    Dano(outro);
                    somDanoJogador.Play();

                    //retorna a cor do texto para vermelho para n�o dar conflito
                    textoDano.GetComponent<TextoDano>().TextoDeDano.color = Color.red;

                    //se n�o estiver ativo o gelo, ele ativo e inicia a corrotina das altera��es
                    if (GeloAtivo.Ativo == false)
                    {
                        
                        outro.gameObject.GetComponent<EfeitosDanoPersonagem>().geloAtivo = true;
                        GeloAtivo.Ativo = true;

                    }

                }
                else if(gameObject == slimeVermelho)
                {

                    //muda a cor do dano para vermelho
                    textoDano.GetComponent<TextoDano>().TextoDeDano.color = Color.red;

                    //faz receber o dano e faz a anima��o de tomar dano
                    Dano(outro);
                    somDanoJogador.Play();
                    outro.gameObject.GetComponent<EfeitosDanoPersonagem>().danoAtivo = true;

                    //verifica se j� est� queimando
                    if (FogoAtivo.Ativo == false)
                    {

                        //avisa que j� est� queimando e ativa o dano
                        FogoAtivo.Ativo = true;
                        outro.gameObject.GetComponent<EfeitosDanoPersonagem>().fogoAtivo = true;
                    
                    }

                }
                else
                {

                    //muda a cor do dano para vermelho
                    textoDano.GetComponent<TextoDano>().TextoDeDano.color = Color.red;

                    //faz receber o dano e faz a anima��o de tomar dano
                    Dano(outro);
                    somDanoJogador.Play();
                    outro.gameObject.GetComponent<EfeitosDanoPersonagem>().danoAtivo = true;

                }

            }
            else
            {

                //adiciona o tempo para poder atacar
                podeAtacar += Time.deltaTime;

            }

        }
        else
        {
            return;
        }

    }

    private void Dano(Collision2D jogador)
    {
        //coloca um n�mero aleat�rio para o dano
        danoDeAtaque = Random.Range(menorDano, maiorDano);

        //diminui a vida do jogador
        jogador.gameObject.GetComponent<VidaJogador>().AtualizaVida(-danoDeAtaque);

        //mostra o dano causado ao jogador
        danoCausado = danoDeAtaque.ToString();
        jogador.gameObject.SendMessage("VerificaDano", ("-" + danoCausado));

        //reinicia o tempo para atacar novamente
        podeAtacar = 0f;
    }

}