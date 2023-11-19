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
        //inicia o componente de corpo do slime
        rb = GetComponent<Rigidbody2D>();

        //inicializa o NavMesh para que não colida com os obstáculos e possa ir na direção do jogador
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }

    private void Update()
    {

        //verifica o jogador mais próximo
        jogadorMaisProximo = EncontrarJogadorMaisProximo();

        //verifica se existe um jogador
        if (jogadorMaisProximo != null)
        {
            //verifica a distância entre ele e o jogador
            float distanciaParaJogador = Vector2.Distance(transform.position, jogadorMaisProximo.transform.position);

            //verifica se a distancia atual é maior que a distancia mínima e então se move
            if (distanciaParaJogador > distanciaMinimaParaJogador)
            {
                agente.SetDestination(jogadorMaisProximo.transform.position);
                VirarSprite(jogadorMaisProximo.transform.position);
            }
        }
    
    }

    //código que retorna o GameObject do jogador mais próximo
    private GameObject EncontrarJogadorMaisProximo()
    {
        //faz um array com todos os GameObject com a tag do jogador
        GameObject[] jogadores = GameObject.FindGameObjectsWithTag(jogadorTag);

        //instancia uma variável nula para o jogador, já que ainda não foi definida
        GameObject jogadorMaisProximo = null;

        //define a menor distância a percorrer até o jogador
        float menorDistancia = float.MaxValue;

        //verifica o array para encontrar o jogador mais próximo
        foreach (GameObject jogador in jogadores)
        {
            //cria uma variável para verificar a distância de cada jogador do array
            float distancia = Vector2.Distance(transform.position, jogador.transform.position);
            
            //verifica se a distancia nova é menor que a menorDistancia, e se for intancia o jogador atual do array como mais próximo
            if (distancia < menorDistancia)
            {
                //define o jogador mais próximo
                jogadorMaisProximo = jogador;

                //define a distância entre eles
                menorDistancia = distancia;
            }
        }

        //retorna o jogador mais próximo
        return jogadorMaisProximo;
    }

    //vira a sprite do inimigo baseado na direção que ele tem que perseguir o jogador
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
