using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text perguntaText;
    [SerializeField] private TMP_Text playerTurnText;
    [SerializeField] private Button[] botoes;
    
    public bool isVF;
    private bool _useShortcut;
    public ListaDePerguntas listaDePerguntas;
    
    private TMP_Text[] _textoBotoes;
    
    private Dado _dado;
    private GameManager _gameManager;
    private MinigameManager _minigameManager;
    private Resultados _resultados;

    private int _player;
    private int _numeroDeCasasAndar;

    [SerializeField] private AudioClip somRespostaCorreta;
    [SerializeField] private AudioClip somRespostaErrada;
    [SerializeField] private float volumeSomResposta = 1.0f;

    private void Awake()
    {
        _textoBotoes = new TMP_Text[botoes.Length];
        
        _resultados = FindObjectOfType<Resultados>(true);
        _minigameManager = FindObjectOfType<MinigameManager>(true);
        _dado = FindObjectOfType<Dado>();
        _gameManager = FindObjectOfType<GameManager>();
        
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });

        for (int i = 0; i < botoes.Length; i++)
        {
            _textoBotoes[i] = botoes[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        _numeroDeCasasAndar = 0;
        _useShortcut = false;

        int perguntaAleatoria = Random.Range(0, isVF ? listaDePerguntas.perguntasVF.Count : listaDePerguntas.perguntasAlternativas.Count);

        _player = _dado ? _dado.jogador : 0;
        
        playerTurnText.text = $"Vez do jogador {(_player + 1).ToString()}";

        perguntaText.text = isVF ? listaDePerguntas.perguntasVF[perguntaAleatoria].pergunta : listaDePerguntas.perguntasAlternativas[perguntaAleatoria].pergunta;


        if (isVF)
        {
            for (int i = 0; i < botoes.Length; i++)
            {
                if (i == 0)
                {
                    _textoBotoes[i].text = "Verdadeiro";

                    if (listaDePerguntas.perguntasVF[perguntaAleatoria].respostaCorreta)
                        botoes[i].onClick.AddListener(RespostaCorreta);
                    else
                        botoes[i].onClick.AddListener(RespostaErrada);
                }
                else if (i == 1)
                {
                    _textoBotoes[i].text = "Falso";            
                    
                    if (listaDePerguntas.perguntasVF[perguntaAleatoria].respostaCorreta)
                        botoes[i].onClick.AddListener(RespostaErrada);
                    else
                        botoes[i].onClick.AddListener(RespostaCorreta);
                }
                else
                {
                    botoes[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < botoes.Length; i++)
            {
                if (i <= listaDePerguntas.perguntasAlternativas[perguntaAleatoria].respostas.Length)
                {
                    _textoBotoes[i].text = listaDePerguntas.perguntasAlternativas[perguntaAleatoria].respostas[i];

                    if (i == listaDePerguntas.perguntasAlternativas[perguntaAleatoria].respostaCorreta)
                    {
                        botoes[i].onClick.AddListener(RespostaCorreta);
                    }
                    else
                    {
                        botoes[i].onClick.AddListener(RespostaErrada);
                    }
                }
                else
                {
                    botoes[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnDisable()
    {
        if (_gameManager)
        {
            _gameManager.BonusMinigame(_player, _numeroDeCasasAndar, _useShortcut);
        }

        foreach (Button button in botoes)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(true);
        }
    }

    
    private void RespostaCorreta()
    {
        // pessoa x anda y casas
        print("Resposta Correta");
        _numeroDeCasasAndar = Random.Range(1, 3);
        _resultados.gameObject.SetActive(true);


        if (_minigameManager._estadoMinigame == EstadoMinigame.PedagioOnca)
        {
            _useShortcut = true;
            _resultados.SetText($"O jogador {(_player + 1).ToString()} acertou e anda " +
                                            $"{_numeroDeCasasAndar.ToString()} casas pelo atalho!");   

        }
        else if (_minigameManager._estadoMinigame == EstadoMinigame.ParadaObrigatoria)
        {
            _resultados.SetText($"Todos acertaram e andam " +
                                            $"{_numeroDeCasasAndar.ToString()} casas");

        }
        else
        {
            _resultados.SetText($"O jogador {(_player + 1).ToString()} acertou e anda " +
                                            $"{_numeroDeCasasAndar.ToString()} casas");   
        }
        AudioManager.Instance.PlaySoundEffect(somRespostaCorreta, volumeSomResposta);
    }

    private void RespostaErrada()
    {
        // pessoa x nao anda
        Debug.Log("Resposta Errada");
        _resultados.gameObject.SetActive(true);
        if (_minigameManager._estadoMinigame == EstadoMinigame.ParadaObrigatoria)
        {
            _resultados.SetText($"Todos erraram");
        }   
        else
        {
            _resultados.SetText($"O jogador {(_player + 1).ToString()} errou");
        }

        AudioManager.Instance.PlaySoundEffect(somRespostaErrada, volumeSomResposta);
    }
}