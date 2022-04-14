using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject playersQuantityMenu;
    [SerializeField] private GameObject playersSelectionMenu;

    private SceneTransition _sceneTransition;
    
    private int _playersQuantity;

    private void Awake()
    {
        _sceneTransition = SceneTransition.Instance;
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
    
    public void PlayersQuantity(int playersQuantity)
    {
        _playersQuantity = playersQuantity;
        playersSelectionMenu.SetActive(true);
        playersQuantityMenu.SetActive(false);
    }

    public void SelectPlayer()
    {
        SceneTransition.Instance.LoadScene("Tabuleiro");
    }
    
    public void SetPlayersCount(int count)
    {
        //GameManager.Instance.SetPlayersCount(count);
        PlayerPrefsController.SetPlayersCount(count);
        _sceneTransition.LoadScene("Tabuleiro");
    }
}
