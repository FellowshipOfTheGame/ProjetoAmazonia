using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ParadaObrigatoria : MonoBehaviour
{
    [SerializeField] private Button backButton;
    
    private VideoPlayer _videoPlayer;
    private Dado _dado;
    private int _jogador;

    private void Awake()
    {
        _videoPlayer = GetComponentInChildren<VideoPlayer>();
        _dado = FindObjectOfType<Dado>();
        backButton.onClick.AddListener(BackButtonClick);
    }
    
    
    private void OnEnable()
    {
        _jogador = _dado.jogador;
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if ((ulong) _videoPlayer.frame == _videoPlayer.frameCount - 1) // o video acabou
        {
            backButton.interactable = true;
        }
    }

    private void BackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
