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

    private float sensibilidade = 0.001f;

    private void Awake()
    {

        if (PlayerPrefs.HasKey("audio"))
        {
            controleVolume.value = PlayerPrefs.GetFloat("audio");
        }

    }

    private void Update()
    {
        // Atualiza o volume e o texto em tempo real
        float volume = controleVolume.value;
        origemAudio.volume = volume;

        if(somGrilo != null)
        {
            somGrilo.volume = volume;
        }
        
        AtualizarTextoPorcentagem(volume);
        PlayerPrefs.SetFloat("audio", volume);

        float entrada = Input.GetAxis("Horizontal");
        float novoValor = controleVolume.value + entrada * sensibilidade;
        controleVolume.value = Mathf.Clamp(novoValor, controleVolume.minValue, controleVolume.maxValue);
    }

    private void AtualizarTextoPorcentagem(float volume)
    {
        // Atualiza o texto exibindo a porcentagem atual
        int porcentagem = (int)(volume * 100);

        textoPorcentagem.text = porcentagem.ToString() + "%";
    }
}