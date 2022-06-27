using System.Collections.Generic;
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

    private void Start()
    {
        _sceneTransition = SceneTransition.instance;
        _playersData = PlayersData.instance;
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
            _playersData.players.Add(new PlayersData.Player(playerCharacter));
            _sceneTransition.LoadScene("Tabuleiro");
        }
        else
        {
            _playersData.players.Add(new PlayersData.Player(playerCharacter));
            _playerNumber++;
            playersQuantityText.text = $"Player {_playerNumber.ToString()} selecione seu personagem";
        }
    }
    
    public void SetPlayersCount(int playersQuantity)
    {
        //GameManager.Instance.SetPlayersCount(count);
        PlayerPrefsController.SetPlayersCount(playersQuantity);
        _playersQuantity = playersQuantity;
        _playersData.players = new List<PlayersData.Player>();
        playersSelectionMenu.SetActive(true);
        playersQuantityMenu.SetActive(false);
    }
}
