using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slimes : MonoBehaviour
{

    //variáveis para a velocidade e para o corpo
    [SerializeField] private float velocidadeMovimento;
    private Rigidbody2D rb;

    //tag do jogador
    private string jogadorTag = "Player";
    [SerializeField] private float distanciaMinimaParaJogador;

    //objeto a ser seguido
    private GameObject jogadorMaisProximo;

    [SerializeField] private NavMeshAgent agente;

    private void Start()
    {
        //inicia o componente de corpo do inimigo
        rb = GetComponent<Rigidbody2D>();
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    private void Update()
    {

        //verifica o jogador mais proximo
        jogadorMaisProximo = EncontrarJogadorMaisProximo();

        //verifica se existe um jogador
        if (jogadorMaisProximo != null)
        {
            //verifica a distância entre ele e o jogador
            float distanciaParaJogador = Vector2.Distance(transform.position, jogadorMaisProximo.transform.position);

            //verifica se a distancia atual é maior que a distancia minima e então se move
            if (distanciaParaJogador > distanciaMinimaParaJogador)
            {
                agente.SetDestination(jogadorMaisProximo.transform.position);
                VirarSprite(jogadorMaisProximo.transform.position);
            }
        }
    
    }

    private GameObject EncontrarJogadorMaisProximo()
    {
        GameObject[] jogadores = GameObject.FindGameObjectsWithTag(jogadorTag);
        GameObject jogadorMaisProximo = null;
        float menorDistancia = float.MaxValue;

        foreach (GameObject jogador in jogadores)
        {
            float distancia = Vector2.Distance(transform.position, jogador.transform.position);
            if (distancia < menorDistancia)
            {
                jogadorMaisProximo = jogador;
                menorDistancia = distancia;
            }
        }

        return jogadorMaisProximo;
    }

    private void MoverEmDirecaoAoAlvo(Vector2 alvo)
    {
        Vector2 direcao = alvo - (Vector2)transform.position;
        rb.velocity = (direcao.normalized * velocidadeMovimento);
    }

    private void VirarSprite(Vector2 alvo)
    {
        Vector2 direcao = alvo - (Vector2)transform.position;
        if (direcao.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direcao.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
