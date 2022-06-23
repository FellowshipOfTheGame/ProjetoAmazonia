using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayersData thePD;
    public CameraMove theCM;
    private GameObject[] partida;
    private static GameObject player1, player2, player3;
    public GameObject personagem1, personagem2, personagem3;
    private GameObject prefab;

    public static int dado = 0;
    public static int casaJogador1 = 0;
    public static int casaJogador2 = 0;
    public static int casaJogador3 = 0;

    // Start is called before the first frame update
    void Awake(){
        
        thePD = PlayersData.Instance;
        theCM = FindObjectOfType<CameraMove>();
        partida = GameObject.FindGameObjectsWithTag("Partida");
        
        DefinirPersonagens();
        
    }
    
    private void DefinirPersonagens(){
        
        int qtdPlayers = thePD.players.Count;

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
                    player1.name = "Player 1";
                    player1.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    player1.GetComponent<Movimento>().andar = false;
                    theCM.p1Cam.Follow = player1.transform;
                    break;
                case 1:
                    player2 = prefab;
                    player2.name = "Player 2";
                    player2.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    player2.GetComponent<Movimento>().andar = false;
                    theCM.p2Cam.Follow = player2.transform;
                    break;
                case 2:
                    player3 = prefab;
                    player3.name = "Player 3";
                    player3.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    player3.GetComponent<Movimento>().andar = false;
                    theCM.p3Cam.Follow = player3.transform;
                    break;

            }

        }

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
