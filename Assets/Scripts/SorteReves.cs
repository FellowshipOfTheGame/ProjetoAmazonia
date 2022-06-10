using UnityEngine;
using TMPro;

public class SorteReves : MonoBehaviour
{
    [SerializeField] private TMP_Text textoSorteado;
    
    public SorteRevesScriptableObject[] sorteRevesScriptableObjects;

    private Movimento[] _playersMovimento;
    
    private Dado _dado;
    
    private int _player;
    private int _numeroDeCasasAndar;

    private void Awake()
    {
        _dado = FindObjectOfType<Dado>();
        _playersMovimento = FindObjectsOfType<Movimento>();
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
        _playersMovimento[_player].qtdCasasAndar = _numeroDeCasasAndar;
        //_playersMovimento[_player].BonusMinigame();
        _playersMovimento[_player].bonus = true;
    }

    public void BotaoVoltarClick()
    {
        //players.AndarCasas(sorteRevesScriptableObjects[numSorteado].casasAndadas);
        gameObject.SetActive(false);
    }
}
