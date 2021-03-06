using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public PlayersData thePD;
    public CameraMove theCM;
    private GameObject[] partida;
    private static GameObject player1, player2, player3;
    public GameObject personagem1, personagem2, personagem3;
    private GameObject prefab, canvas;

    public GameObject telaJogadorVez;

    [SerializeField]
    private Button map;

    public static int dado, player;

    private int rank;

    private int[] ordemChegada, personagensEscolhidos;

    public Movimento[] jogadores;

    public TMP_Text mensagemTurno;

    [SerializeField] private GameObject dadoNovo;

    void Awake(){
        
        theCM = FindObjectOfType<CameraMove>();
        canvas = GameObject.Find("Canvas");
        partida = GameObject.FindGameObjectsWithTag("Partida");

    }

    void Start() {
        
        thePD = PlayersData.instance;
        DefinirPersonagens();
        player = dadoNovo.GetComponent<Dado>().jogador;
        dado = 0;
        rank = 0;
        
        mensagemTurno.text = "Vamos lá, Jogador 1!";
        telaJogadorVez.SetActive(true);
        dadoNovo.SetActive(true);

    }

   
    private void DefinirPersonagens(){
        
        int qtdPlayers = thePD.players.Count;
        ordemChegada = new int[qtdPlayers];
        personagensEscolhidos = new int[qtdPlayers];
        jogadores = new Movimento[qtdPlayers];

        for(int i = 0; i < qtdPlayers; i++){

            Debug.Log(thePD.players[i].character);
            personagensEscolhidos[i] = thePD.players[i].character;

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
                    jogadores[i] = player1.GetComponent<Movimento>();
                    theCM.p1Cam.Follow = player1.transform;
                    break;
                case 1:
                    player2 = prefab;
                    player2.name = "Player 2";
                    player2.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    player2.GetComponent<Movimento>().andar = false;
                    jogadores[i] = player2.GetComponent<Movimento>();
                    theCM.p2Cam.Follow = player2.transform;
                    break;
                case 2:
                    player3 = prefab;
                    player3.name = "Player 3";
                    player3.GetComponent<Movimento>().casaAtual = partida[i].GetComponent<Casa>();
                    player3.GetComponent<Movimento>().andar = false;
                    jogadores[i] = player3.GetComponent<Movimento>();
                    theCM.p3Cam.Follow = player3.transform;
                    break;

            }

        }

    }

    public static void MoverJogador(int jogador, Vector3 offset)
    {

        switch (jogador)
        {

            case 1:
                player1.GetComponent<Movimento>().qtdCasasAndar = dado;
                player1.GetComponent<Movimento>().andar = true;
                player1.GetComponent<Movimento>().offset = offset;
                break;
            
            case 2:
                player2.GetComponent<Movimento>().qtdCasasAndar = dado;
                player2.GetComponent<Movimento>().andar = true;
                player2.GetComponent<Movimento>().offset = offset;
                break;

            case 3:
                player3.GetComponent<Movimento>().qtdCasasAndar = dado;
                player3.GetComponent<Movimento>().andar = true;
                player3.GetComponent<Movimento>().offset = offset;
                break;

        }
        
    }

    public void ChangePlayer(){

        if(rank != thePD.players.Count){

            dadoNovo.GetComponent<Dado>().jogador = (player + 1) % thePD.players.Count;
            player = dadoNovo.GetComponent<Dado>().jogador;

            if(jogadores[player].perdeTurno > 0){
                jogadores[player].perdeTurno = 0;
                ChangePlayer();
                return;
                //dadoNovo.GetComponent<Dado>().jogador = (player + 1) % thePD.players.Count;
                //player = dadoNovo.GetComponent<Dado>().jogador;
            }

            if(jogadores[player].terminou == true){

                ChangePlayer();
                return;
                //dadoNovo.GetComponent<Dado>().jogador = (player + 1) % thePD.players.Count;
                //player = dadoNovo.GetComponent<Dado>().jogador;

            }
            
            theCM.SwitchCamera(player);

            mensagemTurno.text = $"Sua vez, Jogador {(player+1).ToString()}!";
            telaJogadorVez.SetActive(true);

            dadoNovo.SetActive(true);

            // Reabilitar botões
            map.interactable = true;

        }

    }

    public void BonusMinigame(int jogador, int numCasasAndar, bool useShortcut = false, bool useRemainingSteps = true){

        switch (jogador)
        {

            case 0:
                theCM.SwitchCamera(jogador);
                player1.GetComponent<Movimento>().paraFrente = numCasasAndar >= 0 ? true : false;
                print("Qtd a andar: " + player1.GetComponent<Movimento>().qtdCasasAndar );
                player1.GetComponent<Movimento>().bonus = true;
                player1.GetComponent<Movimento>().useShortcut = useShortcut;
                player1.GetComponent<Movimento>().qtdCasasAndar = (useRemainingSteps ? player1.GetComponent<Movimento>().remainingSteps : 0) + Mathf.Abs(numCasasAndar);
                break;
            
            case 1:
                theCM.SwitchCamera(jogador);
                player2.GetComponent<Movimento>().paraFrente = numCasasAndar >= 0 ? true : false;
                player2.GetComponent<Movimento>().bonus = true;
                player2.GetComponent<Movimento>().useShortcut = useShortcut;
                player2.GetComponent<Movimento>().qtdCasasAndar = (useRemainingSteps ? player2.GetComponent<Movimento>().remainingSteps : 0) +Mathf.Abs(numCasasAndar);
                break;

            case 2:
                theCM.SwitchCamera(jogador);
                player3.GetComponent<Movimento>().paraFrente = numCasasAndar >= 0 ? true : false;
                player3.GetComponent<Movimento>().bonus = true;
                player3.GetComponent<Movimento>().useShortcut = useShortcut;
                player3.GetComponent<Movimento>().qtdCasasAndar = (useRemainingSteps ? player3.GetComponent<Movimento>().remainingSteps : 0) +  Mathf.Abs(numCasasAndar);
                break;

        }
        
    }

    private void FimDeJogo(int[] ordemChegada){

        // Desativar botões de jogo
        map.interactable = false;

        // Atualizar informações do painel de resultado        
        canvas.GetComponent<PainelResultado>().UpdateResultados(ordemChegada, personagensEscolhidos, thePD.players.Count);

        // Mostrar resultados
        canvas.GetComponent<PainelResultado>().ShowResultado();

    }

    public void Chegada(){

        Debug.Log(rank);
        Debug.Log(thePD.players.Count);
        ordemChegada[rank] = player + 1;
        rank = rank + 1;

        if(rank == thePD.players.Count){
            FimDeJogo(ordemChegada);
        }

    }

}
