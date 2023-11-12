using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

public class ControleDeSalvamento : MonoBehaviour
{

    [SerializeField] private Button botaoIniciar;
    [SerializeField] private GameObject telaDeConfirmacaoSave;
    private GameObject atributosJogador;
    private string caminhoArquivo;
    private bool arquivoExiste;

    [Header("ControleBotoes")]
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject botaoConfirmar;

    //como chamar em outro lugar o save, lembrando de chamar a fun��o e passar o par�metro salvar dentro depois de especificar as altera��es
    // SaveJogo salvar = new SaveJogo();
    
    private void Awake()
    {
        
        if(PlayerPrefs.GetInt("valorVerdadeiro") == 1)
        {
            CarregarComecoJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
        }

    }

    public void SalvarJogo(int slotSelecionado) 
    {

        PlayerPrefs.SetInt("valorVerdadeiro", 0);
        PlayerPrefs.Save();

        SaveJogo salvar = new SaveJogo();

        //vari�vel para formatar em binario
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local onde ser� salvo
        string localSave = Application.persistentDataPath;

        caminhoArquivo = Path.Combine(localSave + $"/EtecParanormal{slotSelecionado}.save");

        Debug.Log("chegou ate aqui antes da tela de confirmacao");
        //verifica se o save j� existe
        if(File.Exists(caminhoArquivo))
        {

            //se existir abre a tela de confirma��o e verifica se quer mesmo sobrepor o save
            if(telaDeConfirmacaoSave != null)
            {

                telaDeConfirmacaoSave.SetActive(true);
                EventSystem.current.SetSelectedGameObject(botaoConfirmar);

            }
            else
            {
                ConfirmaSave();
            }
        }
        else
        {

            using(FileStream arquivo = File.Create(caminhoArquivo))
            {

                SalvarPlayerPrefs(salvar);

                formatoBinario.Serialize(arquivo, salvar);

            }

            if(botaoIniciar != null)
            {

                botaoIniciar.interactable = true;

            }

            FindObjectOfType<ControleSlotsSave>().MostrarTextoSave();

        }

        if (botaoIniciar != null)
        {
            
            botaoIniciar.interactable = true;
        
        }
        
        PlayerPrefs.SetInt("ultimoSlotSelecionado", slotSelecionado);

    }

    public SaveJogo Carregar(int slotSelecionado)
    {

        Debug.Log("chegou aqui");
        arquivoExiste = false;

        PlayerPrefs.SetInt("ultimoSlotSelecionado", slotSelecionado);

        //vari�vel para formatar em binario
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local onde ser� salvo
        string localSave = Application.persistentDataPath;

        FileStream arquivo;
        
        //verifica se o arquivo existe para abri-lo
        if(File.Exists(localSave + "/EtecParanormal" + slotSelecionado + ".save"))
        {

            arquivo = File.Open(localSave + $"/EtecParanormal{slotSelecionado}.save", FileMode.Open);

            SaveJogo CarregarJogo = (SaveJogo)formatoBinario.Deserialize(arquivo);

            arquivo.Close();

            arquivoExiste = true;

            return CarregarJogo;

        }

        return null;

    }

    public void CarregarJogo(int slot)
    {

        PlayerPrefs.SetInt("valorVerdadeiro", 1);
        PlayerPrefs.SetInt("carregarSpawns", 1);
        PlayerPrefs.Save();

        SaveJogo carregar = Carregar(slot);

        CarregarPlayerPrefs(carregar);

        if (arquivoExiste)
        {

            Debug.Log(PlayerPrefs.GetString("cenaSalva"));
            string cena = PlayerPrefs.GetString("cenaSalva");
            SceneManager.LoadScene(cena);

        }
        else
        {
            return;
        }
    }

    public void CarregarComecoJogo(int slot)
    {
        SaveJogo carregar = Carregar(slot);

        CarregarPlayerPrefs(carregar);
    }

