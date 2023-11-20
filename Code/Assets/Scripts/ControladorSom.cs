using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControladorSom : MonoBehaviour
{
    // Referência ao controle deslizante da barra de som
    [SerializeField] private Slider controleVolume;

    // Referência ao texto que exibirá a porcentagem
    [SerializeField] private Text textoPorcentagem; 

    // Referência aos componentes de áudio
    [SerializeField] private AudioSource origemAudio; 
    [SerializeField] private AudioSource somGrilo;

    //referência à tela de áudio
    [SerializeField] private GameObject telaAudio;

    //sensibilidade do controle ao mudar o som
    private float sensibilidade = 0.001f;

    //variável para reiniciar a entrada dos controles
    private bool resetInputs = false;

    private void Start()
    {
        //instancia o valor do áudio caso tenha algum save
        if (PlayerPrefs.HasKey("audio"))
        {
            controleVolume.value = PlayerPrefs.GetFloat("audio");
            origemAudio.volume = controleVolume.value;

            if(somGrilo != null)
            {
                somGrilo.volume = controleVolume.value;
            }
        }
    }

    private void Update()
    {
        //verifica se a tela não é nula
        if(telaAudio != null)
        {
            //verifica se a tela de áudio está ativa
            if(telaAudio.activeSelf)
            {
                //verifica se o reinício dos controles é falso, se for reinicia eles e define como verdadeiro
                if(resetInputs == false)
                {
                    Input.ResetInputAxes();
                    resetInputs = true;
                }

                //define as entradas que irão mudar o valor do som e vai atualizando o novo valor
                float entrada = Input.GetAxis("Horizontal");
                float novoValor = controleVolume.value + entrada * sensibilidade;
                controleVolume.value = Mathf.Clamp(novoValor, controleVolume.minValue, controleVolume.maxValue);
                
                //define o volume atual que irá ser instanciado
                float volume = controleVolume.value;
                origemAudio.volume = volume;

                //verifica se os sons dos grilos não são nulos para deixar com o mesmo volume
                if(somGrilo != null)
                {
                    somGrilo.volume = volume;
                }
                
                //atualiza o texto e salva o novo valor do áudio
                AtualizarTextoPorcentagem(volume);
                PlayerPrefs.SetFloat("audio", volume);
            }
            else
            {
                //instancia o reinício dos controles como falso
                resetInputs = false;
            }
        }
    }

    private void AtualizarTextoPorcentagem(float volume)
    {
        // Atualiza o texto exibindo a porcentagem atual
        int porcentagem = (int)(volume * 100);
        textoPorcentagem.text = porcentagem.ToString() + "%";
    }
}