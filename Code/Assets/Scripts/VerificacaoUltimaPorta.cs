using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VerificacaoUltimaPorta : MonoBehaviour
{

    [SerializeField] private GameObject telaWin;
    [SerializeField] private GameObject telaPause;
    [SerializeField] private Tilemap portaFechada;
    [SerializeField] private TileBase portaAberta;
    [SerializeField] private Vector3Int posicaoTile;

    private bool podeAbrir = false;

    public void AbrirPorta(InputAction.CallbackContext botao)
    {
        if (botao.performed && podeAbrir)
        {
            if (portaAberta != null)
            {
                portaFechada.SetTile(posicaoTile, portaAberta);
                telaWin.SetActive(true);
                telaPause.SetActive(false);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        podeAbrir = false;
    }
}
