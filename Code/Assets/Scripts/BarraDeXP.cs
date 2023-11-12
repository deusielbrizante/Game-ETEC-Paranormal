using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeXP : MonoBehaviour
{

    public float nivelAtual;
    private float NivelMaximo = 15f;
    private float expAtual;
    private float expNivelAtual;

    [SerializeField] private Text textoDoNivel;
    [SerializeField] private Slider barraDoNivel;
    [SerializeField] private VidaJogador vidaJogador;
    [SerializeField] private SistemaArma danoJogador;
    [SerializeField] private Text subiuDeNivel;

    private void Start()
    {

        danoJogador = GetComponent<SistemaArma>();

        if (PlayerPrefs.HasKey("expNivelAtual"))
        {
        
            if(PlayerPrefs.GetFloat("expNivelAtual") != 0)
            {

                expNivelAtual = PlayerPrefs.GetFloat("expNivelAtual");

            }
            else
            {
                PlayerPrefs.SetFloat("expNivelAtual", 100f);
                PlayerPrefs.Save();
                expNivelAtual = PlayerPrefs.GetFloat("expNivelAtual");
            }
        
        }
        else
        {
            expNivelAtual = 100f;

        }

        if (PlayerPrefs.HasKey("nivelAtual"))
        {

            if (PlayerPrefs.GetFloat("nivelAtual") != 0f)
            {

                nivelAtual = PlayerPrefs.GetFloat("nivelAtual");

            }
            else
            {
                PlayerPrefs.SetFloat("nivelAtual", 1f);
                PlayerPrefs.Save();
                expNivelAtual = PlayerPrefs.GetFloat("nivelAtual");
            }

        }
        else
        {
            nivelAtual = 1;

        }

        if (PlayerPrefs.HasKey("expAtual"))
        {

             expAtual = PlayerPrefs.GetFloat("expAtual");

        }
        else
        {
            expAtual = 0;

        }

        AtualizaHUD();
        SalvarXP();
        danoJogador.objTiro.GetComponent<DanoDaBolinha>().SubiuDeNivel(MenorDano: ((((nivelAtual - 1) * 5) + 5)), MaiorDano: (((nivelAtual - 1) * 5) + 15));

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

        Debug.Log("chegou aqui");

        if(nivelAtual == NivelMaximo && expAtual == expNivelAtual)
        {
            return;
        }


        float xpAtual = expAtual + xp;
        float xpProximoNivel = xpAtual - expNivelAtual;

        if(xpAtual < expNivelAtual)
        {

            expAtual = xpAtual;

        }
        else if(xpAtual >= expNivelAtual)
        {

            expAtual = 0;
            SubirDeNivel(xpProximoNivel);

        }

        AtualizaHUD();

    }

    private void SubirDeNivel(float xp)
    {

        if(nivelAtual >= NivelMaximo)
        {

            expAtual = expNivelAtual;
            AtualizaHUD();
            return;
        
        }

        expNivelAtual *= 1.5f;
        nivelAtual++;

        StartCoroutine(AnimacaoSubiuDeNivel());
        vidaJogador.SubiuDeNivel();
        danoJogador.objTiro.GetComponent<DanoDaBolinha>().SubiuDeNivel(MenorDano:((((nivelAtual - 1) * 5) + 5)), MaiorDano: (((nivelAtual - 1) * 5) + 15));
        AdicionaXP(xp);

        if(nivelAtual % 5 == 0)
        {
            GetComponent<Personagem>().velocidadeMovimento += 0.5f;
        }

    }

    private void AtualizaHUD()
    {

        if(textoDoNivel != null)
        {

            textoDoNivel.text = nivelAtual.ToString();
        
        }

        if(barraDoNivel != null)
        {

            barraDoNivel.maxValue = expNivelAtual;
            barraDoNivel.value = expAtual;

        }

        Debug.Log(expAtual);
        Debug.Log(nivelAtual);
        Debug.Log(expNivelAtual);

    }

    private IEnumerator AnimacaoSubiuDeNivel()
    { 

        for(int i = 0; i <= 3; i++)
        {

            subiuDeNivel.enabled = true;
            
            yield return new WaitForSeconds(0.5f);

            subiuDeNivel.enabled = false;

            yield return new WaitForSeconds(0.5f);

        }
    }

    public void SalvarXP()
    {
        if (PlayerPrefs.HasKey("expNivelAtual"))
        {

            PlayerPrefs.DeleteKey("expNivelAtual");
            PlayerPrefs.SetFloat("expNivelAtual", expNivelAtual);

        }
        else
        {

            PlayerPrefs.SetFloat("expNivelAtual", expNivelAtual);

        }

        if (PlayerPrefs.HasKey("expAtual"))
        {

            PlayerPrefs.DeleteKey("expAtual");
            PlayerPrefs.SetFloat("expAtual", expAtual);

        }
        else
        {

            PlayerPrefs.SetFloat("expAtual", expAtual);

        }

        if (PlayerPrefs.HasKey("nivelAtual"))
        {

            PlayerPrefs.DeleteKey("nivelAtual");
            PlayerPrefs.SetFloat("nivelAtual", nivelAtual);

        }
        else
        {

            PlayerPrefs.SetFloat("nivelAtual", nivelAtual);

        }

        PlayerPrefs.Save();
    }
}
