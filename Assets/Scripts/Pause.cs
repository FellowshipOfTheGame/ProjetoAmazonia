using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject optionsCanvas;
    
    private SceneTransition _sceneTransition;

    private void Awake()
    {
        _sceneTransition = SceneTransition.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrResumeGame();
        }
    }
    
    // TODO: Colocar um botao no tabuleiro para pausar o jogo
    public void PauseOrResumeGame()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        Time.timeScale = pauseCanvas.activeSelf ? 0f : 1f;
    }

    public void BackButton()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MenuButton()
    {
        _sceneTransition.LoadScene("Menu");
    }

    public void OptionsButton()
    {
        optionsCanvas.SetActive(true);
    }
}
