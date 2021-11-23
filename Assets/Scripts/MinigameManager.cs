using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoMinigame
{
    nenhum = 0, quebraCabeca = 1, forca = 2, quiz = 3, memoria = 4, qualAnimal = 5, sorteReves = 6
}

public class MinigameManager : MonoBehaviour
{
    private EstadoMinigame estadoMinigame = EstadoMinigame.nenhum;
    private int[] posicaoPlayers = new int[4];
    private Quiz quiz;
    private Memoria memoria;
    private SorteReves sorteReves;
    //private QuebraCabeca quebraCabeca;
    //private Forca forca;

    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        memoria = FindObjectOfType<Memoria>();
        sorteReves = FindObjectOfType<SorteReves>();
    }

    public void ComecarQuizMinigame()
    {
        estadoMinigame = EstadoMinigame.quiz;
        quiz.gameObject.SetActive(true);
    }

    public void ComecarForcaMinigame()
    {
        estadoMinigame = EstadoMinigame.forca;
        //forca.gameObject.SetActive(true);
    }

    public void ComecarMemoriaMinigame()
    {
        estadoMinigame = EstadoMinigame.memoria;
        memoria.gameObject.SetActive(true);
    }

    public void ComecarQuebraCabecaMinigame()
    {
        estadoMinigame = EstadoMinigame.quebraCabeca;
        //quebraCabeca.gameObject.SetActive(true);
    }

    public void ComecarQualAnimalMinigame()
    {
        estadoMinigame = EstadoMinigame.qualAnimal;
        //qualAnimal.gameObject.SetActive(true);
    }

    public void ComecarSorteRevesMinigame()
    {
        estadoMinigame = EstadoMinigame.sorteReves;
        sorteReves.gameObject.SetActive(true);
    }
}
