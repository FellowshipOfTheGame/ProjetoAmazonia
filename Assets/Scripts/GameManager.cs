using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public PlayersData thePD;
    public CameraMove theCM;
    private GameObject[] partida;
    private static GameObject player1, player2, player3;
    public GameObject personagem1, personagem2, personagem3;
    private GameObject prefab, canvas;
    
    [SerializeField]
    private Button dice;

    [SerializeField]
    private Button map;

    public static int dado = 0, player;
    public static int casaJogador1 = 0;
    public static int casaJogador2 = 0;
    public static int casaJogador3 = 0;
    
    //private Movimento[] _playersMovimento;
    //_playersMovimento = FindObjectsOfType<Movimento>();

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

    public void ChangePlayer(){

        canvas.GetComponent<Dado>().jogador = (player + 1) % thePD.players.Count;
        player = canvas.GetComponent<Dado>().jogador;
        theCM.SwitchCamera(player);
        
        // Reabilitar botão do dado
        dice.interactable = true;
        map.interactable = true;

    }

    public void BonusMinigame(int jogador, int numCasasAndar){
        
        switch (jogador)
        {

            case 0:
                theCM.SwitchCamera(jogador);
                player1.GetComponent<Movimento>().qtdCasasAndar = numCasasAndar;
                player1.GetComponent<Movimento>().bonus = true;
                break;
            
            case 1:
                theCM.SwitchCamera(jogador);
                player2.GetComponent<Movimento>().qtdCasasAndar = numCasasAndar;
                player2.GetComponent<Movimento>().bonus = true;
                break;

            case 2:
                theCM.SwitchCamera(jogador);
                player3.GetComponent<Movimento>().qtdCasasAndar = numCasasAndar;
                player3.GetComponent<Movimento>().bonus = true;
                break;

        }
        
    }

}
