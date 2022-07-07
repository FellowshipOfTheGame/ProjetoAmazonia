using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text perguntaText;
    [SerializeField] private TMP_Text playerTurnText;
    [SerializeField] private Button[] botoes;
    
    public PerguntasScriptableObject[] perguntasScriptableObjects;
    
    private TMP_Text[] _textoBotoes;
    
    private Dado _dado;
    private GameManager _gameManager;
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
        int perguntaAleatoria = Random.Range(0, perguntasScriptableObjects.Length);

        _player = _dado ? _dado.jogador : 0;
        
        playerTurnText.text = $"Vez do jogador {(_player + 1).ToString()}";

        perguntaText.text = perguntasScriptableObjects[perguntaAleatoria].pergunta;

        for (int i = 0; i < botoes.Length; i++)
        {
            _textoBotoes[i].text = perguntasScriptableObjects[perguntaAleatoria].respostas[i];

            if (i == perguntasScriptableObjects[perguntaAleatoria].respostaCorreta)
            {
                botoes[i].onClick.AddListener(RespostaCorreta);
            }
            else
            {
                botoes[i].onClick.AddListener(RespostaErrada);
            }
        }
    }

    private void OnDisable()
    {
        if (_gameManager)
        {
            _gameManager.BonusMinigame(_player, _numeroDeCasasAndar);
        }

        foreach (Button button in botoes)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void RespostaCorreta()
    {
        // pessoa x anda y casas
        print("Resposta Correta");
        _numeroDeCasasAndar = Random.Range(1, 3);
        _resultados.gameObject.SetActive(true);
        _resultados.SetText($"O jogador {(_player + 1).ToString()} acertou e anda " +
                                          $"{_numeroDeCasasAndar.ToString()} casas");
        AudioManager.Instance.PlaySoundEffect(somRespostaCorreta, volumeSomResposta);
    }

    private void RespostaErrada()
    {
        // pessoa x nao anda
        Debug.Log("Resposta Errada");
        _resultados.gameObject.SetActive(true);
        _resultados.SetText($"O jogador {(_player + 1).ToString()} errou");
        AudioManager.Instance.PlaySoundEffect(somRespostaErrada, volumeSomResposta);
    }
}