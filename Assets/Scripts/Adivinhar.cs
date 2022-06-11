using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Adivinhar : MonoBehaviour
{
    [SerializeField] private AdivinharAnimalPlantaScriptableObject[] adivinharAnimalPlantaScriptableObjects;
    [SerializeField] private Image animalPlantaImage;
    
    private Button[] _buttons;
    private TMP_Text[] _textButtons;
    
    private RectTransform _animalPlantaRectTransform;
    private Resultados _resultados;
    private Dado _dado;
    private GameManager _gameManager;
    
    private int[] _ordemJogada;
    
    private int _randomNumber;
    private int _player;
    private int _numeroDeCasasAndar;

    private void Awake()
    {
        _animalPlantaRectTransform = animalPlantaImage.GetComponent<RectTransform>();
        _buttons = GetComponentsInChildren<Button>();
        _resultados = FindObjectOfType<Resultados>(true);
        _dado = FindObjectOfType<Dado>(true);
        _gameManager = FindObjectOfType<GameManager>();
        
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        
        _textButtons = new TMP_Text[_buttons.Length];

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

        for (int i = 0; i < _buttons.Length; i++)
        {
            _textButtons[i] = _buttons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        
        _numeroDeCasasAndar = 0;
        _randomNumber = Random.Range(0, adivinharAnimalPlantaScriptableObjects.Length);
        animalPlantaImage.sprite = adivinharAnimalPlantaScriptableObjects[_randomNumber].spriteAnimalPlanta;
        _animalPlantaRectTransform.sizeDelta *= 128;
        _player = _dado.jogador;
        Debug.LogWarning("Lembrar de pegar o jogador que come�a o minigame de outro script");
        FisherYatesShuffle(_ordemJogada);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _textButtons[i].text = adivinharAnimalPlantaScriptableObjects[_randomNumber].respostas[i];

            if (i == adivinharAnimalPlantaScriptableObjects[_randomNumber].respostaCorreta)
            {
                _buttons[i].onClick.AddListener(RespostaCorreta);
            }
            else
            {
                var i1 = i;
                _buttons[i].onClick.AddListener(delegate { RespostaErrada(_buttons[i1]); });
            }
        }
    }

    private void OnDisable()
    {
        _gameManager.BonusMinigame(_player, _numeroDeCasasAndar);
        
        foreach (var button in _buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void RespostaCorreta()
    {
        print($"O jogador {_player.ToString()} acertou");
        _resultados.gameObject.SetActive(true);
        _numeroDeCasasAndar = Random.Range(1, 3);
        _resultados.resultadosText.text = $"O jogador {_player.ToString()} acertou e anda " +
                                          $"{_numeroDeCasasAndar.ToString()} casas";
    }

    private void RespostaErrada(Button button)
    {
        button.interactable = false;
        print("Resposta errada");
        _animalPlantaRectTransform.sizeDelta /= 2;
        
        Debug.LogWarning("LEMBRAR DE MOSTRAR A VEZ DO JOGADOR MUDANDO");
        
        for (int i = 0; i < _ordemJogada.Length; i++)
        {
            if (_ordemJogada[i] != _player) continue;
            _player = _ordemJogada[(i + 1) % _ordemJogada.Length];
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
