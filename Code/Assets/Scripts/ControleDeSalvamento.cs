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
    //variáveis do botão e da tela de confirmação
    [SerializeField] private Button botaoIniciar;
    [SerializeField] private GameObject telaDeConfirmacaoSave;

    //variáveis locais
    private GameObject atributosJogador;
    private string caminhoArquivo;
    private bool arquivoExiste;

    //variáveis para o controle caso esteja conectado
    [Header("ControleBotoes")]
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject botaoConfirmar;

    //como chamar em outro lugar o save, lembrando de chamar a função e passar o parâmetro salvar dentro depois de especificar as alterações
    //SaveJogo salvar = new SaveJogo();
    
    private void Awake()
    {
        //verifica se o valor verdadeiro é 1 para poder carregar o salvamento do jogo em alguma parte após ele já ter sido criado
        if(PlayerPrefs.GetInt("valorVerdadeiro") == 1)
        {
            CarregarComecoJogo(PlayerPrefs.GetInt("ultimoSlotSelecionado"));
        }
    }

    public void SalvarJogo(int slotSelecionado) 
    {
        //define o valor verdadeiro como 0 e salva
        PlayerPrefs.SetInt("valorVerdadeiro", 0);
        PlayerPrefs.Save();

        SaveJogo salvar = new SaveJogo();

        //variável para formatar em binário
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local padrão onde é salvo o save
        string localSave = Application.persistentDataPath;

        //novo caminho do arquivo do save
        caminhoArquivo = Path.Combine(localSave + $"/EtecParanormal{slotSelecionado}.save");

        //verifica se o save já existe
        if(File.Exists(caminhoArquivo))
        {
            //se existir, verifica se a tela de confirmação não é nulaa, se não for, abre e verifica se o jogador quer mesmo sobrepor o save
            if(telaDeConfirmacaoSave != null)
            {
                telaDeConfirmacaoSave.SetActive(true);
                EventSystem.current.SetSelectedGameObject(botaoConfirmar);
            }
            else
            {
                //confirma o save automaticamente caso a tela de confirmação seja nula
                ConfirmaSave();
            }
        }
        else
        {
            //acessa o caminho do arquivo e salva os valores do jogador antes de criptografar as informações
            using(FileStream arquivo = File.Create(caminhoArquivo))
            {
                SalvarPlayerPrefs(salvar);
                formatoBinario.Serialize(arquivo, salvar);
            }

            //verifica se o botão de iniciar do menu não é nulo para deixá-lo interativo para poder  iniciar o jogo
            if(botaoIniciar != null)
            {
                botaoIniciar.interactable = true;
            }

            //procura o objeto de controle de save e chama a função de atualizar o texto do save
            FindObjectOfType<ControleSlotsSave>().MostrarTextoSave();
        }

        //verifica se o botão de iniciar não é nulo para deixá-lo interativo
        if (botaoIniciar != null)
        {
            botaoIniciar.interactable = true;
        }
        
        //recebe o slot do save selecionado e instancia no save da nuvem
        PlayerPrefs.SetInt("ultimoSlotSelecionado", slotSelecionado);
    }

    public SaveJogo Carregar(int slotSelecionado)
    {
        //define que o arquivo não existe para fazer as verificações
        arquivoExiste = false;

        //define o último slot selecionado para acessar as informações dele
        PlayerPrefs.SetInt("ultimoSlotSelecionado", slotSelecionado);

        //variável para desformatar
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //local padrão onde foi salvo
        string localSave = Application.persistentDataPath;

        FileStream arquivo;
        
        //verifica se o arquivo existe para abrí-lo
        if(File.Exists(localSave + "/EtecParanormal" + slotSelecionado + ".save"))
        {
            //abre o arquivo no local especificado, descriptografa ele e o fecha
            arquivo = File.Open(localSave + $"/EtecParanormal{slotSelecionado}.save", FileMode.Open);
            SaveJogo CarregarJogo = (SaveJogo)formatoBinario.Deserialize(arquivo);
            arquivo.Close();

            //define que o arquivo existe e carrega as informações do save
            arquivoExiste = true;
            return CarregarJogo;
        }

        return null;
    }

    public void CarregarJogo(int slot)
    {
        //define o valor verdadeiro como 1 e salva
        PlayerPrefs.SetInt("valorVerdadeiro", 1);
        PlayerPrefs.Save();

        //define o carregar com o carregar do save passando o slot que foi selecionado
        SaveJogo carregar = Carregar(slot);

        //carrega os saves do jogo passando o arquivo onde foi salvado as informações
        CarregarPlayerPrefs(carregar);

        //verifica se o arquivo existe
        if (arquivoExiste)
        {
            //define a última cena que foi salva e carrega ela
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
        //carrega no começo do jogo as informações da sala e do jogador
        SaveJogo carregar = Carregar(slot);
        CarregarPlayerPrefs(carregar);
    }

    private void SalvarPlayerPrefs(SaveJogo salvar)
    {
        //chama todas as funções do jogador de salvar
        atributosJogador = GameObject.FindWithTag("Player");
        atributosJogador.GetComponent<SistemaArma>().objTiro.GetComponent<DanoDaBolinha>().SalvarDano();
        atributosJogador.GetComponent<VidaJogador>().SalvarVida();
        atributosJogador.GetComponent<BarraDeXP>().SalvarXP();

        //verifica se há chave, se houver salva o novo valor em todas
        if (PlayerPrefs.HasKey("personagemSelecionado"))
        {
            salvar.PersonagemSelecionado = PlayerPrefs.GetInt("personagemSelecionado");
        }

        if (PlayerPrefs.HasKey("cenaSalva"))
        {
            salvar.UltimaCenaSalva = PlayerPrefs.GetString("cenaSalva");
        }
        else
        {
            //se não houver fases salvas, define como fase 1 a cena
            PlayerPrefs.SetString("cenaSalva", "Fase1");
            salvar.UltimaCenaSalva = PlayerPrefs.GetString("cenaSalva");
        }

        if (PlayerPrefs.HasKey("vidaAtual"))
        {
            salvar.VidaAtual = PlayerPrefs.GetFloat("vidaAtual");
        }

        if (PlayerPrefs.HasKey("vidaMaxima"))
        {
            salvar.VidaMaxima = PlayerPrefs.GetFloat("vidaMaxima");
        }

        if (PlayerPrefs.HasKey("menorDano"))
        {
            salvar.DanoMenor = PlayerPrefs.GetFloat("menorDano");
        }

        if (PlayerPrefs.HasKey("maiorDano"))
        {
            salvar.DanoMaior = PlayerPrefs.GetFloat("maiorDano");
        }

        if (PlayerPrefs.HasKey("expNivelAtual"))
        {
            salvar.ExpNivelAtual = PlayerPrefs.GetFloat("expNivelAtual");
        }

        if (PlayerPrefs.HasKey("expAtual"))
        {
            salvar.ExpAtual = PlayerPrefs.GetFloat("expAtual");
        }

        if (PlayerPrefs.HasKey("nivelAtual"))
        {
            salvar.NivelAtual = PlayerPrefs.GetFloat("nivelAtual");
        }
    }

    public void CarregarPlayerPrefs(SaveJogo carregar)
    {
        //verifica se o carregar passado não é nulo e instancia todos os valores para as variáveis da nuvem
        if (carregar != null)
        {
            PlayerPrefs.SetFloat("menorDano", carregar.DanoMenor);

            PlayerPrefs.SetFloat("maiorDano", carregar.DanoMaior);

            PlayerPrefs.SetFloat("vidaAtual", carregar.VidaAtual);
 
            PlayerPrefs.SetFloat("vidaMaxima", carregar.VidaMaxima);

            PlayerPrefs.SetString("cenaSalva", carregar.UltimaCenaSalva);

            PlayerPrefs.SetInt("personagemSelecionado", carregar.PersonagemSelecionado);

            PlayerPrefs.SetFloat("nivelAtual", carregar.NivelAtual);

            PlayerPrefs.SetFloat("expAtual", carregar.ExpAtual);

            PlayerPrefs.SetFloat("expNivelAtual", carregar.ExpNivelAtual);

            PlayerPrefs.Save();
        }
    }

    public void ConfirmaSave()
    {
        //define as variáveis iniciais de save e do formato do arquivo
        SaveJogo salvar = new SaveJogo();
        BinaryFormatter formatoBinario = new BinaryFormatter();

        //usa o caminho do arquivo e o abre
        using (FileStream arquivo = File.Open(caminhoArquivo, FileMode.Open))
        {
            //salva os atributos do jogador e criptografa eles
            SalvarPlayerPrefs(salvar);
            formatoBinario.Serialize(arquivo, salvar);
        }

        //verifica se a tela de confirmação não é nula, e se for e estiver ativa, desativa ela
        if(telaDeConfirmacaoSave != null)
        {
            telaDeConfirmacaoSave.SetActive(false);
        }

        //verifica se o botão iniciar não é nulo para ativar ele e atualiza o texto do slot do save
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

    //caso não confirme o save, apenas desativa a tela de confirmação e volta pro slot 1
    public void NaoConfirmaSave()
    {
        telaDeConfirmacaoSave.SetActive(false);
        EventSystem.current.SetSelectedGameObject(slot1);
    }
}
