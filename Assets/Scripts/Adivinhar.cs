using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Adivinhar : MonoBehaviour
{
    [SerializeField] private AdivinharAnimalPlantaScriptableObject[] adivinharAnimalPlantaScriptableObjects;

    private Image animalPlantaImage;
    private int randomNumber;
    private int jogador = 0;
    private int[] ordemJogada = new int[4] { 0, 1, 2, 3 };
    private Button[] botoes;
    private TMP_Text[] textoBotoes;

    private void Awake()
    {
        animalPlantaImage = GetComponentsInChildren<Image>()[1];
        botoes = GetComponentsInChildren<Button>();
    }

    private void OnEnable()
    {
        randomNumber = Random.Range(0, adivinharAnimalPlantaScriptableObjects.Length);
        animalPlantaImage.sprite = adivinharAnimalPlantaScriptableObjects[randomNumber].spriteAnimalPlanta;
        jogador = 0;
        Debug.LogWarning("Lembrar de pegar o jogador que começa o minigame de outro script");
        FisherYatesShuffle(ordemJogada);

        for (int i = 0; i < botoes.Length; i++)
        {
            textoBotoes[i].text = adivinharAnimalPlantaScriptableObjects[randomNumber].respostas[i];

            if (i == adivinharAnimalPlantaScriptableObjects[randomNumber].respostaCorreta)
            {
                botoes[i].onClick.AddListener(delegate { RespostaCorreta(); });
            }
            else
            {
                botoes[i].onClick.AddListener(delegate { RespostaErrada(); });
            }
        }
    }

    public void RespostaCorreta()
    {
        print($"O jogador {jogador} acertou");
    }

    private void RespostaErrada()
    {
        print("Resposta errada");

        for (int i = 0; i < ordemJogada.Length; i++)
        {
            if (ordemJogada[i] == jogador)
            {
                jogador = ordemJogada[(i + 1) % ordemJogada.Length];
                break;
            }
        }
    }

    private void FisherYatesShuffle(int[] array)
    {
        int tamanho = array.Length;

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);

            int t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}
