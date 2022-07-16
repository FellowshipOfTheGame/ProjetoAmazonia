using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ParadaObrigatoria : MonoBehaviour
{
    [SerializeField] private Button backButton;
    
    private VideoPlayer _videoPlayer;
    private Dado _dado;
    private int _jogador;
    private GameManager _gameManager;

    [SerializeField] Quiz quiz;
    [SerializeField] private AudioClip somParadaObrigatoria;
    [SerializeField] private float volumeParadaObrigatoria = 1.0f;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _videoPlayer = GetComponentInChildren<VideoPlayer>();
        _dado = FindObjectOfType<Dado>();
        backButton.onClick.AddListener(BackButtonClick);
    }
    
    private void OnEnable()
    {
        _jogador = _dado? _dado.jogador : 1;
        AudioManager.Instance.PlaySoundEffect(somParadaObrigatoria, volumeParadaObrigatoria);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if ((ulong) _videoPlayer.frame == _videoPlayer.frameCount - 1) // o video acabou
        {
            backButton.gameObject.SetActive(true);
        }
    }

    private void BackButtonClick()
    {
        quiz.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
