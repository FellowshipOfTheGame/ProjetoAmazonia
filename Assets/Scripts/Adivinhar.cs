using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Adivinhar : MonoBehaviour
{
    [SerializeField] private AdivinharAnimalPlantaScriptableObject[] adivinharAnimalPlantaScriptableObjects;
    [SerializeField] private Image animalPlantaImage;
    [SerializeField] private TMP_Text playerTurnText;
    
    private Button[] _buttons;
    private TMP_Text[] _textButtons;
    
    private AdivinharAnimalPlantaScriptableObject _adivinharAnimalPlantaSorteadoScriptableObject;
    private RectTransform _animalPlantaRectTransform;
    private Resultados _resultados;
    private Dado _dado;
    private GameManager _gameManager;
    
    private int[] _ordemJogada;
    
    private int _randomNumber;
    private int _player;
    private int _numeroDeCasasAndar;

    [SerializeField] private AudioClip somRespostaCorreta;
    [SerializeField] private AudioClip somRespostaErrada;
    [SerializeField] private float volumeSomResposta = 1.0f;


    private void Awake()
    {
        _animalPlantaRectTransform = animalPlantaImage.GetComponent<RectTransform>();
        _buttons = GetComponentsInChildren<Button>();
        _resultados = FindObjectOfType<Resultados>(true);
        _dado = FindObjectOfType<Dado>(true);
        _gameManager = FindObjectOfType<GameManager>();
        
        _resultados.backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        
        _textButtons = new TMP_Text[_buttons.Length];

        for (int i = 0; i < _buttons.Length; i++)
        {
            _textButtons[i] = _buttons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        _numeroDeCasasAndar = 0;
        _randomNumber = Random.Range(0, adivinharAnimalPlantaScriptableObjects.Length);
        _adivinharAnimalPlantaSorteadoScriptableObject = adivinharAnimalPlantaScriptableObjects[_randomNumber];
        animalPlantaImage.sprite = _adivinharAnimalPlantaSorteadoScriptableObject.spriteAnimalPlanta;
        _animalPlantaRectTransform.sizeDelta *= 128;
        _player = _dado ? _dado.jogador : 0;
        playerTurnText.text = $"Vez do jogador {(_player + 1).ToString()}";

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
        if (_gameManager)
        {
            _gameManager.BonusMinigame(_player, _numeroDeCasasAndar);
        }

        foreach (var button in _buttons)
        {
            button.onClick.RemoveAllListeners();
            button.interactable = true;
        }
    }

    private void RespostaCorreta()
    {
        print($"O jogador {_player.ToString()} acertou");
        _resultados.gameObject.SetActive(true);
        _numeroDeCasasAndar = Random.Range(1, 3);
        _resultados.SetText($"O jogador {(_player + 1).ToString()} acertou e anda " +
                                          $"{_numeroDeCasasAndar.ToString()} {(_numeroDeCasasAndar == 1 ? "casa" : "casas")}", true);
        _resultados.SetImage(_adivinharAnimalPlantaSorteadoScriptableObject.spriteAnimalPlanta);
        AudioManager.Instance.PlaySoundEffect(somRespostaCorreta, volumeSomResposta);
    }

    private void RespostaErrada(Button button)
    {
        button.interactable = false;
        Debug.Log("Resposta errada");
        _animalPlantaRectTransform.sizeDelta /= 2;
        
        for (int i = 0; i < _ordemJogada.Length; i++)
        {
            if (_ordemJogada[i] != _player) continue;
            _player = _ordemJogada[(i + 1) % _ordemJogada.Length];
            break;
        }
        
        playerTurnText.text = $"Vez do jogador {(_player + 1).ToString()}";
        AudioManager.Instance.PlaySoundEffect(somRespostaErrada, volumeSomResposta);
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
