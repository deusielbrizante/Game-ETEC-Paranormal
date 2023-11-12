using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecionarPersonagem : MonoBehaviour
{
    [SerializeField] private Text[] textoBotao;
    [SerializeField] private Button botaoContinuar;
    [HideInInspector] public bool PersonagemSelecionado = false;


    public void PersonagemEscolhido(int personagemEscolhido)
    {

        if (PlayerPrefs.HasKey("personagemSelecionado"))
        {

            PlayerPrefs.DeleteKey("personagemSelecionado");

        }

        PlayerPrefs.SetInt("personagemSelecionado", personagemEscolhido);
        PersonagemSelecionado = true;
        PlayerPrefs.Save();

        foreach (Text texto in textoBotao)
        {
            texto.text = "SELECIONAR";
        }

        textoBotao[personagemEscolhido].text = "SELECIONADO";
        botaoContinuar.interactable = true;

        Debug.Log(PlayerPrefs.GetInt("personagemSelecionado"));

    }

}
