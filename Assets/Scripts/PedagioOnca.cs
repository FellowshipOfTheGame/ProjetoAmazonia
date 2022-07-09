using System;
using UnityEngine;
using UnityEngine.UI;

public class PedagioOnca : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private Dado _dado;

    private int _jogador;

    [SerializeField] private AudioClip somPedagioOnca;
    [SerializeField] private float volumePedagioOnca = 1.0f;

    void Awake()
    {
        backButton.onClick.AddListener(BackButtonClick);
        _dado = FindObjectOfType<Dado>();
    }
    
    
    private void OnEnable()
    {
        //PEGAR UMA FRASE ALEATORIA
        _jogador = _dado? _dado.jogador : 1;
        AudioManager.Instance.PlaySoundEffect(somPedagioOnca, volumePedagioOnca);
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
