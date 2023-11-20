using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaveBau : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D outro)
    {
        //verifica se é o jogador que está colidindo com a chave
        if(outro.gameObject.tag == "Player")
        {
            //define as variáveis da imagem da chave e aumenta a quantidade de chaves
            outro.gameObject.GetComponent<Personagem>().chaveBau.enabled = true;
            outro.gameObject.GetComponent<Personagem>().TemChaveBau += 1;

            //destrói a chave
            Destroy(gameObject);
        }
    }
}
