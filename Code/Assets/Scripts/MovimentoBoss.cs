using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    //vari�veis de dist�ncia para o teletransporte
    [SerializeField] private Vector2 distanciaMinima;
    [SerializeField] private Vector2 distanciaMaxima;

    //vari�veis de velocidade e dura��o de movimento
    [SerializeField] private float velocidadeMovimento;
    [SerializeField] private float duracaoMovimento;

    //tmepo para se teletransportar
    [SerializeField] private float tempoParaTeletransportar;

    //vari�veis de estado
    private Vector2 posicaoFinalAoAndar;
    private float tempoAtual = 0f;
    private Vector2 posicaoAleatoria;
    private Transform jogador;

    void Start()
    {
        //inicia a corrotina de movimento do boss
        StartCoroutine(MoveBoss());
    }

    void Update()
    {
        //define o tempo atual corrido
        tempoAtual += Time.deltaTime;

        //verifica se j� pode se teletransportar
        if (tempoAtual >= tempoParaTeletransportar)
        {
            //chama a fun��o de teletransporte e redefine o tempo corrido
            PosicaoDoTeleporte();
            tempoAtual = 0f;
        }

        //define a posi��o do jogador
        jogador = GameObject.FindGameObjectWithTag("Player").transform;

        //define a dire��o em vector2 do jogador
        Vector2 direcao = (Vector2)jogador.transform.position - (Vector2)transform.position;
        
        //verifica se o boss tem que rotacionar para ficar olhando para ele
        if (direcao.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direcao.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    //corrotina de movimento do boss
    private IEnumerator MoveBoss()
    {
        //define um loop infinito
        while (true)
        {
            //espera 5s no in�cio
            yield return new WaitForSeconds(5f);

            //define o tempo inicial, verifica at� onde o boss ir� andar e calcula a dist�ncia dele at� a posi��o
            posicaoFinalAoAndar = new Vector2(Random.Range(distanciaMinima.x, distanciaMaxima.x), Random.Range(distanciaMinima.y, distanciaMaxima.y));
            float distanciaAPercorrer = Vector2.Distance(transform.position, posicaoFinalAoAndar);
            float tempoInicial = Time.time;

            //verifica se o tempo atual � menor que o tempo inicial somado com a dura��o do movimento
            while (Time.time < tempoInicial + duracaoMovimento)
            {
                //cria vari�vel da dist�ncia percorrida e outra que passa o valor do tempo para andar
                float distanciaPercorrida = (Time.time - tempoInicial) * velocidadeMovimento;
                float tempoParaAndar = distanciaPercorrida / distanciaAPercorrer;

                //faz a movimenta��o e retorna para fora do la�o
                transform.position = Vector2.Lerp(transform.position, posicaoFinalAoAndar, tempoParaAndar);
                yield return null;
            }
        }
    }

    //verifica a posi��o do teleporte
    private void PosicaoDoTeleporte()
    {
        bool temColisor = true;

        //cria a posi��o aleat�ria dentro da dist�ncia m�xima e m�nima
        posicaoAleatoria = new Vector2(Random.Range(distanciaMinima.x, distanciaMaxima.x), Random.Range(distanciaMinima.y, distanciaMaxima.y));
            
        // 0.5f � o raio do c�rculo de verifica��o
        Collider2D[] colisor = Physics2D.OverlapCircleAll(posicaoAleatoria, 0.5f);

        //verifica se tem col~is�o naquele local e se n�o tiver teleporta
        temColisor = colisor.Length > 0;
        transform.position = posicaoAleatoria;
    }
}
