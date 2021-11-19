using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memoria : MonoBehaviour
{
    [SerializeField] private GameObject cartaPrefab;
    [SerializeField] private GameObject gridLayoutGroup;
    private GameObject[] cartas;
    private Button[] botoesCartas;
    private Image[] botoesImage;
    private int i;
    private int quantidadeCartas = 0;
    private int indicePrimeiraCartaAberta = -1;
    public CartasScriptableObject[] cartasScriptableObjects;

    private void Awake()
    {
        quantidadeCartas = cartasScriptableObjects.Length;
        cartas = new GameObject[quantidadeCartas];
        botoesCartas = new Button[quantidadeCartas];
        botoesImage = new Image[quantidadeCartas];

        for (i = 0; i < quantidadeCartas; i++)
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

        for (i = 0; i < quantidadeCartas; i++)
        {
            int wtf = i;
            botoesCartas[i].onClick.AddListener(delegate { ClickCarta(wtf); }); // ?????????????
            botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        }
    }

    private void OnDisable()
    {
        for (i = 0; i < quantidadeCartas; i++)
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
            print("deu bom");
            indicePrimeiraCartaAberta = -1;
        }
        else
        {
            DesvirarCartas(indicePrimeiraCartaAberta, indice);
            print("desvirando cartas");
        }
    }

    private void FisherYatesShuffle(GameObject[] array)
    {
        System.Random random = new System.Random();
        int tamanho = array.Length;

        for (i = 0; i < tamanho - 1; i++)
        {
            int r = i + random.Next(tamanho - i);

            GameObject t = array[r];
            array[r] = array[i];
            array[r].transform.SetSiblingIndex(i);
            array[i].transform.SetSiblingIndex(r);
            array[i] = t;
        }
    }
}
