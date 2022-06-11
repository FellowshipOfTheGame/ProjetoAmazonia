using UnityEngine;
using TMPro;

public class SorteReves : MonoBehaviour
{
    [SerializeField] private TMP_Text textoSorteado;
    
    public SorteRevesScriptableObject[] sorteRevesScriptableObjects;

    private Dado _dado;
    private GameManager _gameManager;
    
    private int _player;
    private int _numeroDeCasasAndar;

    private void Awake()
    {
        _dado = FindObjectOfType<Dado>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        int numSorteado = Random.Range(0, sorteRevesScriptableObjects.Length);
        _player = _dado.jogador;
        _numeroDeCasasAndar = sorteRevesScriptableObjects[numSorteado].casasAndadas;
        textoSorteado.text = $"Player {_player + 1} teve {sorteRevesScriptableObjects[numSorteado].texto.ToLower()}" +
                             $" e anda {_numeroDeCasasAndar.ToString()}";
    }
    
    private void OnDisable()
    {
        _gameManager.BonusMinigame(_player, _numeroDeCasasAndar);
    }

    public void BotaoVoltarClick()
    {
        //players.AndarCasas(sorteRevesScriptableObjects[numSorteado].casasAndadas);
        gameObject.SetActive(false);
    }
}
