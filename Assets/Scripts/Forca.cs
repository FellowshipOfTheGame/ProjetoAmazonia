using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text;
using System.Globalization;
using Random = UnityEngine.Random;

public class Forca : MonoBehaviour
{
    [SerializeField] private GameObject letraPrefab;
    [SerializeField] private TMP_Text palavraAleatoriaText;
    [SerializeField] private TMP_Text playerTurnText;
    [SerializeField] private ForcaScriptableObject[] forcaScriptableObjects;

    private Button[] _letrasButton;
        
    private GridLayoutGroup _gridLayoutGroup;
    private Resultados _resultados;
    private Dado _dado;
    private GameManager _gameManager;
    
    private int[] _ordemJogada;
    private char[] _charArray;
    private char[] _palavra;

    private const int AlfabetoTamanho = 26;

    private string _palavraComAcento;
    private int _numeroDeCasasAndar;
    private int _errosMax = 6;
    private int _jogador;
    private int _erros;
    private bool _acertou;

    private void Awake()
    {
        _gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
        _dado = FindObjectOfType<Dado>();
        _gameManager = FindObjectOfType<GameManager>();
        _letrasButton = new Button[AlfabetoTamanho];
        _resultados = FindObjectOfType<Resultados>(true);
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });

        for (int i = 'A'; i < AlfabetoTamanho + 'A'; i++)
        {
            int indice = i - 'A';
            GameObject letraGameObject =  Instantiate(letraPrefab, _gridLayoutGroup.transform, false);
            _letrasButton[indice] = letraGameObject.GetComponent<Button>();
            letraGameObject.GetComponentInChildren<TMP_Text>().text = char.ConvertFromUtf32(i);
            _letrasButton[indice].onClick.AddListener(delegate { LetraClick(indice); });
        }
    }

    private void OnEnable()
    {
        
        _numeroDeCasasAndar = 0;
        int randomNumber = Random.Range(0, forcaScriptableObjects.Length);
        _palavra = new char[forcaScriptableObjects[randomNumber].animal.Length];
        _palavraComAcento = forcaScriptableObjects[randomNumber].animal;
        _charArray = RemoveAccents(_palavraComAcento).ToCharArray();

        print(new string(_charArray));

        for (int i = 0; i < _palavra.Length; i++)
        {
            _palavra[i] = '-';
        }

        foreach (Button button in _letrasButton)
        {
            button.interactable = true;
        }

        _jogador = _dado ? _dado.jogador : 0;
        
        playerTurnText.text = $"Vez do jogador {(_jogador + 1).ToString()}";
        _erros = 0;
        
        if (_ordemJogada == null)
        {
            int playersCount;
            
            try
            {
                playersCount = PlayersData.instance.players.Count;
            }
            catch (NullReferenceException)
            {
                playersCount = 1;
            }
            
            _ordemJogada = new int[playersCount];
        
            for (int i = 0; i < playersCount; i++)
            {
                _ordemJogada[i] = i;
            }
        }
        
        FisherYatesShuffle(_ordemJogada);
        palavraAleatoriaText.text = new string(_palavra);
    }

    private void OnDisable()
    {
        if (_gameManager)
        {
            _gameManager.BonusMinigame(_jogador, _numeroDeCasasAndar);
        }
    }

    private void OnDestroy()
    {
        foreach (Button button in _letrasButton)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void LetraClick(int indice)
    {
        _acertou = false;

        for (int i = 0; i < _charArray.Length; i++)
        {
            if (_charArray[i] != indice + 'A') continue;
            int numero = indice + 'A';
            _palavra[i] = (char) numero;
            _acertou = true;
        }
        if (_acertou)
        {
            string palavra = new string(_palavra);
            print("Acertou");

            palavraAleatoriaText.text = Substituir(palavra, _palavraComAcento);
            
            if (!_palavra.Contains('-'))
            {
                print($"O jogador {_jogador.ToString()} acertou");
                _resultados.gameObject.SetActive(true);
                _numeroDeCasasAndar = Random.Range(1, 3);
                _resultados.resultadosText.text = $"O jogador {(_jogador + 1).ToString()} acertou e anda " +
                                                  $"{_numeroDeCasasAndar.ToString()} " +
                                                  $"{(_numeroDeCasasAndar == 1 ? "casa" : "casas")}";
            }
        }
        else
        {
            print("Errou");
            _erros++;
            // trocar a imagem da forca

            if (_erros >= _errosMax)
            {
                print("Todos perdem");
                _resultados.gameObject.SetActive(true);
                _resultados.resultadosText.text = "Todos perdem";

                return;
            }

            for (int i = 0; i < _ordemJogada.Length; i++)
            {
                if (_ordemJogada[i] != _jogador) continue;
                _jogador = _ordemJogada[(i + 1) % _ordemJogada.Length];
                break;
            }
        
            playerTurnText.text = $"Vez do jogador {(_jogador + 1).ToString()}";
        }

        _letrasButton[indice].interactable = false;
    }

    private static void FisherYatesShuffle(int[] array)
    {
        int tamanho = array.Length;

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);

            (array[r], array[i]) = (array[i], array[r]);
        }
    }
    
    private string RemoverAcentos(string texto)
    {
        const string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
        const string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

        for (int i = 0; i < comAcentos.Length; i++)
        {
            texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
        }
        return texto;
    }
    
    private string RemoveAccents(string str)
    {  
        return new string(str  
            .Normalize(NormalizationForm.FormD)  
            .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)  
            .ToArray());
    }

    private string Substituir(string old, string nova)
    {
        char[] oldChars = old.ToCharArray();
        char[] novaChars = nova.ToCharArray();
        
        for (int i = 0; i < oldChars.Length; i++)
        {
            if (oldChars[i] != '-')
            {
                oldChars[i] = novaChars[i];
            }
        }

        return new string(oldChars);
    }
}
