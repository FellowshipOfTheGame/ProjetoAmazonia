using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject playersMenu;
    
    private SceneTransition _sceneTransition;

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
        playersMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void SetPlayersCount(int count)
    {
        //GameManager.Instance.SetPlayersCount(count);
        PlayerPrefsController.SetPlayersCount(count);
        _sceneTransition.LoadScene("Tabuleiro");
    }
}
