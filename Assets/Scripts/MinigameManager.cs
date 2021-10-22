using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoMinigame
{
    nenhum = 0, quebraCabeca = 1, forca = 2
}

public class MinigameManager : MonoBehaviour
{
    private EstadoMinigame estadoMinigame = 0;
    private int[] posicaoPlayers = new int[4];
    //private QuebraCabeca quebraCabeca;
    //private Forca forca;
}
