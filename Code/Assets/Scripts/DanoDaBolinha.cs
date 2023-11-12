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

        if (outro.gameObject.tag == "Inimigo")
        {

            danoDeAtaque = Random.Range(menorDano, maiorDano);

            //chama a função do inimigo para tirar a vida
            outro.gameObject.GetComponent<AudioSource>().Play();
            outro.gameObject.GetComponent<VidaInimigo>().AtualizaVida(-danoDeAtaque);

            //exibe o dano causado
            danoCausado = danoDeAtaque.ToString("0");
            outro.SendMessage("VerificaDano", ("-" + danoCausado));

            Destroy(gameObject);

        }else if (outro.gameObject.tag == "colisor")
        {

            Destroy(gameObject);

        }

    }

    public void SalvarDano()
    {
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
        menorDano = MenorDano;
        maiorDano = MaiorDano;
    }

}
