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
    //painéis de controle
    [SerializeField] private GameObject painelDePausa;
    [SerializeField] private GameObject painelDeAudio;
    
    //nome da cena atual
    [SerializeField] private string cena;

    //variável para controle de salvamento
    [SerializeField] private ControleDeSalvamento controleDeSave;
    
    //botões do menu
    [SerializeField] private GameObject botaoRetomar;
    [SerializeField] private GameObject botaoVoltar;
    
    //variável para o controle de celular
    [SerializeField] private GameObject telaCelular;

    //variáveis locais de controle
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

        //variável para verificar se há controle
        joystickName = Input.GetJoystickNames();

        //verificação para ver se irá deixar ativo ou desativo a tela do celular
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
        //variável para verificar se há controle
        joystickName = Input.GetJoystickNames();

        //variável local de controle
        bool algumControleConectado = false;

        //verifica se há algum controle conectado e instancia
        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                algumControleConectado = true;
                break;
            }
        }

        //verifica se não tem controle e se está pausado
        if (algumControleConectado == false && estaPausado)
        {
            //define que não conectou o controle
            conectouControle = false;
            
            //define como não selecionado o botão do menu
            if(ultimoBotaoSelecionado != null)
            {
                ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);
            }
        }
        
        //verifica se está pausado, se há algum controle e se ele está conectado
        if(estaPausado && algumControleConectado && conectouControle == false)
        {
            //seleciona o botão retomar e avisa que conectou o controle na cena
            EventSystem.current.SetSelectedGameObject(botaoRetomar);
            conectouControle = true;
        }

        //verifica se há algum controle para adicionar o último botão selecionado da cena
        if(joystickName.Length > 0)
        {
            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;
        }
    }

    //verifica se o botão de pause foi selecionado para pausar o jogo
    public void VerificaPause(InputAction.CallbackContext botao)
    {
        if (botao.performed)
        {
            PausarJogo();
        }
    }

    //função de pausar/despausar o jogo
    public void PausarJogo()
    {
        //se estiver pausado, retoma o jogo, senão pausará
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

    //função para retornar ao menu
    public void Menu()
    {
        SceneManager.LoadScene(cena);
    }

    //função para abrir a tela de áudio
    public void AbrirAudio() 
    {
        //define a tela de pause como falsa e ativa a tela de áudio
        painelDePausa.SetActive(false);
        painelDeAudio.SetActive(true);

        //verifica se tem algum controle para selecionar o botão
        if(joystickName.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botaoVoltar);
        }
    }

    //verifica se o botão de voltar foi pressionado para retornar à cena anterior
    public void Voltar(InputAction.CallbackContext botao)
    {
        //verifica se foi clicaco e se está pausado
        if (botao.performed && estaPausado)
        {
            //cria uma variável local do botão com a tag voltar da cena atual
            Button botaoVoltar = GameObject.FindWithTag("voltar").GetComponent<Button>();

            //verifica se não é nulo antes de clicar nele 
            if (botaoVoltar != null)
            {
                botaoVoltar.onClick.Invoke();
            }
        }
    }

    //função para fechar o áudio
    public void FecharAudio()
    {
        //fecha a tela de áudio e ativa a tela de pause
        painelDeAudio.SetActive(false);
        painelDePausa.SetActive(true);

        //verifica se tem controle para selecionar o botão retomar
        if(joystickName.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botaoRetomar);
        }
    }

    //corrotina de pausar
    private IEnumerator Pausar()
    {
        //espera 0,2s para não causar bugs
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
        //espera 0,2s para não causar bugs
        yield return new WaitForSecondsRealtime(0.2f);

        //define a tela de pause como falsa, define o estado de pause como falso e trava o mouse
        painelDePausa.SetActive(false);
        estaPausado = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
