using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameObject player1, player2, player3, canvas, theCM;

    public static int dado = 0;
    public static int casaJogador1 = 0;
    public static int casaJogador2 = 0;
    public static int casaJogador3 = 0;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        player3 = GameObject.Find("Player 3");
        canvas = GameObject.Find("Canvas");
        theCM = GameObject.Find("CinemachineManager");

        player1.GetComponent<Movimento>().andar = false;
        player2.GetComponent<Movimento>().andar = false;
        player3.GetComponent<Movimento>().andar = false;

    }

    // Update is called once per frame
    /*void Update()
    {
        
        if(player1.GetComponent<Movimento>().numeroCasa == casaJogador1 + dado)
        {
            TipoDaCasa(player1.GetComponent<Movimento>().casaAtual);

            canvas.GetComponent<Dado>().jogador = (canvas.GetComponent<Dado>().jogador + 1) % 3;
            theCM.GetComponent<CameraMove>().SwitchCamera();
        }

        if (player2.GetComponent<Movimento>().numeroCasa == casaJogador2 + dado)
        {
            TipoDaCasa(player2.GetComponent<Movimento>().casaAtual);

            canvas.GetComponent<Dado>().jogador = (canvas.GetComponent<Dado>().jogador + 1) % 3;
            theCM.GetComponent<CameraMove>().SwitchCamera();
        }

        if (player3.GetComponent<Movimento>().numeroCasa == casaJogador3 + dado)
        {
            TipoDaCasa(player3.GetComponent<Movimento>().casaAtual);

            canvas.GetComponent<Dado>().jogador = (canvas.GetComponent<Dado>().jogador + 1) % 3;
            theCM.GetComponent<CameraMove>().SwitchCamera();
        }

    }*/

    public static void MoverJogador(int jogador)
    {

        switch (jogador)
        {

            case 1:
                player1.GetComponent<Movimento>().qtdCasasAndar = dado;
                player1.GetComponent<Movimento>().andar = true;
                break;
            
            case 2:
                player2.GetComponent<Movimento>().qtdCasasAndar = dado;
                player2.GetComponent<Movimento>().andar = true;
                break;

            case 3:
                player3.GetComponent<Movimento>().qtdCasasAndar = dado;
                player3.GetComponent<Movimento>().andar = true;
                break;

        }
        
    }

}
