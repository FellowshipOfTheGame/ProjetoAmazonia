using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{

    private MinigameManager minigameManager;
    public Animator animator;

    private float speed = 2f;

    public int numeroCasa = 0;

    public bool andar = false;

    public Casa casaAtual;
    
    public int qtdCasasAndar;
    private GameObject canvas, theCM;

    private void Start()
    {
        minigameManager = FindObjectOfType<MinigameManager>();
        canvas = GameObject.Find("Canvas");
        theCM = GameObject.Find("CinemachineManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(andar)
            Andar();
    }

    private void Andar()
    {

        animator.SetBool("Andar", andar);

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
            animator.SetBool("Andar", andar);
            
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

    public void BonusMinigame(){

        if(qtdCasasAndar >= 0){

            transform.position = Vector2.MoveTowards(transform.position, casaAtual.transform.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,casaAtual.transform.position) < 0.0001f){

                casaAtual = casaAtual.proxima;
                numeroCasa += 1;
                qtdCasasAndar -= 1;
                
            }

        }else{

            casaAtual = casaAtual.anterior;
            numeroCasa -=1;
            andar = false;

        }
            
    }

}
