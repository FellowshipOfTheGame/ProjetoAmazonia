using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memoria : MonoBehaviour
{
    [SerializeField] private GameObject cartaPrefab;
    [SerializeField] private GameObject gridLayoutGroup;
    public CartasScriptableObject[] cartasScriptableObjects;
    private GameObject[] cartas;
    private Button[] botoesCartas;
    private Image[] botoesImage;
    private int quantidadeCartas = 0;
    private int indicePrimeiraCartaAberta = -1;
    private int jogador = 0;
    private int[] pontos = new int[4];
    private int[] ordemJogada = new int[4] { 0, 1, 2, 3 };

    private void Awake()
    {
        quantidadeCartas = cartasScriptableObjects.Length;
        cartas = new GameObject[quantidadeCartas];
        botoesCartas = new Button[quantidadeCartas];
        botoesImage = new Image[quantidadeCartas];

        for (int i = 0; i < quantidadeCartas; i++)
        {
            cartas[i] = Instantiate(cartaPrefab, gridLayoutGroup.transform, false);
            botoesCartas[i] = cartas[i].GetComponent<Button>();
            botoesImage[i] = cartas[i].GetComponent<Image>();
            botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        }
    }

    private void OnEnable()
    {
        FisherYatesShuffle(cartas);

        for (int i = 0; i < quantidadeCartas; i++)
        {
            int wtf = i;
            botoesCartas[i].onClick.AddListener(delegate { ClickCarta(wtf); }); // ?????????????
            botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        }

        jogador = 0;
        Debug.LogWarning("Lembrar de pegar o jogador que comeca o minigame de outro script");
        FisherYatesShuffle(ordemJogada);
    }

    private void OnDisable()
    {
        for (int i = 0; i < quantidadeCartas; i++)
        {
            botoesCartas[i].onClick.RemoveAllListeners();
        }
    }

    private void DesvirarCartas(int i, int j)
    {
        botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        botoesImage[j].sprite = cartasScriptableObjects[j].verso;
        botoesCartas[i].interactable = true;
        botoesCartas[j].interactable = true;
        indicePrimeiraCartaAberta = -1;
    }

    private void AbrirCarta(int indice)
    {
        botoesImage[indice].sprite = cartasScriptableObjects[indice].frente;
        botoesCartas[indice].interactable = false;
    }

    private void ClickCarta(int indice)
    {
        AbrirCarta(indice);

        if (indicePrimeiraCartaAberta == -1)
        {
            print($"setando indice primeira carta {indice}");
            indicePrimeiraCartaAberta = indice;
        }
        else if (cartasScriptableObjects[indicePrimeiraCartaAberta].indice == cartasScriptableObjects[indice].indice)
        {
            pontos[jogador] += 10;
            print("deu bom");
            indicePrimeiraCartaAberta = -1;

            for (int i = 0; i < ordemJogada.Length; i++)
            {
                if (ordemJogada[i] == jogador)
                {
                    jogador = ordemJogada[(i + 1) % ordemJogada.Length];
                    break;
                }
            }
        }
        else
        {
            DesvirarCartas(indicePrimeiraCartaAberta, indice);
            print("desvirando cartas");
            jogador = ordemJogada[(jogador + 1) % ordemJogada.Length];
        }
    }

    private void FisherYatesShuffle(GameObject[] array)
    {
        int tamanho = array.Length;

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);
            
            array[r].transform.SetSiblingIndex(i);
            array[i].transform.SetSiblingIndex(r);
        }
    }

    private void FisherYatesShuffle(int[] array)
    {
        int tamanho = array.Length;

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);

            (array[r], array[i]) = (array[i], array[r]);
        }
    }
}
