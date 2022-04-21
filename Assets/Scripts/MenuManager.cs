using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject playersQuantityMenu;
    [SerializeField] private GameObject playersSelectionMenu;
    [SerializeField] private TMP_Text playersQuantityText;

    private SceneTransition _sceneTransition;
    private PlayersData _playersData;

    private int _playersQuantity;
    private int _playerNumber = 1;

    private void Awake()
    {
        _sceneTransition = SceneTransition.Instance;
        _playersData = PlayersData.Instance;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    
    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void CloseCredits()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PlayGame()
    {
        playersQuantityMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SelectPlayer(int playerCharacter)
    {
        if (_playerNumber == _playersQuantity)
        {
            _sceneTransition.LoadScene("Tabuleiro");
        }
        else
        {
            _playersData.players[_playerNumber - 1] = new PlayersData.Player(_playerNumber - 1, playerCharacter);
            _playerNumber++;
            playersQuantityText.text = $"Player {_playerNumber} selecione seu personagem";
        }
    }
    
    public void SetPlayersCount(int playersQuantity)
    {
        //GameManager.Instance.SetPlayersCount(count);
        PlayerPrefsController.SetPlayersCount(playersQuantity);
        _playersQuantity = playersQuantity;
        _playersData.players = new PlayersData.Player[_playersQuantity];
        playersSelectionMenu.SetActive(true);
        playersQuantityMenu.SetActive(false);
    }
}
