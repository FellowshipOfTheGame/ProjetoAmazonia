using UnityEngine;

public class Casa : MonoBehaviour
{

    [Header("Caminho padrão")]
    public Casa anterior;
    public Casa proxima;

    [Space(5)]

    [Header("Atalho")]
    public bool bifurcacao;
    public Casa atalho;

    [Space(5)]

    [Header("Função da casa")]
    public EstadoMinigame tipoDaCasa;

}
