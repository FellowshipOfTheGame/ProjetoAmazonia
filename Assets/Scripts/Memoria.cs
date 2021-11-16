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
    private int quantidadeCartasAbertas = 0;
    private int indicePrimeiraCartaAberta = -1;
    private int indiceSegundaCartaAberta = -1;
    private int indiceUltimaCartaAberta = -1;
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
        }
    }

    private void OnDisable()
    {
        for (i = 0; i < quantidadeCartas; i++)
        {
            botoesImage[i].sprite = cartasScriptableObjects[i].verso;

            botoesCartas[i].onClick.RemoveAllListeners();
        }
    }

    private void LateUpdate()
    {
        print(quantidadeCartasAbertas);
        if (quantidadeCartasAbertas == 2)
        {
            DesvirarCartas(indicePrimeiraCartaAberta, indiceSegundaCartaAberta);
            indicePrimeiraCartaAberta = -1;
            indiceSegundaCartaAberta = -1;
            quantidadeCartasAbertas = 0;
        }
    }

    private void DesvirarCartas(int i, int j)
    {
        botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        botoesImage[j].sprite = cartasScriptableObjects[j].verso;
    }

    private void AbrirCarta(int indice)
    {
        botoesImage[indice].sprite = cartasScriptableObjects[indice].frente;
        quantidadeCartasAbertas++;
    }

    private void ClickCarta(int indice)
    {
        if (quantidadeCartasAbertas == 0 && indicePrimeiraCartaAberta != indice) //indicePrimeiraCartaAberta != indice && indiceSegundaCartaAberta == -1)
        {
            AbrirCarta(indice);
            indicePrimeiraCartaAberta = indice;
        }
        else if (quantidadeCartasAbertas == 1 && indiceSegundaCartaAberta != indice) //indiceSegundaCartaAberta != indice && indicePrimeiraCartaAberta != -1)
        {
            AbrirCarta(indice);
            indiceSegundaCartaAberta = indice;
        }
        
        /*
        if (quantidadeCartasAbertas <= 2 && indiceUltimaCartaAberta != indice)
        {
            print("ALOU");
            indiceUltimaCartaAberta = indice;
        }*/
        /*
        if (quantidadeCartasAbertas <= 2)
        {
            botoesImage[indice].sprite = cartasScriptableObjects[indice].frente;
            quantidadeCartasAbertas++;
        }
        else
        {
            botoesCartas[indice].interactable = false;
        }*/


















        /*
        if (quantidadeCartasAbertas <= 2 && indiceUltimaCartaAberta != indice) // Se a carta esta fechada
        {
            botoesImage[indice].sprite = cartasScriptableObjects[indice].frente;
            quantidadeCartasAbertas++;
            indiceUltimaCartaAberta = indice;

            if (quantidadeCartasAbertas == 2)
            {
                if (cartasScriptableObjects[indiceUltimaCartaAberta].indice == cartasScriptableObjects[indice].indice) // Achou o par
                {
                    print(indiceUltimaCartaAberta);
                    botoesCartas[indice].interactable = false;
                    botoesCartas[indiceUltimaCartaAberta].interactable = false;
                    botoesImage[indice].sprite = null;
                    botoesImage[indiceUltimaCartaAberta].sprite = null;
                    quantidadeCartasAbertas = 0;
                    indiceUltimaCartaAberta = -1;
                }
            }
        }*/
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
