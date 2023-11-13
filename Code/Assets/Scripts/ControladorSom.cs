using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControladorSom : MonoBehaviour
{
    [SerializeField] private Slider controleVolume; // Refer�ncia ao controle deslizante da barra de som
    [SerializeField] private Text textoPorcentagem; // Refer�ncia ao texto que exibir� a porcentagem
    [SerializeField] private AudioSource origemAudio; // Refer�ncia ao componente de �udio que deseja controlar
    [SerializeField] private AudioSource somGrilo;
    [SerializeField] private GameObject telaAudio;

    private float sensibilidade = 0.001f;
    private bool resetInputs = false;

    private void Start()
    {

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
        // Atualiza o volume e o texto em tempo real
        if(telaAudio != null)
        {

            resetInputs = false;

            if(telaAudio.activeSelf)
            {

                if(resetInputs == false)
                {
                    Input.ResetInputAxes();
                    resetInputs = true;
                }

                float entrada = Input.GetAxis("Horizontal");
                float novoValor = controleVolume.value + entrada * sensibilidade;
                controleVolume.value = Mathf.Clamp(novoValor, controleVolume.minValue, controleVolume.maxValue);
                
                float volume = controleVolume.value;
                origemAudio.volume = volume;

                if(somGrilo != null)
                {
                    somGrilo.volume = volume;
                }
                
                AtualizarTextoPorcentagem(volume);
                PlayerPrefs.SetFloat("audio", volume);

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