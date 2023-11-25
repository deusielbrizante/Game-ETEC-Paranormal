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
    //pain�is de controle
    [SerializeField] private GameObject painelDePausa;
    [SerializeField] private GameObject painelDeAudio;
    
    //nome da cena atual
    [SerializeField] private string cena;

    //vari�vel para controle de salvamento
    [SerializeField] private ControleDeSalvamento controleDeSave;
    
    //bot�es do menu
    [SerializeField] private GameObject botaoRetomar;
    [SerializeField] private GameObject botaoVoltar;
    
    //vari�vel para o controle de celular
    [SerializeField] private GameObject telaCelular;

    //vari�veis locais de controle
    private GameObject ultimoBotaoSelecionado;
    private string[] joystickName;
    private bool estaPausado;
    private bool conectouControle;
    
    private void Start()
    {
        //define as telas de pause como desativadas e o estado de pause como desativado
        painelDeAudio.SetActive(false);
        painelDePausa.SetActive(false);
        estaPausado = false;

        //define o tempo como corrido e desativa e trava o mouse
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //vari�vel para verificar se h� controle
        joystickName = Input.GetJoystickNames();

        //verifica��o para ver se ir� deixar ativo ou desativo a tela do celular
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
        //vari�vel para verificar se h� controle
        joystickName = Input.GetJoystickNames();

        //vari�vel local de controle
        bool algumControleConectado = false;

        //verifica se h� algum controle conectado e instancia
        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                algumControleConectado = true;
                break;
            }
        }

        //verifica se n�o tem controle e se est� pausado
        if (algumControleConectado == false && estaPausado)
        {
            //define que n�o conectou o controle
            conectouControle = false;
            
            //define como n�o selecionado o bot�o do menu
            if(ultimoBotaoSelecionado != null)
            {
                ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);
            }
        }
        
        //verifica se est� pausado, se h� algum controle e se ele est� conectado
        if(estaPausado && algumControleConectado && conectouControle == false)
        {
            //seleciona o bot�o retomar e avisa que conectou o controle na cena
            EventSystem.current.SetSelectedGameObject(botaoRetomar);
            conectouControle = true;
        }

        //verifica se h� algum controle para adicionar o �ltimo bot�o selecionado da cena
        if(joystickName.Length > 0)
        {
            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;
        }
    }

    //verifica se o bot�o de pause foi selecionado para pausar o jogo
    public void VerificaPause(InputAction.CallbackContext botao)
    {
        if (botao.performed)
        {
            PausarJogo();
        }
    }

    //fun��o de pausar/despausar o jogo
    public void PausarJogo()
    {
        //se estiver pausado, retoma o jogo, sen�o pausar�
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

    //fun��o para retornar ao menu
    public void Menu()
    {
        SceneManager.LoadScene(cena);
    }

    //fun��o para abrir a tela de �udio
    public void AbrirAudio() 
    {
        //define a tela de pause como falsa e ativa a tela de �udio
        painelDePausa.SetActive(false);
        painelDeAudio.SetActive(true);

        //verifica se tem algum controle para selecionar o bot�o
        if(joystickName.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botaoVoltar);
        }
    }

    //verifica se o bot�o de voltar foi pressionado para retornar � cena anterior
    public void Voltar(InputAction.CallbackContext botao)
    {
        //verifica se foi clicaco e se est� pausado
        if (botao.performed && estaPausado)
        {
            //cria uma vari�vel local do bot�o com a tag voltar da cena atual
            Button botaoVoltar = GameObject.FindWithTag("voltar").GetComponent<Button>();

            //verifica se n�o � nulo antes de clicar nele 
            if (botaoVoltar != null)
            {
                botaoVoltar.onClick.Invoke();
            }
        }
    }

    //fun��o para fechar o �udio
    public void FecharAudio()
    {
        //fecha a tela de �udio e ativa a tela de pause
        painelDeAudio.SetActive(false);
        painelDePausa.SetActive(true);

        //verifica se tem controle para selecionar o bot�o retomar
        if(joystickName.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botaoRetomar);
        }
    }

    //corrotina de pausar
    private IEnumerator Pausar()
    {
        //espera 0,2s para n�o causar bugs
        yield return new WaitForSecondsRealtime(0.2f);

        //ativa a tela de pause, define o estado de pause como verdadeiro e destrava o mouse
        painelDePausa.SetActive(true);
        estaPausado = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
            
    //corrotina para voltar ao jogo
    private IEnumerator RetomarJogo()
    {
        //espera 0,2s para n�o causar bugs
        yield return new WaitForSecondsRealtime(0.2f);

        //define a tela de pause como falsa, define o estado de pause como falso e trava o mouse
        painelDePausa.SetActive(false);
        estaPausado = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
