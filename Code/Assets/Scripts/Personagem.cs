using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{

    //vari�veis para configurar a anima��o e movimento do personagem
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

    //vari�veis para configurar a detec��o de colis�o com o ch�o e as paredes
    [SerializeField] private LayerMask parede;

    [SerializeField] private float distanciaAteParede;

    //refer�ncias para as colis�es do personagem
    private BoxCollider2D colisor;
    private RaycastHit2D hit;
    private Vector3 boxSize;

    private void Awake()
    {

        //instacia as vari�veis ao colisor do jogador
        colisor = GetComponent<BoxCollider2D>();

        TemChaveBau = 0;
        TemChavePorta = false;

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

        //chama todas as fun��es que forem ser utilizadas a todo frame

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

        //pega os valores de x, y e da velocidade e armazena para fazer a anima��o do jogador
        animacao.SetFloat("Horizontal", horizontal);
        animacao.SetFloat("Vertical", vertical);
        animacao.SetFloat("Speed", movimento.magnitude);

        AtualizaDirecaoIdle(movimento);

        // Atualize o par�metro "IdleDirection" no Animator
        animacao.SetFloat("DirecaoFinalIdle", ultimaDirecao);

    }

    private void MovimentaPersonagem(Vector3 movimento)
    {

        //faz o c�lculo da movimenta��o da personagem
        Vector3 novaPosicao = transform.position + movimento * velocidadeMovimento * Time.deltaTime;

        //verifica se a nova posi��o est� colidindo com o ch�o
        if (!ColideComParede(novaPosicao))
        {

            transform.position = novaPosicao;

        }

    }

    //fun��o verdadeiro ou falso para colis�o com a parede
    private bool ColideComParede(Vector3 posicao)
    {

        boxSize = colisor.size - new Vector2(distanciaAteParede, distanciaAteParede);
        hit = Physics2D.BoxCast(posicao, size: boxSize, 0f, Vector2.zero, 0f, parede);
        return hit.collider != null;

    }

    private void AtualizaDirecaoIdle(Vector3 movimento)
    {
        // Atualiza a vari�vel de dire��o de idle com base na �ltima dire��o
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
