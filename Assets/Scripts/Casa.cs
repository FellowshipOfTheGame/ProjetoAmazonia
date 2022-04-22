using UnityEngine;

public class Casa : MonoBehaviour
{

    [Header("Caminho padr�o")]
    public Casa anterior;
    public Casa proxima;

    [Space(5)]

    [Header("Atalho")]
    public bool bifurcacao;
    public Casa atalho;

    [Space(5)]

    [Header("Fun��o da casa")]
    public EstadoMinigame tipoDaCasa;

}
