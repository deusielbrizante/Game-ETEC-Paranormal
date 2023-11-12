using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bau : MonoBehaviour
{

    public bool abriuBau = false;
    private int pegouChave;
    private bool estaPerto = false;
    private BarraDeXP xpJogador;
    private Personagem chaves;
    private bool clicou = false;

    [SerializeField] private Animator animacao;
    [SerializeField] private GameObject coracao;
    [SerializeField] private GameObject chavePorta;

    private void Start()
    {
        if (abriuBau)
        {
            animacao.enabled = true;
            clicou = true;
        }
    }

    public void AbrirBau(InputAction.CallbackContext botao)
    {

        if(chaves != null)
        {

            pegouChave = chaves.TemChaveBau;

            if (botao.performed && pegouChave >= 1 && estaPerto && !clicou)
            {

                animacao.enabled = true;
                StartCoroutine(JogarCoracao());
                StartCoroutine(JogarChave());
                abriuBau = true;
                clicou = true;

            }
        }
    }

    private void OnCollisionStay2D(Collision2D outro)
    {
        if (!abriuBau)
        {
            if (outro.gameObject.tag == "Player")
            {

                chaves = outro.gameObject.GetComponent<Personagem>();
                xpJogador = outro.gameObject.GetComponent<BarraDeXP>();
                estaPerto = true;

            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        estaPerto = false;
    }

    private IEnumerator JogarCoracao()
    {
        yield return new WaitForSeconds(0.15f);
        coracao.SetActive(true);
        xpJogador.AdicionaXP(30f);
        chaves.TemChaveBau -= 1;

        if(chaves.TemChaveBau == 0)
        {
            chaves.chaveBau.enabled = false;
        }

        PlayerPrefs.SetInt("chaveBau", chaves.TemChaveBau);
        PlayerPrefs.Save();
    }

    private IEnumerator JogarChave()
    {
        yield return new WaitForSeconds(0.15f);
        chavePorta.SetActive(true);
    }
}
