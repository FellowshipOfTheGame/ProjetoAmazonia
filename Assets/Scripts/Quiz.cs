using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text perguntaText;
    [SerializeField] private Button[] botoes;
    private TMP_Text[] textoBotoes;

    public PerguntasScriptableObject[] perguntasScriptableObjects;
    
    private void Awake()
    {
        textoBotoes = new TMP_Text[botoes.Length];

        for (int i = 0; i < botoes.Length; i++)
        {
            textoBotoes[i] = botoes[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        int perguntaAleatoria = Random.Range(0, perguntasScriptableObjects.Length);

        perguntaText.text = perguntasScriptableObjects[perguntaAleatoria].pergunta;

        for (int i = 0; i < botoes.Length; i++)
        {
            textoBotoes[i].text = perguntasScriptableObjects[perguntaAleatoria].respostas[i];

            if (i == perguntasScriptableObjects[perguntaAleatoria].respostaCorreta)
            {
                botoes[i].onClick.AddListener(delegate { RespostaCorreta(); });
            }
            else
            {
                botoes[i].onClick.AddListener(delegate { RespostaErrada(); });
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].onClick.RemoveAllListeners();
        }
    }

    private void RespostaCorreta()
    {
        // pessoa x anda y casas
        gameObject.SetActive(false);
    }

    private void RespostaErrada()
    {
        // pessoa x nao anda
        gameObject.SetActive(false);
    }
}