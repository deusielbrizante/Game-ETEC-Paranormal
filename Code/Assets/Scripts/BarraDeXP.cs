using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeXP : MonoBehaviour
{

    //variáveis locais de nível e experiência
    public float nivelAtual;
    private float NivelMaximo = 15f;
    private float expAtual;
    private float expNivelAtual;

    //variáveis do inspetor dos objetos que serão usados
    [SerializeField] private Text textoDoNivel;
    [SerializeField] private Slider barraDoNivel;
    [SerializeField] private VidaJogador vidaJogador;
    [SerializeField] private SistemaArma danoJogador;
    [SerializeField] private Text subiuDeNivel;

    private void Start()
    {
        //define o dano do jogador
        danoJogador = GetComponent<SistemaArma>();

        //verifica o save para instânciar os valores do jogador
        if (PlayerPrefs.HasKey("expNivelAtual"))
        {
            //verifica se a experiência atual é diferente de zero
            if(PlayerPrefs.GetFloat("expNivelAtual") != 0)
            {

                expNivelAtual = PlayerPrefs.GetFloat("expNivelAtual");

            }
            else
            {
                //define a experiência do nivel atual como 100 e após isso recebe o valor
                PlayerPrefs.SetFloat("expNivelAtual", 100f);
                PlayerPrefs.Save();
                expNivelAtual = PlayerPrefs.GetFloat("expNivelAtual");
            }
        
        }
        else
        {
            //define como 100 de experiência
            expNivelAtual = 100f;
        }

        //verifica o save para instânciar os valores do jogador
        if (PlayerPrefs.HasKey("nivelAtual"))
        {
            //verifica se o nível atual é diferente de zero
            if (PlayerPrefs.GetFloat("nivelAtual") != 0f)
            {

                nivelAtual = PlayerPrefs.GetFloat("nivelAtual");

            }
            else
            {
                //define o nível atual como 1 e após isso recebe o valor
                PlayerPrefs.SetFloat("nivelAtual", 1f);
                PlayerPrefs.Save();
                expNivelAtual = PlayerPrefs.GetFloat("nivelAtual");
            }

        }
        else
        {
            //define como 1 o nível
            nivelAtual = 1;
        }

        //verifica o save para instânciar os valores do jogador
        if (PlayerPrefs.HasKey("expAtual"))
        {

             expAtual = PlayerPrefs.GetFloat("expAtual");

        }
        else
        {
            //define como 0 a experiência atual caso não tenha nada
            expAtual = 0;
        }

        //instancia na tela as variáveis
        AtualizaHUD();
        SalvarXP();
        danoJogador.objTiro.GetComponent<DanoDaBolinha>().SubiuDeNivel(MenorDano: ((((nivelAtual - 1) * 5) + 5)), MaiorDano: (((nivelAtual - 1) * 5) + 15));

        //verifica se o nível atual é divisível por 5 para aumentar a velocidade de movimento
        if(nivelAtual % 5 == 0)
        {

            for(int i = 5; i <= nivelAtual;i += 5)
            {
                GetComponent<Personagem>().velocidadeMovimento += 0.5f;
            }
        }
    }

    public void AdicionaXP(float xp)
    {
        //verifica se o jogador já chegou no limite de nível e experiência
        if(nivelAtual == NivelMaximo && expAtual == expNivelAtual)
        {
            return;
        }

        //instancia a experiência atual e a do próximo nível
        float xpAtual = expAtual + xp;
        float xpProximoNivel = xpAtual - expNivelAtual;

        //verifica se a experiência atual é menor que a do nível atual e se for pega a experiência obtida pelo jogador e soma
        if(xpAtual < expNivelAtual)
        {
            expAtual = xpAtual;
        }
        else if(xpAtual >= expNivelAtual)
        {
            //se a esperiência atual for maior ou igual a do nível atual, define como zero a experiência atual e sobe de nível
            expAtual = 0;
            SubirDeNivel(xpProximoNivel);
        }

        AtualizaHUD();
    }

    private void SubirDeNivel(float xp)
    {
        //verifica se o nível atual não é o nível máximo antes de subir
        if(nivelAtual >= NivelMaximo)
        {
            expAtual = expNivelAtual;
            AtualizaHUD();
            return;
        }

        //aumenta o nível atual e a experiência do nível atual
        expNivelAtual *= 1.5f;
        nivelAtual++;

        //inicia a animação de subir de nível e aumenta os atributos
        StartCoroutine(AnimacaoSubiuDeNivel());
        vidaJogador.SubiuDeNivel();
        danoJogador.objTiro.GetComponent<DanoDaBolinha>().SubiuDeNivel(MenorDano:((((nivelAtual - 1) * 5) + 5)), MaiorDano: (((nivelAtual - 1) * 5) + 15));
        AdicionaXP(xp);

        //verifica se o nível atual do jogador não é divisível por 5 para aumentar a velocidade
        if(nivelAtual % 5 == 0)
        {
            GetComponent<Personagem>().velocidadeMovimento += 0.5f;
        }
    }

    private void AtualizaHUD()
    {
        //verifica se o texto não é nulo antes de atualizar o nível atual
        if(textoDoNivel != null)
        {
            textoDoNivel.text = nivelAtual.ToString();
        }

        //verifica se a barra do nível não é nulo antes de atualizar para os novos valores
        if(barraDoNivel != null)
        {
            barraDoNivel.maxValue = expNivelAtual;
            barraDoNivel.value = expAtual;
        }
    }

    private IEnumerator AnimacaoSubiuDeNivel()
    { 
        //faz a animação de piscar 3 vezes o texto de subiu de nível
        for(int i = 0; i <= 3; i++)
        {
            //ativa o texto
            subiuDeNivel.enabled = true;
            yield return new WaitForSeconds(0.5f);

            //desativa o texto
            subiuDeNivel.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SalvarXP()
    {
        //verifica se há alguma chave de salvamento e se existir, deleta a chave e recria instanciando o novo valor
        if (PlayerPrefs.HasKey("expNivelAtual"))
        {
            PlayerPrefs.DeleteKey("expNivelAtual");
            PlayerPrefs.SetFloat("expNivelAtual", expNivelAtual);
        }
        else
        {
            //se não houver nenhuma chave, apenas cria e salva
            PlayerPrefs.SetFloat("expNivelAtual", expNivelAtual);
        }

        //verifica se há alguma chave de salvamento e se existir, deleta a chave e recria instanciando o novo valor
        if (PlayerPrefs.HasKey("expAtual"))
        {
            PlayerPrefs.DeleteKey("expAtual");
            PlayerPrefs.SetFloat("expAtual", expAtual);
        }
        else
        {
            //se não houver nenhuma chave, apenas cria e salva
            PlayerPrefs.SetFloat("expAtual", expAtual);
        }

        //verifica se há alguma chave de salvamento e se existir, deleta a chave e recria instanciando o novo valor
        if (PlayerPrefs.HasKey("nivelAtual"))
        {
            PlayerPrefs.DeleteKey("nivelAtual");
            PlayerPrefs.SetFloat("nivelAtual", nivelAtual);
        }
        else
        {
            //se não houver nenhuma chave, apenas cria e salva
            PlayerPrefs.SetFloat("nivelAtual", nivelAtual);
        }

        PlayerPrefs.Save();
    }
}
