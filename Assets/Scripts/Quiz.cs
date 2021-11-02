using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    private Text perguntaText;
    //private Dictionary<string, Respostas> perguntasDicionario = new Dictionary<string, Respostas>();
    private Button[] botoes;
    private Text[] textoBotoes;

    public PerguntasScriptableObject[] perguntasScriptableObjects;
    
    private void Awake()
    {
        perguntaText = GetComponent<Text>();
        //perguntasDicionario = BancoPerguntas.perguntasDicionario;
        botoes = GetComponentsInChildren<Button>();

        for (int i = 0; i < botoes.Length; i++)
        {
            textoBotoes[i] = botoes[i].GetComponentInChildren<Text>();
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

        /* Se for usar dicionario
        int numeroAleatorio = Random.Range(0, perguntasDicionario.Count);
        int contador = 0;

        foreach (KeyValuePair<string, Respostas> pergunta in perguntasDicionario)
        {
            if (contador == numeroAleatorio)
            {
                perguntaText.text = pergunta.Key;

                for (int i = 0; i < botoes.Length; i++)
                {
                    textoBotoes[i].text = pergunta.Value.respostas[i];

                    if (i == pergunta.Value.respostaCorreta)
                    {
                        botoes[i].onClick.AddListener( delegate { RespostaCorreta(); });
                    }
                    else
                    {
                        botoes[i].onClick.AddListener( delegate { RespostaErrada(); });
                    }
                }

                break;
            }

            contador++;
        }*/
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