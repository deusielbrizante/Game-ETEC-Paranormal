using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Data.Common;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        joystickName = Input.GetJoystickNames();

        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                if(telaCelular != null)
                {
                    telaCelular.SetActive(true);
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

            if(telaCelular != null)
            {
                telaCelular.SetActive(false);
            }
            
            if(ultimoBotaoSelecionado != null)
            {
                
                ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);

            }

        }
        
        if(estaPausado && algumControleConectado && conectouControle == false)
        {

            EventSystem.current.SetSelectedGameObject(botaoRetomar);
            conectouControle = true;
        
            if(telaCelular != null)
            {
                telaCelular.SetActive(true);
            }

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

        if(painelDeAudio.activeSelf && Input.GetJoystickNames().Length > 0)
        {
            return;
        }
        else if(painelDeAudio.activeSelf && Input.GetJoystickNames().Length == 0)
        {
            Button botaoVoltar = GameObject.FindWithTag("voltar").GetComponent<Button>();

            if (botaoVoltar != null)
            {
                botaoVoltar.onClick.Invoke();
            }
        }
        else
        {
            if (estaPausado)
            {
                painelDePausa.SetActive(false);
                estaPausado = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if(telaCelular != null)
                {
                    telaCelular.SetActive(true);
                }
            }
            else
            {
                painelDePausa.SetActive(true);
                estaPausado = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if(telaCelular != null)
                {
                    telaCelular.SetActive(false);
                }
            }
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
}
