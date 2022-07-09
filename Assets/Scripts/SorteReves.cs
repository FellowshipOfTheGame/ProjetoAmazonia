using UnityEngine;
using TMPro;

public class SorteReves : MonoBehaviour
{
    [SerializeField] private TMP_Text textoSorteado;
    
    public SorteRevesScriptableObject[] sorteRevesScriptableObjects;

    [SerializeField] private AudioClip somSorte;
    [SerializeField] private AudioClip somReves;
    [SerializeField] private float volumeSom = 1.0f;

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
        _player = _dado ? _dado.jogador : 0;
        _numeroDeCasasAndar = sorteRevesScriptableObjects[numSorteado].casasAndadas;
        textoSorteado.text = $"Player {(_player + 1).ToString()} teve " +
                             $"{sorteRevesScriptableObjects[numSorteado].texto.ToLower()}" +
                             $" e anda {_numeroDeCasasAndar.ToString()}";
        AudioManager.Instance.PlaySoundEffect(_numeroDeCasasAndar > 0 ? somSorte : somReves, volumeSom);
    }
    
    private void OnDisable()
    {
        if (_gameManager)
        {
            _gameManager.BonusMinigame(_player, _numeroDeCasasAndar);
        }
    }

    public void BotaoVoltarClick()
    {
        //players.AndarCasas(sorteRevesScriptableObjects[numSorteado].casasAndadas);
        gameObject.SetActive(false);
    }
}
