using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coracao : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D outro)
    {
        //verifica se é o jogador que está colidindo e se for, cura ele e depois destrói o coração
        if(outro.gameObject.tag == "Player")
        {
            outro.GetComponent<VidaJogador>().Cura();
            Destroy(gameObject);
        }
    }
}
