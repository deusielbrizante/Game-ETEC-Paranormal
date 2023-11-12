using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaveSala : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D outro)
    {
        
        if(outro.gameObject.tag == "Player")
        {

            outro.gameObject.GetComponent<Personagem>().chavePorta.enabled = true;
            outro.gameObject.GetComponent<Personagem>().TemChavePorta = true;

            PlayerPrefs.SetInt("chaveSala", 1);
            PlayerPrefs.Save();
            
            Destroy(gameObject);

        }

    }
}
