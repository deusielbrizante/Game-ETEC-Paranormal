using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Data.Common;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Controls;

public class Pause : MonoBehaviour
{

    private GameObject ultimoBotaoSelecionado;
    private string[] joystickName;
    private bool estaPausado;
    private bool conectouControle;
    [SerializeField] private GameObject painelDePausa;
    [SerializeField] private GameObject painelDeAudio;
    [SerializeField] private string cena;
    [SerializeField] private ControleDeSalvamento controleDeSave;
    [SerializeField] private GameObject botaoRetomar;
    [SerializeField] private GameObject botaoVoltar;
    [SerializeField] private GameObject telaCelular;

    private void Start()
    {

        painelDeAudio.SetActive(false);
        painelDePausa.SetActive(false);
        estaPausado = false;
        Time.timeScale = 1;
/*        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        joystickName = Input.GetJoystickNames();

        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                if(telaCelular != null)
                {
                    telaCelular.SetActive(false);
                }

                break;
            }
        }
    }

    private void Update()
    {

        joystickName = Input.GetJoystickNames();

        bool algumControleConectado = false;

        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                algumControleConectado = true;
                break;
            }
        }

        if (algumControleConectado == false && estaPausado)
        {
            conectouControle = false;
            
            if(ultimoBotaoSelecionado != null)
            {
                ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);
            }

        }
        
        if(estaPausado && algumControleConectado && conectouControle == false)
        {

            EventSystem.current.SetSelectedGameObject(botaoRetomar);
            conectouControle = true;

        }

        if(joystickName.Length > 0)
        {

            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;
        
        }
    }

    public void VerificaPause(InputAction.CallbackContext botao)
    {

        if (botao.performed)
        {
            PausarJogo();
        }

    }

    public void PausarJogo()
    {
        if (estaPausado)
        {
            Time.timeScale = 1;
            StartCoroutine(RetomarJogo());
        }
        else
        {
            Time.timeScale = 0;
            StartCoroutine(Pausar());
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(cena);
    }

    public void AbrirAudio() 
    {

        painelDePausa.SetActive(false);
        painelDeAudio.SetActive(true);

        if(joystickName.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botaoVoltar);
        }
    }

    public void Voltar(InputAction.CallbackContext botao)
    {

        if (botao.performed && estaPausado)
        {

            Button botaoVoltar = GameObject.FindWithTag("voltar").GetComponent<Button>();

            if (botaoVoltar != null)
            {
                botaoVoltar.onClick.Invoke();
            }

        }

    }

    public void FecharAudio()
    {
        painelDeAudio.SetActive(false);
        painelDePausa.SetActive(true);

        if(joystickName.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botaoRetomar);
        }
    }

    private IEnumerator Pausar()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        painelDePausa.SetActive(true);
        estaPausado = true;
/*        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;*/
    }
            
    private IEnumerator RetomarJogo()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        painelDePausa.SetActive(false);
        estaPausado = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
