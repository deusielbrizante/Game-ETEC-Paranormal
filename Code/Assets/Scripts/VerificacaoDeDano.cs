using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificacaoDeDano : MonoBehaviour
{

    //pega o objeto texto que exibe o dano 
    public GameObject TextoDano;

    public void VerificaDano (string dano)
    {
        
        if (TextoDano != null)
        {

            //cria uma variavel que recebe a posição do jogador/inimigo e inicia nela
            var danoRecebido = Instantiate(TextoDano, transform.position, Quaternion.identity);

            //chama a função TextoSelecionado e passa o dano para ela
            danoRecebido.SendMessage("TextoSelecionado", dano);

        }
    }

}
