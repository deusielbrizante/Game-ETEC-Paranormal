using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class ControleSlotsSave : MonoBehaviour
{
    [SerializeField] private Text[] slotSave;
    [SerializeField] private Button continuar;
    [SerializeField] private Button[] botaoSave;

    private string caminhoArquivo;

    private void Awake()
    {

        if (continuar != null)
        {

            continuar.gameObject.SetActive(false);

        }

        MostrarTextoSave();

    }

    public void MostrarTextoSave()
    {

        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local onde ser� salvo
        string localSave = Application.persistentDataPath;

        for (int i = 0; i < slotSave.Length; i++)
        {

            caminhoArquivo = Path.Combine(localSave + $"/EtecParanormal{i}.save");

            if (File.Exists(caminhoArquivo))
            {

                using (FileStream arquivo = File.Open(caminhoArquivo, FileMode.Open))
                {

                    SaveJogo CarregarJogo = (SaveJogo)formatoBinario.Deserialize(arquivo);

                    if (slotSave[i].text != null)
                    {

                        slotSave[i].text = CarregarJogo.UltimaCenaSalva;
                        Debug.Log(CarregarJogo.UltimaCenaSalva);

                        if (continuar != null)
                        {

                            continuar.gameObject.SetActive(true);

                        }

                    }

                }

            }
            else
            {
                slotSave[i].text = $"Slot {i} Vazio";
            }
        }

        MostrarBotao();
    }

    private void MostrarBotao()
    {
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local onde ser� salvo
        string localSave = Application.persistentDataPath;

        for (int i = 0; i < botaoSave.Length; i++)
        {

            caminhoArquivo = Path.Combine(localSave + $"/EtecParanormal{i}.save");

            if (File.Exists(caminhoArquivo))
            {

                using (FileStream arquivo = File.Open(caminhoArquivo, FileMode.Open))
                {

                    SaveJogo CarregarJogo = (SaveJogo)formatoBinario.Deserialize(arquivo);

                    botaoSave[i].interactable = true;

                    if (continuar != null)
                    {

                        continuar.gameObject.SetActive(true);

                    }

                }

            }
            else
            {
                botaoSave[i].interactable = false;
            }
        }
    }
}
