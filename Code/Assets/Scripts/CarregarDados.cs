using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregarDados : MonoBehaviour
{
    //define a variável menu pelo inspetor
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        //carrega todos os saves antes de começar o jogo
        menu.GetComponent<ControleDeSalvamento>().CarregarComecoJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
    }
}
