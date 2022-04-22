using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{

    private MinigameManager minigameManager;

    public Transform[] casas;

    private float speed = 2f;

    public int numeroCasa = 0;

    public bool andar = false;

    // Movimento alternativo
    public Casa casaAtual;
    public Casa casa;
    public int qtdCasasAndar;
    private GameObject canvas, theCM;
    //********************//

    private void Start()
    {
        minigameManager = FindObjectOfType<MinigameManager>();
        transform.position = casas[numeroCasa].transform.position;
        casaAtual = casa.GetComponent<Casa>();
        canvas = GameObject.Find("Canvas");
        theCM = GameObject.Find("CinemachineManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(andar)
            Andar2();
    }

    private void Andar()
    {
        if(numeroCasa <= casas.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, casas[numeroCasa].transform.position, speed * Time.deltaTime);

            if(transform.position == casas[numeroCasa].transform.position)
            {
                numeroCasa += 1;
            }

        }
    }

    private void Andar2()
    {

        if(qtdCasasAndar >= 0){

            transform.position = Vector2.MoveTowards(transform.position, casaAtual.transform.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,casaAtual.transform.position) < 0.0001f){

                switch(casaAtual.tipoDaCasa){
                    case EstadoMinigame.ParadaObrigatoria:
                        Debug.Log("Parada Obrigatória!");
                        minigameManager.ComecarParadaObrigatoria();
                        casaAtual = casaAtual.proxima;
                        qtdCasasAndar = -2;
                        break;
                    case EstadoMinigame.PedagioOnca:
                        Debug.Log("Pedágio da onça!");
                        minigameManager.ComecarPedagioOnca();
                        casaAtual = casaAtual.proxima;
                        qtdCasasAndar = -2;
                        break;
                    default:
                        casaAtual = casaAtual.proxima;
                        numeroCasa += 1;
                        qtdCasasAndar -= 1;
                        break;
                }

            }

        }else{

            if(qtdCasasAndar == -1){
                casaAtual = casaAtual.anterior;
                numeroCasa -=1;
                TipoDaCasa();
            }

            andar = false;
            
            canvas.GetComponent<Dado>().jogador = (canvas.GetComponent<Dado>().jogador + 1) % 3;
            theCM.GetComponent<CameraMove>().SwitchCamera();
        }

    }

    private void TipoDaCasa(){
        switch(casaAtual.tipoDaCasa){
            case EstadoMinigame.Forca:
                Debug.Log("Forca!");
                minigameManager.ComecarForcaMinigame();
                break;
            case EstadoMinigame.Memoria:
                Debug.Log("Memória!");
                minigameManager.ComecarMemoriaMinigame();
                break;
            case EstadoMinigame.QualAnimal:
                Debug.Log("Qual é o animal?!");
                minigameManager.ComecarQualAnimalMinigame();
                break;
            case EstadoMinigame.QuebraCabeca:
                Debug.Log("Quebra-cabeça!");
                minigameManager.ComecarQuebraCabecaMinigame();
                break;
            case EstadoMinigame.Quiz:
                Debug.Log("Quiz!");
                minigameManager.ComecarQuizMinigame();
                break;
            case EstadoMinigame.SorteReves:
                Debug.Log("Sorte ou revés!");
                minigameManager.ComecarSorteRevesMinigame();
                break;
            case EstadoMinigame.Nenhum:
                break;
        }
    }

}
