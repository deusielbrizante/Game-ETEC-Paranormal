using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    //variáveis de distância para o teletransporte
    [SerializeField] private Vector2 distanciaMinima;
    [SerializeField] private Vector2 distanciaMaxima;

    //variáveis de velocidade e duração de movimento
    [SerializeField] private float velocidadeMovimento;
    [SerializeField] private float duracaoMovimento;

    //tmepo para se teletransportar
    [SerializeField] private float tempoParaTeletransportar;

    //variáveis de estado
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

        //verifica se já pode se teletransportar
        if (tempoAtual >= tempoParaTeletransportar)
        {
            //chama a função de teletransporte e redefine o tempo corrido
            PosicaoDoTeleporte();
            tempoAtual = 0f;
        }

        //define a posição do jogador
        jogador = GameObject.FindGameObjectWithTag("Player").transform;

        //define a direção em vector2 do jogador
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
            //espera 5s no início
            yield return new WaitForSeconds(5f);

            //define o tempo inicial, verifica até onde o boss irá andar e calcula a distância dele até a posição
            posicaoFinalAoAndar = new Vector2(Random.Range(distanciaMinima.x, distanciaMaxima.x), Random.Range(distanciaMinima.y, distanciaMaxima.y));
            float distanciaAPercorrer = Vector2.Distance(transform.position, posicaoFinalAoAndar);
            float tempoInicial = Time.time;

            //verifica se o tempo atual é menor que o tempo inicial somado com a duração do movimento
            while (Time.time < tempoInicial + duracaoMovimento)
            {
                //cria variável da distância percorrida e outra que passa o valor do tempo para andar
                float distanciaPercorrida = (Time.time - tempoInicial) * velocidadeMovimento;
                float tempoParaAndar = distanciaPercorrida / distanciaAPercorrer;

                //faz a movimentação e retorna para fora do laço
                transform.position = Vector2.Lerp(transform.position, posicaoFinalAoAndar, tempoParaAndar);
                yield return null;
            }
        }
    }

    //verifica a posição do teleporte
    private void PosicaoDoTeleporte()
    {
        bool temColisor = true;

        //cria a posição aleatória dentro da distância máxima e mínima
        posicaoAleatoria = new Vector2(Random.Range(distanciaMinima.x, distanciaMaxima.x), Random.Range(distanciaMinima.y, distanciaMaxima.y));
            
        // 0.5f é o raio do círculo de verificação
        Collider2D[] colisor = Physics2D.OverlapCircleAll(posicaoAleatoria, 0.5f);

        //verifica se tem col~isão naquele local e se não tiver teleporta
        temColisor = colisor.Length > 0;
        transform.position = posicaoAleatoria;
    }
}
