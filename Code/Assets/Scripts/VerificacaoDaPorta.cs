using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VerificacaoDaPorta : MonoBehaviour
{

    [SerializeField] private string proximaCena;
    [SerializeField] private GameObject menu;
    [SerializeField] private Tilemap portaFechada;
    [SerializeField] private TileBase portaAberta;
    [SerializeField] private Vector3Int posicaoTile;

    private bool podeAbrir = false;
    private bool podeCarregarCena = false;
    private bool clicou = false;

    public void AbrirPorta(InputAction.CallbackContext botao)
    {
        if (botao.performed && podeAbrir)
        {
            if (portaAberta != null)
            {

                portaFechada.SetTile(posicaoTile, portaAberta);
                clicou = true;

            }
        }

        if (podeCarregarCena)
        {

            StartCoroutine(SalvarEMudarDeCena());

        }
    }

    private void OnTriggerStay2D(Collider2D outro)
    {
        
        if(outro.gameObject.tag == "Player")
        {

            if (outro.gameObject.GetComponent<Personagem>().TemChavePorta == true)
            {
                podeAbrir = true;
            }

            if(clicou)
            {

                if(outro.gameObject.GetComponent<Personagem>().TemChavePorta == false && outro.gameObject.GetComponent<Personagem>().TemChaveBau == 0)
                {
                    podeCarregarCena = true;
                }
                else
                {
                    outro.gameObject.GetComponent<Personagem>().TemChavePorta = false;
                    outro.gameObject.GetComponent<Personagem>().TemChaveBau = 0;
                }

            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        podeAbrir = false;

    }

    private IEnumerator SalvarEMudarDeCena()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;

        PlayerPrefs.SetString("cenaSalva", proximaCena);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("cenaSalva"));

        PlayerPrefs.SetInt("valorVerdadeiro", 1);
        PlayerPrefs.Save();
        
        menu.GetComponent<ControleDeSalvamento>().SalvarJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));

        SceneManager.LoadScene(proximaCena);

    }
}
