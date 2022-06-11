using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{

    private MinigameManager minigameManager;
    public Animator animator;

    private float speed = 2f;

    public bool andar = false, acabouMinigame = false, bonus = false;

    public Casa casaAtual;
    
    public int qtdCasasAndar;
    private GameObject canvas, theCM;

    private EstadoMinigame lastMinigame;

    private GameManager theGM;

    private void Start()
    {
        minigameManager = FindObjectOfType<MinigameManager>();
        canvas = GameObject.Find("Canvas");
        theCM = GameObject.Find("CinemachineManager");
        theGM = FindObjectOfType<GameManager>();
        lastMinigame = EstadoMinigame.Nenhum;
    }

    // Update is called once per frame
    void Update()
    {
        if(andar)
            Andar();
        
        if(bonus)
            AndarMinigame();

    }

    private void Andar()
    {

        animator.SetBool("Andar", andar);

        if(qtdCasasAndar >= 0){

            transform.position = Vector2.MoveTowards(transform.position, casaAtual.transform.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,casaAtual.transform.position) < 0.0001f){

                if(casaAtual.tipoDaCasa == EstadoMinigame.ParadaObrigatoria && lastMinigame != EstadoMinigame.ParadaObrigatoria){

                    Debug.Log("Parada Obrigatória!");
                    minigameManager.ComecarParadaObrigatoria();
                    lastMinigame = EstadoMinigame.ParadaObrigatoria;
                    qtdCasasAndar = -2;
                    theGM.ChangePlayer();

                }else if(casaAtual.tipoDaCasa == EstadoMinigame.PedagioOnca && lastMinigame != EstadoMinigame.PedagioOnca){

                    Debug.Log("Pedágio da onça!");
                    minigameManager.ComecarPedagioOnca();
                    lastMinigame = EstadoMinigame.PedagioOnca;
                    qtdCasasAndar = -2;
                    theGM.ChangePlayer();

                }else{

                    casaAtual = casaAtual.proxima;
                    qtdCasasAndar -= 1;

                }

            }

        }else{

            if(qtdCasasAndar == -1){
                casaAtual = casaAtual.anterior;
                TipoDaCasa();
            }

            andar = false;
            animator.SetBool("Andar", andar);
            
        }

    }

    private void TipoDaCasa(){
        switch(casaAtual.tipoDaCasa){
            case EstadoMinigame.Forca:
                Debug.Log("Forca!");
                minigameManager.ComecarForcaMinigame();
                lastMinigame = EstadoMinigame.Forca;
                break;
            case EstadoMinigame.Memoria:
                Debug.Log("Memória!");
                minigameManager.ComecarMemoriaMinigame();
                lastMinigame = EstadoMinigame.Memoria;
                break;
            case EstadoMinigame.QualAnimal:
                Debug.Log("Qual é o animal?!");
                minigameManager.ComecarQualAnimalMinigame();
                lastMinigame = EstadoMinigame.QualAnimal;
                break;
            case EstadoMinigame.QuebraCabeca:
                Debug.Log("Quebra-cabeça!");
                minigameManager.ComecarQuebraCabecaMinigame();
                lastMinigame = EstadoMinigame.QuebraCabeca;
                break;
            case EstadoMinigame.Quiz:
                Debug.Log("Quiz!");
                minigameManager.ComecarQuizMinigame();
                lastMinigame = EstadoMinigame.Quiz;
                break;
            case EstadoMinigame.SorteReves:
                Debug.Log("Sorte ou revés!");
                minigameManager.ComecarSorteRevesMinigame();
                lastMinigame = EstadoMinigame.SorteReves;
                break;
            case EstadoMinigame.Nenhum:
                theGM.ChangePlayer();
                break;
        }
    }

    public void AndarMinigame(){

        animator.SetBool("Andar", bonus);

        if(qtdCasasAndar >= 0){

            transform.position = Vector2.MoveTowards(transform.position, casaAtual.transform.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,casaAtual.transform.position) < 0.0001f){

                casaAtual = casaAtual.proxima;
                qtdCasasAndar -= 1;
                
            }

        }else{

            casaAtual = casaAtual.anterior;
            bonus = false;
            animator.SetBool("Andar", bonus);
            theGM.ChangePlayer();

        }
            
    }

}
