using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuTroca : MonoBehaviour
{
    //variaveis dos menus
    [SerializeField] private string sala;
    [SerializeField] private GameObject telaParaSalvar;
    [SerializeField] private GameObject telaParaCarregar;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject menuOpcoes;
    [SerializeField] private GameObject menuPersonagens;
    [SerializeField] private GameObject telaTutorialPC;
    [SerializeField] private GameObject telaTutorialControle;

    [Header("Mudan�a de Bot�es")]
    [SerializeField] private GameObject iniciar;
    [SerializeField] private GameObject som;
    [SerializeField] private GameObject Saveslot1;
    [SerializeField] private GameObject primeiroPersonagem;
    [SerializeField] private GameObject slot1;

    
    private bool PersonagemSelecionado;
    private ControleSlotsSave controleSlot;


    private void Start()
    {

        controleSlot = FindObjectOfType<ControleSlotsSave>();

        //inicia o menu corretamente
        DesligarTodasAsTelas();
        menuInicial.SetActive(true);
        Time.timeScale = 1;
    
    }

    public void Jogar()
    {

        if (GetComponent<SelecionarPersonagem>() != null)
        {
            PersonagemSelecionado = GetComponent<SelecionarPersonagem>().PersonagemSelecionado;
        }

        if (PersonagemSelecionado)
        {
            DesligarTodasAsTelas();
            telaParaSalvar.SetActive(true);
            controleSlot.MostrarTextoSave();
            EventSystem.current.SetSelectedGameObject(slot1);
        }
        else
        {
            return;
        }

    }

    public void Carregar()
    {

        DesligarTodasAsTelas();
        telaParaCarregar.SetActive(true);
        controleSlot.MostrarTextoSave();
        EventSystem.current.SetSelectedGameObject(Saveslot1);

    }

    public void Inicar()
    {   
        StartCoroutine(TelaTutorialControleEPC());
    }

    public void AbrirEscolhaPersonagens() 
    {

        DesligarTodasAsTelas();
        menuPersonagens.SetActive(true);
        EventSystem.current.SetSelectedGameObject(primeiroPersonagem);

    }

    public void AbrirOpcoes() 
    {

        DesligarTodasAsTelas();
        menuOpcoes.SetActive(true);
        EventSystem.current.SetSelectedGameObject(som);

    }

    public void VoltarAoMenu() 
    {

        DesligarTodasAsTelas();
        menuInicial.SetActive(true);
        EventSystem.current.SetSelectedGameObject(iniciar);

    }

    public void SairJogo() 
    {

        Debug.Log("Sair do jogo");
        Application.Quit();

    }

    public void DesligarTodasAsTelas()
    {
        menuInicial.SetActive(false);
        menuOpcoes.SetActive(false);
        menuPersonagens.SetActive(false);
        telaParaSalvar.SetActive(false);
        telaParaCarregar.SetActive(false);
    }

    private IEnumerator TelaTutorialControleEPC()
    {
        DesligarTodasAsTelas();

        string[] joystickName = Input.GetJoystickNames();
        bool temControle = false;

        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                temControle = true;
                Debug.Log("achou o controle");
                break;
            }
        }

        if (temControle)
        {
            telaTutorialControle.SetActive(true);
        }
        else
        {
            telaTutorialPC.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Fase1");
    }

    public void Voltar(InputAction.CallbackContext botao)
    {

        if (botao.performed)
        {

            Button botaoVoltar = GameObject.FindWithTag("voltar").GetComponent<Button>();

            if (botaoVoltar != null)
            {

                botaoVoltar.onClick.Invoke();

            }

        }

    }

}
