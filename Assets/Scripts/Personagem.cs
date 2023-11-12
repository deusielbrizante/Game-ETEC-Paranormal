using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{

    //variáveis para configurar a animação e movimento do personagem
    [SerializeField] private Animator animacao;
    public float velocidadeMovimento;

    public int TemChaveBau { get; set; }
    public bool TemChavePorta { get; set; }

    private float vertical;
    private float horizontal;

    public Image chaveBau;
    public Image chavePorta;

    private float ultimaDirecao;
    private Vector3 movimento;

    //variáveis para configurar a detecção de colisão com o chão e as paredes
    [SerializeField] private LayerMask parede;

    [SerializeField] private float distanciaAteParede;

    //referências para as colisões do personagem
    private BoxCollider2D colisor;
    private RaycastHit2D hit;
    private Vector3 boxSize;

    private void Awake()
    {

        //instacia as variáveis ao colisor do jogador
        colisor = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("chaveBau"))
        {
            TemChaveBau = PlayerPrefs.GetInt("chaveBau");
        }

        if (PlayerPrefs.HasKey("chaveSala"))
        {
            if (PlayerPrefs.GetInt("chaveSala") == 1)
            {

                TemChavePorta = true;

            }
            else
            {
                TemChavePorta = false;
            }

        }

        SalvarChaves();

        if (TemChavePorta)
        {
            chavePorta.enabled = true;
        }

        if(TemChaveBau >= 1)
        {
            chaveBau.enabled = true;
        }
    }

    private void Update()
    {

        //chama todas as funções que forem ser utilizadas a todo frame

        AtualizaAnimacao(movimento);

        MovimentaPersonagem(movimento);

    }

    public void InputMovimento(InputAction.CallbackContext valor)
    {

        Vector2 movimentoDirecao = valor.ReadValue<Vector2>();

        horizontal = movimentoDirecao.x;

        vertical = movimentoDirecao.y;

        //cria um vector3 com os valores em float horizontais e verticais das personagens sendo o 0 para a profundidade
        movimento = new Vector3(horizontal, vertical, 0f);
    }

    private void AtualizaAnimacao(Vector3 movimento)
    {

        //pega os valores de x, y e da velocidade e armazena para fazer a animação do jogador
        animacao.SetFloat("Horizontal", horizontal);
        animacao.SetFloat("Vertical", vertical);
        animacao.SetFloat("Speed", movimento.magnitude);

        AtualizaDirecaoIdle(movimento);

        // Atualize o parâmetro "IdleDirection" no Animator
        animacao.SetFloat("DirecaoFinalIdle", ultimaDirecao);

    }

    private void MovimentaPersonagem(Vector3 movimento)
    {

        //faz o cálculo da movimentação da personagem
        Vector3 novaPosicao = transform.position + movimento * velocidadeMovimento * Time.deltaTime;

        //verifica se a nova posição está colidindo com o chão
        if (!ColideComParede(novaPosicao))
        {

            transform.position = novaPosicao;

        }

    }

    //função verdadeiro ou falso para colisão com a parede
    private bool ColideComParede(Vector3 posicao)
    {

        boxSize = colisor.size - new Vector2(distanciaAteParede, distanciaAteParede);
        hit = Physics2D.BoxCast(posicao, size: boxSize, 0f, Vector2.zero, 0f, parede);
        return hit.collider != null;

    }

    private void AtualizaDirecaoIdle(Vector3 movimento)
    {
        // Atualiza a variável de direção de idle com base na última direção
        if (movimento != Vector3.zero)
        {
            if (Mathf.Abs(movimento.x) > Mathf.Abs(movimento.y))
            {
                if (movimento.x > 0) // Direita
                    ultimaDirecao = 4;
                else // Esquerda
                    ultimaDirecao = 3;
            }
            else
            {
                if (movimento.y > 0) // Cima
                    ultimaDirecao = 1;
                else // Baixo
                    ultimaDirecao = 2;
            }
        }
    }

    public void SalvarChaves()
    {
        if (PlayerPrefs.HasKey("chaveBau"))
        {
            PlayerPrefs.DeleteKey("chaveBau");
            PlayerPrefs.SetFloat("chaveBau", TemChaveBau);
            Debug.Log($"salvou no script vida atual: {PlayerPrefs.GetFloat("chaveBau")}");

        }
        else
        {

            PlayerPrefs.SetFloat("chaveBau", TemChaveBau);
            Debug.Log($"salvou no script vida atual: {PlayerPrefs.GetFloat("chaveBau")}");

        }

        if (PlayerPrefs.HasKey("chaveSala"))
        {

            PlayerPrefs.DeleteKey("chaveSala");

            if (TemChavePorta)
            {

                PlayerPrefs.SetFloat("chaveSala", 1);

            }
            else
            {
                PlayerPrefs.SetFloat("chaveSala", 0);
            }

            Debug.Log($"salvou no script vida maxima: {PlayerPrefs.GetFloat("vidaMaxima")}");

        }
        else
        {

            if (TemChavePorta)
            {

                PlayerPrefs.SetFloat("chaveSala", 1);

            }
            else
            {
                PlayerPrefs.SetFloat("chaveSala", 0);
            }

            Debug.Log($"salvou no script vida maxima: {PlayerPrefs.GetFloat("vidaMaxima")}");

        }

        PlayerPrefs.Save();
    }
}
