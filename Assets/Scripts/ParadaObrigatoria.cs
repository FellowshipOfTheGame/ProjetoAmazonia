using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ParadaObrigatoria : MonoBehaviour
{
    [SerializeField] private Button backButton;
    
    private VideoPlayer _videoPlayer;
    //private int _jogador;

    private void Awake()
    {
        _videoPlayer = GetComponentInChildren<VideoPlayer>();
        backButton.onClick.AddListener(BackButtonClick);
    }
    
    /*
    private void OnEnable()
    {
         PEGAR UMA ANIMACAO ALEATORIA
    }*/

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