    private void SalvarPlayerPrefs(SaveJogo salvar)
    {

        atributosJogador = GameObject.FindWithTag("Player");
        Debug.Log(atributosJogador);
        atributosJogador.GetComponent<SistemaArma>().objTiro.GetComponent<DanoDaBolinha>().SalvarDano();
        atributosJogador.GetComponent<VidaJogador>().SalvarVida();
        atributosJogador.GetComponent<BarraDeXP>().SalvarXP();

        Debug.Log("PlayerPrefs salvos");

        if (PlayerPrefs.HasKey("personagemSelecionado"))
        {
            salvar.PersonagemSelecionado = PlayerPrefs.GetInt("personagemSelecionado");
            Debug.Log($"personagem selecionado: {salvar.PersonagemSelecionado}");
        }

        if (PlayerPrefs.HasKey("cenaSalva"))
        {

            Debug.Log(PlayerPrefs.GetString("cenaSalva"));

            salvar.UltimaCenaSalva = PlayerPrefs.GetString("cenaSalva");
            Debug.Log($"cena salva: {salvar.UltimaCenaSalva}");
        }
        else
        {
            PlayerPrefs.SetString("cenaSalva", "Fase1");
            salvar.UltimaCenaSalva = PlayerPrefs.GetString("cenaSalva");
            Debug.Log($"cena salva: {salvar.UltimaCenaSalva}");
        }

        if (PlayerPrefs.HasKey("vidaAtual"))
        {
            salvar.VidaAtual = PlayerPrefs.GetFloat("vidaAtual");
            Debug.Log($"vida atual: {salvar.VidaAtual}");
        }

        if (PlayerPrefs.HasKey("vidaMaxima"))
        {
            salvar.VidaMaxima = PlayerPrefs.GetFloat("vidaMaxima");
            Debug.Log($"vida maxima: { salvar.VidaMaxima}");
        }

        if (PlayerPrefs.HasKey("menorDano"))
        {
            salvar.DanoMenor = PlayerPrefs.GetFloat("menorDano");
            Debug.Log($"menor dano: {salvar.DanoMenor}");
        }

        if (PlayerPrefs.HasKey("maiorDano"))
        {
            salvar.DanoMaior = PlayerPrefs.GetFloat("maiorDano");
            Debug.Log($"maior dano: {salvar.DanoMaior}");
        }

        if (PlayerPrefs.HasKey("expNivelAtual"))
        {

            salvar.ExpNivelAtual = PlayerPrefs.GetFloat("expNivelAtual");
            Debug.Log($"exp atual do nivel: {salvar.ExpNivelAtual}");

        }

        if (PlayerPrefs.HasKey("expAtual"))
        {

            salvar.ExpAtual = PlayerPrefs.GetFloat("expAtual");
            Debug.Log($"exp atual: {salvar.ExpAtual}");

        }

        if (PlayerPrefs.HasKey("nivelAtual"))
        {

            salvar.NivelAtual = PlayerPrefs.GetFloat("nivelAtual");
            Debug.Log($"nivel atual: {salvar.NivelAtual}");

        }

    }

    public void CarregarPlayerPrefs(SaveJogo carregar)
    {
        Debug.Log("PlayerPrefs carregados");

        if (carregar != null)
        {

            PlayerPrefs.SetFloat("menorDano", carregar.DanoMenor);
            Debug.Log($"menor dano: {PlayerPrefs.GetInt("menorDano")}");

            PlayerPrefs.SetFloat("maiorDano", carregar.DanoMaior);
            Debug.Log($"maior dano: {PlayerPrefs.GetInt("maiorDano")}");

            PlayerPrefs.SetFloat("vidaAtual", carregar.VidaAtual);
            Debug.Log($"vida atual: {PlayerPrefs.GetInt("vidaAtual")}");
 
            PlayerPrefs.SetFloat("vidaMaxima", carregar.VidaMaxima);
            Debug.Log($"vida maxima: {PlayerPrefs.GetInt("vidaMaxima")}");

            PlayerPrefs.SetString("cenaSalva", carregar.UltimaCenaSalva);
            Debug.Log($"cena: {PlayerPrefs.GetInt("cenaSalva")}");

            PlayerPrefs.SetInt("personagemSelecionado", carregar.PersonagemSelecionado);
            Debug.Log($"personagem: {PlayerPrefs.GetInt("personagemSelecionado")}");

            PlayerPrefs.SetFloat("nivelAtual", carregar.NivelAtual);
            Debug.Log($"nivel atual: {PlayerPrefs.GetFloat("nivelAtual")}");

            PlayerPrefs.SetFloat("expAtual", carregar.ExpAtual);
            Debug.Log($"exp atual: {PlayerPrefs.GetFloat("expAtual")}");

            PlayerPrefs.SetFloat("expNivelAtual", carregar.ExpNivelAtual);
            Debug.Log($"exp do nivel atual: {PlayerPrefs.GetFloat("expNivelAtual")}");

            PlayerPrefs.Save();
        }

    }

    public void ConfirmaSave()
    {
        SaveJogo salvar = new SaveJogo();
        BinaryFormatter formatoBinario = new BinaryFormatter();

        using (FileStream arquivo = File.Open(caminhoArquivo, FileMode.Open))
        {

            SalvarPlayerPrefs(salvar);

            formatoBinario.Serialize(arquivo, salvar);

        }

        if(telaDeConfirmacaoSave != null)
        {

            telaDeConfirmacaoSave.SetActive(false);

        }

        if(botaoIniciar != null)
        {
            
            botaoIniciar.interactable = true;
            FindObjectOfType<ControleSlotsSave>().MostrarTextoSave();

            if(slot1 != null)
            {

                EventSystem.current.SetSelectedGameObject(slot1);
            
            }

        }

    }

    public void NaoConfirmaSave()
    {
        telaDeConfirmacaoSave.SetActive(false);
        EventSystem.current.SetSelectedGameObject(slot1);
    }
}
