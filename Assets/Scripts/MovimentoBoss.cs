using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    [SerializeField] private Vector2 distanciaMinima;
    [SerializeField] private Vector2 distanciaMaxima;
    [SerializeField] private float velocidadeMovimento;
    [SerializeField] private float duracaoMovimento;
    [SerializeField] private float tempoParaTeletransportar;

    private Vector2 posicaoFinalAoAndar;
    private float tempoAtual = 0f;
    private Vector2 posicaoAleatoria;
    private Transform jogador;

    void Start()
    {
        StartCoroutine(MoveBoss());
    }

    void Update()
    {

        tempoAtual += Time.deltaTime;

        if (tempoAtual >= tempoParaTeletransportar)
        {
            PosicaoDoTeleporte();
            tempoAtual = 0f;
        }

        jogador = GameObject.FindGameObjectWithTag("Player").transform;

        Vector2 direcao = (Vector2)jogador.transform.position - (Vector2)transform.position;
        if (direcao.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direcao.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private IEnumerator MoveBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            posicaoFinalAoAndar = new Vector2(Random.Range(distanciaMinima.x, distanciaMaxima.x), Random.Range(distanciaMinima.y, distanciaMaxima.y));
            float distanciaAPercorrer = Vector2.Distance(transform.position, posicaoFinalAoAndar);
            float tempoInicial = Time.time;

            while (Time.time < tempoInicial + duracaoMovimento)
            {
                float distanciaPercorrida = (Time.time - tempoInicial) * velocidadeMovimento;
                float tempoParaAndar = distanciaPercorrida / distanciaAPercorrer;
                transform.position = Vector2.Lerp(transform.position, posicaoFinalAoAndar, tempoParaAndar);
                yield return null;
            }

        }
    }

    private void PosicaoDoTeleporte()
    {
        bool temColisor = true;

        posicaoAleatoria = new Vector2(Random.Range(distanciaMinima.x, distanciaMaxima.x), Random.Range(distanciaMinima.y, distanciaMaxima.y));
            
        // 0.5f é o raio do círculo de verificação
        Collider2D[] colisor = Physics2D.OverlapCircleAll(posicaoAleatoria, 0.5f);

        temColisor = colisor.Length > 0;

        transform.position = posicaoAleatoria;
    }
}
