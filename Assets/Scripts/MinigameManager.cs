using UnityEngine;

public enum EstadoMinigame
{
    Nenhum = 0, QuebraCabeca = 1, Forca = 2, Quiz = 3, Memoria = 4, QualAnimal = 5, SorteReves = 6, ParadaObrigatoria = 7, PedagioOnca = 8
}

public class MinigameManager : MonoBehaviour
{
    private EstadoMinigame _estadoMinigame = EstadoMinigame.Nenhum;
    private Quiz _quiz;
    private Memoria _memoria;
    private SorteReves _sorteReves;
    private QuebraCabeca _quebraCabeca;
    private Forca _forca;
    private Adivinhar _adivinharAnimalPlanta;
    private ParadaObrigatoria _paradaObrigatoria;
    private PedagioOnca _pedagioOnca;
    private int[] _posicaoPlayers = new int[4];

    private void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
        _memoria = FindObjectOfType<Memoria>();
        _sorteReves = FindObjectOfType<SorteReves>();
        _quebraCabeca = FindObjectOfType<QuebraCabeca>();
        _forca = FindObjectOfType<Forca>();
        _adivinharAnimalPlanta = FindObjectOfType<Adivinhar>();
        _paradaObrigatoria = FindObjectOfType<ParadaObrigatoria>();
        _pedagioOnca = FindObjectOfType<PedagioOnca>();
    }

    public void ComecarQuizMinigame()
    {
        _estadoMinigame = EstadoMinigame.Quiz;
        _quiz.gameObject.SetActive(true);
    }

    public void ComecarForcaMinigame()
    {
        _estadoMinigame = EstadoMinigame.Forca;
        _forca.gameObject.SetActive(true);
    }

    public void ComecarMemoriaMinigame()
    {
        _estadoMinigame = EstadoMinigame.Memoria;
        _memoria.gameObject.SetActive(true);
    }

    public void ComecarQuebraCabecaMinigame()
    {
        _estadoMinigame = EstadoMinigame.QuebraCabeca;
        _quebraCabeca.gameObject.SetActive(true);
    }

    public void ComecarQualAnimalMinigame()
    {
        _estadoMinigame = EstadoMinigame.QualAnimal;
        _adivinharAnimalPlanta.gameObject.SetActive(true);
    }

    public void ComecarSorteRevesMinigame()
    {
        _estadoMinigame = EstadoMinigame.SorteReves;
        _sorteReves.gameObject.SetActive(true);
    }
    public void ComecarParadaObrigatoria()
    {
        // verificacao se esta e a primeira vez que esta funcao e chamada deve ser feita no script que chama esta funcao
        _estadoMinigame = EstadoMinigame.ParadaObrigatoria;
        _paradaObrigatoria.gameObject.SetActive(true);
    }

    public void ComecarPedagioOnca()
    {
        // verificacao se esta e a primeira vez que esta funcao e chamada deve ser feita no script que chama esta funcao
        _estadoMinigame = EstadoMinigame.PedagioOnca;
        _pedagioOnca.gameObject.SetActive(true);
    }
}
