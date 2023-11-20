using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AtivaDesativaControle : MonoBehaviour
{

    //instancia as variáveis globais
    private GameObject ultimoBotaoSelecionado;
    private string[] joystickName;
    private bool conectouControle;

    private void Update()
    {

        //recebe os joysticks conectados e coloca em um array
        joystickName = Input.GetJoystickNames();

        //cria uma variável para identificar quando o controle estará conectado
        bool algumControleConectado = false;

        //faz uma varredura na lista e vê qual objeto da lista não é nulo e define como conectado
        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                algumControleConectado = true;
                break;
            }
        }

        //verifica se algum controle não está conectado e desseleciona o botão do controle se não for nulo
        if (algumControleConectado == false)
        {
            conectouControle = false;

            if (ultimoBotaoSelecionado != null)
            {

                ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);

            }

        }

        //verifica se tem algum controle conectado antes de instanciar que iniciou o controle, define as variável de botão da tela e seleciona ele
        if (algumControleConectado && conectouControle == false)
        {

            GameObject botaoTela = GameObject.FindWithTag("selecionavel");
            EventSystem.current.SetSelectedGameObject(botaoTela);
            conectouControle = true;

        }

        //verifica se há algun controle e instancia o último botão, que é o botão atual selecionado
        if (joystickName.Length > 0)
        {

            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;

        }
    }
}
