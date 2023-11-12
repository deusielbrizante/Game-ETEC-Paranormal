using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDosBoss : MonoBehaviour
{
    //variaveis ded tempo, objeto e quantidade
    [SerializeField] private GameObject inimigo1;
    [SerializeField] private GameObject inimigo2;
    [SerializeField] private int limiteSpawn;
    [SerializeField] private int tempoEsperaInicial;
    
    public int NumeroAtual = 0;
    private bool instaciouUmaVez = false;
    private GameObject mob;

    //variaveis para iniciar
    private bool iniciarSpawn = false;
    private bool jogadorPerto = false;

    private void Start()
    {

        StartCoroutine(InicioComAtraso());

    }

    private void Update()
    {
        if (jogadorPerto)
        {

            if (iniciarSpawn == true && NumeroAtual < limiteSpawn && mob == null)
            {

                if(instaciouUmaVez)
                {
                    mob = Instantiate(inimigo2, transform.position, inimigo2.transform.rotation);
                    NumeroAtual++;
                }
                else
                {
                    mob = Instantiate(inimigo1, transform.position, inimigo1.transform.rotation);
                    NumeroAtual++;
                }

                instaciouUmaVez = true;

            }
            else
            {
                return;
            }

        }
    }

    private IEnumerator InicioComAtraso()
    {
        yield return new WaitForSeconds(tempoEsperaInicial);
        iniciarSpawn = true;
    }

    private void OnTriggerStay2D(Collider2D outro)
    {
        if(outro.gameObject.tag == "Player")
        {

            jogadorPerto = true;

        }
    }

    private void OnTriggerExit2D(Collider2D outro)
    {
        
        if(outro.gameObject.tag == "Player")
        {
            jogadorPerto = false;
        }

    }
}
