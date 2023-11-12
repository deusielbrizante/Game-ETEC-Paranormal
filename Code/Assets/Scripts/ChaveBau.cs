using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaveBau : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D outro)
    {
        
        if(outro.gameObject.tag == "Player")
        {

            outro.gameObject.GetComponent<Personagem>().chaveBau.enabled = true;
            outro.gameObject.GetComponent<Personagem>().TemChaveBau += 1;
            
            PlayerPrefs.SetInt("chaveBau", outro.gameObject.GetComponent<Personagem>().TemChaveBau);
            PlayerPrefs.Save();

            Destroy(gameObject);

        }

    }
}
