using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text perguntaText;
    [SerializeField] private Button[] botoes;
    
    public PerguntasScriptableObject[] perguntasScriptableObjects;
    
    private TMP_Text[] _textoBotoes;
    private Movimento[] _playersMovimento;
    
    private Dado _dado;
    private Resultados _resultados;

    private int _player;
    private int _numeroDeCasasAndar;

    private void Awake()
    {
        _textoBotoes = new TMP_Text[botoes.Length];
        _playersMovimento = FindObjectsOfType<Movimento>();
        
        _resultados = FindObjectOfType<Resultados>(true);
        _dado = FindObjectOfType<Dado>();
        
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
        _player = _dado.jogador;

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
        _playersMovimento[_player].qtdCasasAndar = _numeroDeCasasAndar;
        _playersMovimento[_player].BonusMinigame();
        
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
        _resultados.resultadosText.text = $"O jogador {_player.ToString()} acertou e anda " +
                                          $"{_numeroDeCasasAndar.ToString()} casas";
    }

    private void RespostaErrada()
    {
        // pessoa x nao anda
        print("Resposta Errada");
        _resultados.gameObject.SetActive(true);
        _resultados.resultadosText.text = $"O jogador {_player} errou";
    }
}