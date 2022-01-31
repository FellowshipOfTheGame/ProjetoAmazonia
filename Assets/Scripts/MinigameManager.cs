using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoMinigame
{
    nenhum = 0, quebraCabeca = 1, forca = 2, quiz = 3, memoria = 4, qualAnimal = 5, sorteReves = 6, paradaObrigatoria = 7, pedagioOnca = 8
}

public class MinigameManager : MonoBehaviour
{
    private EstadoMinigame estadoMinigame = EstadoMinigame.nenhum;
    private int[] posicaoPlayers = new int[4];
    private Quiz quiz;
    private Memoria memoria;
    private SorteReves sorteReves;
    private QuebraCabeca quebraCabeca;
    private Forca forca;
    private Adivinhar adivinharAnimalPlanta;
    private ParadaObrigatoria paradaObrigatoria;
    private PedagioOnca pedagioOnca;

    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        memoria = FindObjectOfType<Memoria>();
        sorteReves = FindObjectOfType<SorteReves>();
        quebraCabeca = FindObjectOfType<QuebraCabeca>();
        forca = FindObjectOfType<Forca>();
        adivinharAnimalPlanta = FindObjectOfType<Adivinhar>();
        paradaObrigatoria = FindObjectOfType<ParadaObrigatoria>();
        pedagioOnca = FindObjectOfType<PedagioOnca>();
    }

    public void ComecarQuizMinigame()
    {
        estadoMinigame = EstadoMinigame.quiz;
        quiz.gameObject.SetActive(true);
    }

    public void ComecarForcaMinigame()
    {
        estadoMinigame = EstadoMinigame.forca;
        forca.gameObject.SetActive(true);
    }

    public void ComecarMemoriaMinigame()
    {
        estadoMinigame = EstadoMinigame.memoria;
        memoria.gameObject.SetActive(true);
    }

    public void ComecarQuebraCabecaMinigame()
    {
        estadoMinigame = EstadoMinigame.quebraCabeca;
        quebraCabeca.gameObject.SetActive(true);
    }

    public void ComecarQualAnimalMinigame()
    {
        estadoMinigame = EstadoMinigame.qualAnimal;
        adivinharAnimalPlanta.gameObject.SetActive(true);
    }

    public void ComecarSorteRevesMinigame()
    {
        estadoMinigame = EstadoMinigame.sorteReves;
        sorteReves.gameObject.SetActive(true);
    }
    public void ComecarParadaObrigatoria()
    {
        // verificacao se esta é a primeira vez que esta funcao é chamada deve ser feita no script que chama esta funcao
        estadoMinigame = EstadoMinigame.paradaObrigatoria;
        paradaObrigatoria.gameObject.SetActive(true);
    }

    public void ComecarPedagioOnca()
    {
        // verificacao se esta é a primeira vez que esta funcao é chamada deve ser feita no script que chama esta funcao
        estadoMinigame = EstadoMinigame.pedagioOnca;
        pedagioOnca.gameObject.SetActive(true);
    }
}
