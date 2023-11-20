using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class ControleSlotsSave : MonoBehaviour
{
    //define as variáveis do inspetor da tela do save
    [SerializeField] private Text[] slotSave;
    [SerializeField] private Button continuar;
    [SerializeField] private Button[] botaoSave;

    //define o caminho padrão de save dos arquivos
    private string caminhoArquivo;

    private void Awake()
    {
        //verifica se o continuar não é nulo para desativá-lo no começo
        if (continuar != null)
        {
            continuar.gameObject.SetActive(false);
        }

        //atualiza o texto dos saves
        MostrarTextoSave();
    }

    public void MostrarTextoSave()
    {
        //formato do arquivo
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local padrão onde é salvo
        string localSave = Application.persistentDataPath;

        //verifica para cada slot de save se ele existe e se existir, altera o nome para o nome da fase que está
        for (int i = 0; i < slotSave.Length; i++)
        {
            caminhoArquivo = Path.Combine(localSave + $"/EtecParanormal{i}.save");

            //verifica se o caminho do slot do save existe
            if (File.Exists(caminhoArquivo))
            {
                //se o arquivo existir, usa o caminho dele
                using (FileStream arquivo = File.Open(caminhoArquivo, FileMode.Open))
                {
                    //descriptografa o arquivo
                    SaveJogo CarregarJogo = (SaveJogo)formatoBinario.Deserialize(arquivo);

                    //verifica se o texto desse slot não é nulo para poder alterar o nome dele para o da fase
                    if (slotSave[i].text != null)
                    {
                        slotSave[i].text = CarregarJogo.UltimaCenaSalva;

                        //verifica se o botão continuar não é nulo para ativá-lo
                        if (continuar != null)
                        {
                            continuar.gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                //define como slot vazio caso não hava save naquele slot
                slotSave[i].text = $"Slot {i} Vazio";
            }
        }

        MostrarBotao();
    }

    private void MostrarBotao()
    {
        //formato do arquivo
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local padrão onde é salvo
        string localSave = Application.persistentDataPath;

        //verifica para cada botão dos slots de save se ele existe e se existir, deixa ele interativo
        for (int i = 0; i < botaoSave.Length; i++)
        {
            caminhoArquivo = Path.Combine(localSave + $"/EtecParanormal{i}.save");

            //verifica se o caminho do slot do save existe
            if (File.Exists(caminhoArquivo))
            {
                //se o arquivo existir, usa o caminho dele
                using (FileStream arquivo = File.Open(caminhoArquivo, FileMode.Open))
                {
                    //deixa o botão na posição atual interativo caso exista
                    SaveJogo CarregarJogo = (SaveJogo)formatoBinario.Deserialize(arquivo);
                    botaoSave[i].interactable = true;

                    //verifica se o botão continuar não é nulo para ativá-lo
                    if (continuar != null)
                    {
                        continuar.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                //define como interativo caso não haja save naquela posição do slot
                botaoSave[i].interactable = false;
            }
        }
    }
}
