using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;

public class Memoria : MonoBehaviour
{
    [SerializeField] private GameObject cartaPrefab;
    [SerializeField] private GameObject gridLayoutGroup;
    
    public CartasScriptableObject[] cartasScriptableObjects;
    
    private GameObject[] _cartasGameObjects;
    private Button[] _botoesCartas;
    private Image[] _botoesImage;
    private Movimento[] _playersMovimento;
    
    private Resultados _resultados;
    private Dado _dado;
    
    private int[] _ordemJogada;
    
    private int _quantidadeCartas;
    private int _indicePrimeiraCartaAberta = -1;
    private int _jogador;
    private int _numeroDeCasasAndar;

    private void Awake()
    {
        _quantidadeCartas = cartasScriptableObjects.Length;
        _dado = FindObjectOfType<Dado>();
        _cartasGameObjects = new GameObject[_quantidadeCartas];
        _botoesCartas = new Button[_quantidadeCartas];
        _botoesImage = new Image[_quantidadeCartas];
        
        _resultados = FindObjectOfType<Resultados>(true);
        
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        
        int playersCount;
        
        try
        {
            playersCount = PlayersData.Instance.players.Count;
        }
        catch (System.NullReferenceException)
        {
            playersCount = 1;
        }
        
        _ordemJogada = new int[playersCount];

        for (int i = 0; i < playersCount; i++)
        {
            _ordemJogada[i] = i;
        }

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
        
        _numeroDeCasasAndar = 0;
        
        for(int i = 0; i < _quantidadeCartas; i++)
        {
            _botoesCartas[i].interactable = true;
            _botoesImage[i].sprite = cartasScriptableObjects[i].verso;
        }
        
        FisherYatesShuffle(_cartasGameObjects);
        _jogador = _dado.jogador;
        Debug.LogWarning("Lembrar de pegar o jogador que comeca o minigame de outro script");
        FisherYatesShuffle(_ordemJogada);
    }

    private void OnDestroy()
    {
        foreach (Button button in _botoesCartas) 
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void OnDisable()
    {
        _playersMovimento[_jogador].qtdCasasAndar = _numeroDeCasasAndar;
        //_playersMovimento[_jogador].BonusMinigame();
        _playersMovimento[_jogador].bonus = true;
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
            print("deu bom");
            _indicePrimeiraCartaAberta = -1;
            
            //se acabaram as cartas, mostrar resultados
            if (_botoesCartas.All(botaoCarta => !botaoCarta.interactable)) //copilot lindo
            {
                _numeroDeCasasAndar = Random.Range(1, 3);
                _resultados.gameObject.SetActive(true);
                _resultados.resultadosText.text = $"Jogador {_jogador + 1} avançou {_numeroDeCasasAndar} casas";
            }

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
