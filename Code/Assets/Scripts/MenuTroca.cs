using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuTroca : MonoBehaviour
{
    //variaveil da sala
    [SerializeField] private string sala;

    //variável das telas
    [SerializeField] private GameObject telaParaSalvar;
    [SerializeField] private GameObject telaParaCarregar;
    [SerializeField] private GameObject telaTutorialPC;
    [SerializeField] private GameObject telaTutorialControle;

    //variáveis dos menus
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject menuOpcoes;
    [SerializeField] private GameObject menuPersonagens;

    //botões para o controle
    [Header("Mudança de Botões")]
    [SerializeField] private GameObject iniciar;
    [SerializeField] private GameObject som;
    [SerializeField] private GameObject Saveslot1;
    [SerializeField] private GameObject primeiroPersonagem;
    [SerializeField] private GameObject slot1;

    //variáveis para verificar o estado
    private bool TemControle;
    private bool PersonagemSelecionado;
    
    //variável do controle dos slots
    private ControleSlotsSave controleSlot;

    private void Start()
    {
        //define no começo o controle slot com o tipo dele
        controleSlot = FindObjectOfType<ControleSlotsSave>();

        //inicia o menu corretamente com todas as telas desligadas menos o menu
        DesligarTodasAsTelas();
        menuInicial.SetActive(true);
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        //verifica se tem algum controle conectado e define na variável de estado
        if(Input.GetJoystickNames().Length > 0)
        {
            TemControle = true;
        }
        else
        {
            TemControle = false;
        }
    }

    //função para chamar a tela de save
    public void Jogar()
    {
        //verifica se o selecionar personagem não é nulo antes de continuar
        if (GetComponent<SelecionarPersonagem>() != null)
        {
            PersonagemSelecionado = GetComponent<SelecionarPersonagem>().PersonagemSelecionado;
        }

        if (PersonagemSelecionado)
        {
            //se o personagem já foi selecionado irá desligar todas as telas, ir para a tela de carregamento e atualizar o texto dos slots de save  
            DesligarTodasAsTelas();
            telaParaSalvar.SetActive(true);
            controleSlot.MostrarTextoSave();

            //se tiver controle ele irá selecionar o slot 1 como padrão
            if (TemControle)
            {
                EventSystem.current.SetSelectedGameObject(slot1);
            }
        }
        else
        {
            return;
        }
    }

    //função para carregar os dados salvados
    public void Carregar()
    {
        //desliga todas as telas, ativa a tela de carregamento e atualiza os textos
        DesligarTodasAsTelas();
        telaParaCarregar.SetActive(true);
        controleSlot.MostrarTextoSave();

        //se tiver controle seleciona o save slot1
        if (TemControle)
        {
            EventSystem.current.SetSelectedGameObject(Saveslot1);
        }
    }

    //função para iniciar a corrotina da tela de tutorial
    public void Inicar()
    {   
        StartCoroutine(TelaTutorialControleEPC());
    }

    //função para abrir a escolha de personagens
    public void AbrirEscolhaPersonagens() 
    {
        //desliga todas as telas e ativa a tela de seleção de personagem
        DesligarTodasAsTelas();
        menuPersonagens.SetActive(true);

        //se tiver controle irá selecionar o primeiro personagem
        if (TemControle)
        {
            EventSystem.current.SetSelectedGameObject(primeiroPersonagem);
        }
    }

    //função para ir para o áudio
    public void AbrirOpcoes() 
    {
        //desliga todas as telas e ativa a tela de áudio
        DesligarTodasAsTelas();
        menuOpcoes.SetActive(true);

        //se tiver controle irá selecionar o som
        if (TemControle)
        {
            EventSystem.current.SetSelectedGameObject(som);
        }
    }

    //função para voltar ao menu
    public void VoltarAoMenu() 
    {
        //desliga todas as telas e ativa o menu
        DesligarTodasAsTelas();
        menuInicial.SetActive(true);

        //verifica se tem controle conectado para selecionar o botão iniciar
        if (TemControle)
        {
            EventSystem.current.SetSelectedGameObject(iniciar);
        }
    }

    //função para sair do jogo
    public void SairJogo() 
    {
        Application.Quit();
    }

    //função para desligar todas as telas
    public void DesligarTodasAsTelas()
    {
        menuInicial.SetActive(false);
        menuOpcoes.SetActive(false);
        menuPersonagens.SetActive(false);
        telaParaSalvar.SetActive(false);
        telaParaCarregar.SetActive(false);
    }

    //corrotina da tela de tutorial
    private IEnumerator TelaTutorialControleEPC()
    {
        //desliga todas as telas
        DesligarTodasAsTelas();

        //variáveis de verificação do controle
        string[] joystickName = Input.GetJoystickNames();
        bool temControle = false;

        //verifica se há algum controle conectado
        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                temControle = true;
                break;
            }
        }

        //se tiver controle ele ativa a tela de tutorial de controle
        if (temControle)
        {
            telaTutorialControle.SetActive(true);
        }
        else
        {
            //senão ele ativa a outra tela
            telaTutorialPC.SetActive(true);
        }

        //espera 5s para a pessoa ver os comando e depois carrega a fase1
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Fase1");
    }

    public void Voltar(InputAction.CallbackContext botao)
    {
        //verifica se o botão voltar foi clicado no controle
        if (botao.performed)
        {
            //se foi clicado irá pegar o botão com a tag voltar
            Button botaoVoltar = GameObject.FindWithTag("voltar").GetComponent<Button>();

            //verifica se esse botão não é nulo e clica nele
            if (botaoVoltar != null)
            {
                botaoVoltar.onClick.Invoke();
            }
        }
    }
}
