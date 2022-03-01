using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Memoria : MonoBehaviour
{
    [SerializeField] private GameObject cartaPrefab;
    [SerializeField] private GameObject gridLayoutGroup;
    
    public CartasScriptableObject[] cartasScriptableObjects;
    
    private GameObject[] _cartasGameObjects;
    private Button[] _botoesCartas;
    private Image[] _botoesImage;
    private int _quantidadeCartas;
    private int _indicePrimeiraCartaAberta = -1;
    private int _jogador;
    private int[] _pontos = new int[4];
    private readonly int[] _ordemJogada = { 0, 1, 2, 3 };

    private void Awake()
    {
        _quantidadeCartas = cartasScriptableObjects.Length;
        _cartasGameObjects = new GameObject[_quantidadeCartas];
        _botoesCartas = new Button[_quantidadeCartas];
        _botoesImage = new Image[_quantidadeCartas];

        for (int i = 0; i < _quantidadeCartas; i++)
        {
            _cartasGameObjects[i] = Instantiate(cartaPrefab, gridLayoutGroup.transform, false);
            _botoesCartas[i] = _cartasGameObjects[i].GetComponent<Button>();
            _botoesImage[i] = _cartasGameObjects[i].GetComponent<Image>();

            int wtf = i;
            _botoesCartas[i].onClick.AddListener(delegate { ClickCarta(wtf); }); // ?????????????
        }
    }

    private void OnEnable()
    {
        for(int i = 0; i < _quantidadeCartas; i++)
        {
            _botoesCartas[i].interactable = true;
            _botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        }
        
        FisherYatesShuffle(_cartasGameObjects);
        _jogador = 0;
        Debug.LogWarning("Lembrar de pegar o jogador que comeca o minigame de outro script");
        Array.Clear(_pontos, 0, _pontos.Length);
        FisherYatesShuffle(_ordemJogada);
    }

    private void OnDestroy()
    {
        foreach (Button button in _botoesCartas) 
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void DesvirarCartas(int i, int j)
    {
        _botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        _botoesImage[j].sprite = cartasScriptableObjects[j].verso;
        _botoesCartas[i].interactable = true;
        _botoesCartas[j].interactable = true;
        _indicePrimeiraCartaAberta = -1;
    }

    private void AbrirCarta(int indice)
    {
        _botoesImage[indice].sprite = cartasScriptableObjects[indice].frente;
        _botoesCartas[indice].interactable = false;
    }

    private void ClickCarta(int indice)
    {
        AbrirCarta(indice);

        if (_indicePrimeiraCartaAberta == -1)
        {
            print($"setando indice primeira carta {indice.ToString()}");
            _indicePrimeiraCartaAberta = indice;
        }
        else if (cartasScriptableObjects[_indicePrimeiraCartaAberta].indice == cartasScriptableObjects[indice].indice)
        {
            _pontos[_jogador] += 10;
            print("deu bom");
            _indicePrimeiraCartaAberta = -1;

            for (int i = 0; i < _ordemJogada.Length; i++)
            {
                if (_ordemJogada[i] != _jogador) continue;
                _jogador = _ordemJogada[(i + 1) % _ordemJogada.Length];
            }
        }
        else
        {
            DesvirarCartas(_indicePrimeiraCartaAberta, indice);
            print("desvirando cartas");
            _jogador = _ordemJogada[(_jogador + 1) % _ordemJogada.Length];
        }
    }

    private void FisherYatesShuffle(GameObject[] array)
    {
        int tamanho = array.Length;

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);

            Transform transformI = array[i].transform;
            
            array[r].transform.SetSiblingIndex(i);
            transformI.SetSiblingIndex(r);
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
