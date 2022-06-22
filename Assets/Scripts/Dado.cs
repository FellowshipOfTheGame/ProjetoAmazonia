using UnityEngine;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{

    [SerializeField]
    private Button dice;

    [SerializeField]
    private Button map;

    public int jogador = 0;
    private int numeroDado = 0;

    public void JogarDado()
    {

        numeroDado = Random.Range(1, 7);
        Debug.Log(numeroDado.ToString());

        GameManager.dado = numeroDado;

        // Desabilitar botão
        dice.interactable = false;
        map.interactable = false;

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
