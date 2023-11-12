using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coracao : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D outro)
    {
        if(outro.gameObject.tag == "Player")
        {
            outro.GetComponent<VidaJogador>().Cura();
            Destroy(gameObject);
        }
    }
}
