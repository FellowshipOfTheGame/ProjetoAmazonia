using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text perguntaText;
    [SerializeField] private Button[] botoes;
    
    public PerguntasScriptableObject[] perguntasScriptableObjects;
    
    private TMP_Text[] _textoBotoes;
    
    private Resultados _resultados;

    private int _player;

    [SerializeField] private AudioClip somRespostaCorreta;
    [SerializeField] private AudioClip somRespostaErrada;
    [SerializeField] private float volumeSomResposta = 1.0f;

    private void Awake()
    {
        _textoBotoes = new TMP_Text[botoes.Length];
        
        _resultados = FindObjectOfType<Resultados>(true);
        
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });

        for (int i = 0; i < botoes.Length; i++)
        {
            _textoBotoes[i] = botoes[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        int perguntaAleatoria = Random.Range(0, perguntasScriptableObjects.Length);

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
        foreach (Button button in botoes)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void RespostaCorreta()
    {
        // pessoa x anda y casas
        Debug.Log("Resposta Correta");
        _resultados.gameObject.SetActive(true);
        _resultados.resultadosText.text = $"O jogador {_player} acertou";

        AudioManager.Instance.PlaySoundEffect(somRespostaCorreta, volumeSomResposta);
    }

    private void RespostaErrada()
    {
        // pessoa x nao anda
        Debug.Log("Resposta Errada");
        _resultados.gameObject.SetActive(true);
        _resultados.resultadosText.text = $"O jogador {_player} errou";

        AudioManager.Instance.PlaySoundEffect(somRespostaErrada, volumeSomResposta);
    }
}