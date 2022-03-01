using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text perguntaText;
    [SerializeField] private Button[] botoes;
    
    public PerguntasScriptableObject[] perguntasScriptableObjects;
    
    private TMP_Text[] _textoBotoes;

    private void Awake()
    {
        _textoBotoes = new TMP_Text[botoes.Length];

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
        print("Resposta Correta");
        //gameObject.SetActive(false);
    }

    private void RespostaErrada()
    {
        // pessoa x nao anda
        print("Resposta Errada");
        //gameObject.SetActive(false);
    }
}