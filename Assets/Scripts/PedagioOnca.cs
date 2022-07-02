using System;
using UnityEngine;
using UnityEngine.UI;

public class PedagioOnca : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private Dado _dado;

    private int _jogador;

    void Awake()
    {
        backButton.onClick.AddListener(BackButtonClick);
        _dado = FindObjectOfType<Dado>();
    }
    
    
    private void OnEnable()
    {
        //PEGAR UMA FRASE ALEATORIA
        _jogador = _dado? _dado.jogador : 1;
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void BackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
