using UnityEngine;

public enum EstadoMinigame
{
    Nenhum = 0, QuebraCabeca = 1, Forca = 2, Quiz = 3, Memoria = 4, QualAnimal = 5, SorteReves = 6, ParadaObrigatoria = 7, PedagioOnca = 8, PerdaTurno = 9, QuizVF = 10
}

public class MinigameManager : MonoBehaviour
{
    public EstadoMinigame _estadoMinigame = EstadoMinigame.Nenhum;
    [SerializeField] Quiz _quiz;
    private Memoria _memoria;
    private SorteReves _sorteReves;
    private QuebraCabeca _quebraCabeca;
    private Forca _forca;
    private Adivinhar _adivinharAnimalPlanta;
    private ParadaObrigatoria _paradaObrigatoria;
    private PedagioOnca _pedagioOnca;
    private PerdaTurno _perdaTurno;
    private int[] _posicaoPlayers = new int[4];

    private void Awake()
    {
        // _quiz = FindObjectOfType<Quiz>(true); // Passado no inspector
        _memoria = FindObjectOfType<Memoria>(true);
        _sorteReves = FindObjectOfType<SorteReves>(true);
        _quebraCabeca = FindObjectOfType<QuebraCabeca>(true);
        _forca = FindObjectOfType<Forca>(true);
        _adivinharAnimalPlanta = FindObjectOfType<Adivinhar>(true);
        _paradaObrigatoria = FindObjectOfType<ParadaObrigatoria>(true);
        _pedagioOnca = FindObjectOfType<PedagioOnca>(true);
        _perdaTurno = FindObjectOfType<PerdaTurno>(true);
    }

    public void ComecarQuizMinigame(bool VF)
    {
        _quiz.isVF = VF;
        _estadoMinigame = VF ? EstadoMinigame.Quiz : EstadoMinigame.QuizVF;
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

    public void ComecarPerdaTurno(){
        _estadoMinigame = EstadoMinigame.PerdaTurno;
        _perdaTurno.gameObject.SetActive(true);
    }

}
