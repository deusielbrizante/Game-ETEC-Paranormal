using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AtivaDesativaControle : MonoBehaviour
{

    private GameObject ultimoBotaoSelecionado;
    private string[] joystickName;
    private bool conectouControle;

    private void Update()
    {

        joystickName = Input.GetJoystickNames();

        bool algumControleConectado = false;

        foreach (string joystickNam in joystickName)
        {
            if (!string.IsNullOrEmpty(joystickNam))
            {
                algumControleConectado = true;
                Debug.Log("achou o controle");
                break;
            }
        }

        if (algumControleConectado == false)
        {
            conectouControle = false;

            if (ultimoBotaoSelecionado != null)
            {

                ultimoBotaoSelecionado.GetComponent<Selectable>().OnDeselect(null);

            }

        }

        if (algumControleConectado && conectouControle == false)
        {

            GameObject botaoTela = GameObject.FindWithTag("selecionavel");
            EventSystem.current.SetSelectedGameObject(botaoTela);
            Debug.Log(botaoTela.name);
            conectouControle = true;

        }

        if (joystickName.Length > 0)
        {

            ultimoBotaoSelecionado = EventSystem.current.currentSelectedGameObject;

        }

        Debug.Log(algumControleConectado);
    }
}
