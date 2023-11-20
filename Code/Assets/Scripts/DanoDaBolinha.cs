using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoDaBolinha : MonoBehaviour
{

    //variáveis para os danos
    [SerializeField] private float menorDano;
    [SerializeField]private float maiorDano;
    private float danoDeAtaque;

    //conversão de int para string para mostrar o dano causado
    private string danoCausado;

    private void Awake()
    {
        //verifica se há alguma chave de save e se houver, instancia os valores
        if (PlayerPrefs.HasKey("menorDano"))
        {
            menorDano = PlayerPrefs.GetFloat("menorDano");
        }

        if (PlayerPrefs.HasKey("maiorDano"))
        {
            maiorDano = PlayerPrefs.GetFloat("maiorDano");
        }

        SalvarDano();
    }


    private void OnTriggerStay2D(Collider2D outro)
    {
        //verifica se é o inimigo que está colidindo
        if (outro.gameObject.tag == "Inimigo")
        {
            //define o dano aleatório a ser causado
            danoDeAtaque = Random.Range(menorDano, maiorDano);

            //chama a função do inimigo para alterar a vida
            outro.gameObject.GetComponent<AudioSource>().Play();
            outro.gameObject.GetComponent<VidaInimigo>().AtualizaVida(-danoDeAtaque);

            //exibe o dano causado
            danoCausado = danoDeAtaque.ToString("0");
            outro.SendMessage("VerificaDano", ("-" + danoCausado));

            Destroy(gameObject);
        }
        else if (outro.gameObject.tag == "colisor")
        {
            //se for um colisor, no caso objeto, ele destrói a bolinha também
            Destroy(gameObject);
        }
    }

    public void SalvarDano()
    {
        //verifica as chaves e se houver, deleta e instancia os valores, senão apenas salva
        if (PlayerPrefs.HasKey("menorDano"))
        {
            PlayerPrefs.DeleteKey("menorDano");
            PlayerPrefs.SetFloat("menorDano", menorDano);
        }
        else
        {
            PlayerPrefs.SetFloat("menorDano", menorDano);
        }

        if (PlayerPrefs.HasKey("maiorDano"))
        {
            PlayerPrefs.DeleteKey("maiorDano");
            PlayerPrefs.SetFloat("maiorDano", maiorDano);
        }
        else
        {
            PlayerPrefs.SetFloat("maiorDano", maiorDano);
        }

        PlayerPrefs.Save();
    }

    public void SubiuDeNivel(float MenorDano, float MaiorDano)
    {
        //atualiza o dano que pode ser causado pelo jogador
        menorDano = MenorDano;
        maiorDano = MaiorDano;
    }

}
