using UnityEngine;
using TMPro;

public class SorteReves : MonoBehaviour
{
    [SerializeField] private TMP_Text textoSorteado;
    
    public SorteRevesScriptableObject[] sorteRevesScriptableObjects;
    
    private Dado _dado;
    
    private int _player;

    
    private void Awake()
    {
        _dado = FindObjectOfType<Dado>();
    }
    

    private void OnEnable()
    {
        int numSorteado = Random.Range(0, sorteRevesScriptableObjects.Length);
        _player = _dado.jogador;
        textoSorteado.text = $"Player {_player + 1} teve {sorteRevesScriptableObjects[numSorteado].texto.ToLower()}";
    }

    public void BotaoVoltarClick()
    {
        //players.AndarCasas(sorteRevesScriptableObjects[numSorteado].casasAndadas);
        gameObject.SetActive(false);
    }
}
