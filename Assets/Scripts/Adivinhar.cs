using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Adivinhar : MonoBehaviour
{
    [SerializeField] private AdivinharAnimalPlantaScriptableObject[] adivinharAnimalPlantaScriptableObjects;
    
    private Button[] _buttons;
    private TMP_Text[] _textButtons;
    
    private Image _animalPlantaImage;
    private Resultados _resultados;
    
    private int[] _ordemJogada;
    
    private int _randomNumber;
    private int _player;

    private void Awake()
    {
        _animalPlantaImage = GetComponentsInChildren<Image>()[1];
        _buttons = GetComponentsInChildren<Button>();
        _resultados = FindObjectOfType<Resultados>(true);
        
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        
        _textButtons = new TMP_Text[_buttons.Length];
        
        int playersCount = PlayersData.Instance.playersCount;
        
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
        _randomNumber = Random.Range(0, adivinharAnimalPlantaScriptableObjects.Length);
        _animalPlantaImage.sprite = adivinharAnimalPlantaScriptableObjects[_randomNumber].spriteAnimalPlanta;
        _player = 0;
        Debug.LogWarning("Lembrar de pegar o jogador que começa o minigame de outro script");
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
                _buttons[i].onClick.AddListener(RespostaErrada);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void RespostaCorreta()
    {
        print($"O jogador {_player} acertou");
        _resultados.gameObject.SetActive(true);
        _resultados.resultadosText.text = $"O jogador {_player} acertou";
    }

    private void RespostaErrada()
    {
        print("Resposta errada");

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
