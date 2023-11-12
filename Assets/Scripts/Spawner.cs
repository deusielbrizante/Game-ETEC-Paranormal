using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //variaveis ded tempo, objeto e quantidade
    [SerializeField] private GameObject inimigo;
    [SerializeField] private int limiteSpawn;
    [SerializeField] private int tempoEsperaInicial;
    
    public int NumeroAtual = 0;
    private bool instanciado = false;
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
        if (instanciado && mob == null && NumeroAtual <= limiteSpawn)
        {

            NumeroAtual++;
            instanciado = false;
            Debug.Log("morreu");
        }

        if (jogadorPerto)
        {

            if (iniciarSpawn == true && NumeroAtual < limiteSpawn && mob == null)
            {

                mob = Instantiate(inimigo, transform.position, inimigo.transform.rotation);
                instanciado = true;
                Debug.Log(instanciado);
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
