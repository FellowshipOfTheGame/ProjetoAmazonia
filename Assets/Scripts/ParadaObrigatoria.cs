using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ParadaObrigatoria : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private Button backButton;
    private int jogador;

    private void Start()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        backButton.onClick.AddListener(delegate { BackButtonClick(); });
    }

    private void OnEnable()
    {
        // PEGAR UMA ANIMACAO ALEATORIA
    }

    private void Update()
    {
        if ((ulong) videoPlayer.frame == videoPlayer.frameCount - 1) // o video acabou
        {
            backButton.interactable = true;
        }
    }

    private void BackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
