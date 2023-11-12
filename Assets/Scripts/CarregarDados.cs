using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregarDados : MonoBehaviour
{

    [SerializeField] private GameObject menu;

    private void Awake()
    {
        menu.GetComponent<ControleDeSalvamento>().CarregarComecoJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
    }
}
