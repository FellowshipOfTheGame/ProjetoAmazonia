using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayersData thePD;
    private GameObject[] partida;
    private static GameObject player1, player2, player3, canvas, theCM;
    public GameObject personagem1, personagem2, personagem3;
    private GameObject prefab;

    public static int dado = 0;
    public static int casaJogador1 = 0;
    public static int casaJogador2 = 0;
    public static int casaJogador3 = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        thePD = PlayersData.Instance;
        partida = GameObject.FindGameObjectsWithTag("Partida");
        
        DefinirPersonagens();

        Debug.Log(player1);
        Debug.Log(player2);
        Debug.Log(player3);

        //player1 = GameObject.Find("Player 1");
        //player2 = GameObject.Find("Player 2");
        //player3 = GameObject.Find("Player 3");
        canvas = GameObject.Find("Canvas");
        theCM = GameObject.Find("CinemachineManager");

        //player1.GetComponent<Movimento>().andar = false;
        //player2.GetComponent<Movimento>().andar = false;
        //player3.GetComponent<Movimento>().andar = false;

    }

    private void DefinirPersonagens(){
        
        int qtdPlayers = thePD.players.Length;

        for(int i = 0; i < qtdPlayers; i++){

            Debug.Log(thePD.players[i].character);

            switch(thePD.players[i].character){

                case 0:
                    prefab = Instantiate(personagem1, partida[i].transform.position, Quaternion.identity);
                    break;
                case 1:
                    prefab = Instantiate(personagem2, partida[i].transform.position, Quaternion.identity);
                    break;
                case 2:
                    prefab = Instantiate(personagem3, partida[i].transform.position, Quaternion.identity);
                    break;

            }

            switch(i){

                case 0:
                    player1 = prefab;
                    player1.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    break;
                case 1:
                    player2 = prefab;
                    player2.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    break;
                case 2:
                    player3 = prefab;
                    player3.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    break;

            }

        }

        player1.GetComponent<Movimento>().andar = false;
        player2.GetComponent<Movimento>().andar = false;
        player3.GetComponent<Movimento>().andar = false;

    }

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
