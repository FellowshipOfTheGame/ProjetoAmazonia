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
    private GameObject prefab, canvas;

    public static int dado = 0, player;
    public static int casaJogador1 = 0;
    public static int casaJogador2 = 0;
    public static int casaJogador3 = 0;

    // Start is called before the first frame update
    void Awake(){
        
        thePD = PlayersData.Instance;
        theCM = FindObjectOfType<CameraMove>();
        canvas = GameObject.Find("Canvas");
        partida = GameObject.FindGameObjectsWithTag("Partida");
        
        DefinirPersonagens();
        
    }

    void Start() {

        player = canvas.GetComponent<Dado>().jogador;
        
    }

    void Update(){
        
        // Checa se chegou no fim do turno do jogador = muda a câmera
        // Fim do turno = andar + minigame

        switch(player){

            case 0:

                if(player1.GetComponent<Movimento>().andar == false && player1.GetComponent<Movimento>().acabouMinigame == true){

                    player1.GetComponent<Movimento>().acabouMinigame = false;
                    // Talvez fazer uma pausa para exibir mensagem antes de mudar o player
                    ChangePlayer();

                }

                break;

            case 1:

                if(player2.GetComponent<Movimento>().andar == false && player2.GetComponent<Movimento>().acabouMinigame == true){

                    player2.GetComponent<Movimento>().acabouMinigame = false;
                    // Talvez fazer uma pausa para exibir mensagem antes de mudar o player
                    ChangePlayer();

                }
            
                break;
                
            case 2:

                if(player3.GetComponent<Movimento>().andar == false && player3.GetComponent<Movimento>().acabouMinigame == true){

                    player3.GetComponent<Movimento>().acabouMinigame = false;
                    // Talvez fazer uma pausa para exibir mensagem antes de mudar o player
                    ChangePlayer();

                }

                break;

        }

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

    private void ChangePlayer(){

        canvas.GetComponent<Dado>().jogador = (player + 1) % thePD.players.Count;
        player = canvas.GetComponent<Dado>().jogador;
        theCM.SwitchCamera();

    }

}
