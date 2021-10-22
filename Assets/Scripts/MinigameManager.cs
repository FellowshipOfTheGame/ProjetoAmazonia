using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoMinigame
{
    nenhum = 0, quebraCabeca = 1, forca = 2, quiz = 3
}

public class MinigameManager : MonoBehaviour
{
    private EstadoMinigame estadoMinigame = EstadoMinigame.nenhum;
    private int[] posicaoPlayers = new int[4];
    private Quiz quiz;
    //private QuebraCabeca quebraCabeca;
    //private Forca forca;

    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
    }

    private void QuizMinigame()
    {
        estadoMinigame = EstadoMinigame.quiz;
        quiz.gameObject.SetActive(true);
        
    }
}
