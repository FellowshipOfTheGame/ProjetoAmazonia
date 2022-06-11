using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{

    public int jogador = 0;
    private int numeroDado = 0;

    public void JogarDado()
    {

        numeroDado = Random.Range(1, 7);
        Debug.Log(numeroDado.ToString());

        GameManager.dado = numeroDado;

        // Desabilitar botão

        switch (jogador)
        {

            case 0:
                GameManager.MoverJogador(jogador + 1);
                break;
            case 1:
                GameManager.MoverJogador(jogador + 1);
                break;
            case 2:
                GameManager.MoverJogador(jogador + 1);
                break;

        }

    }

}
