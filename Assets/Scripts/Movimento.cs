using UnityEngine;

public class Movimento : MonoBehaviour
{

    private MinigameManager minigameManager;
    public Animator animator;

    private float speed = 2f;

    public bool andar = false, bonus = false, paraFrente = true, terminou = false;
    public int perdeTurno = 0;

    public Casa casaAtual;
    
    public int qtdCasasAndar;
    private GameObject canvas, theCM;

    public AudioClip somDeAndar;
    public float volumeSomDeAndar = 1.0f;
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

                AudioManager.Instance.PlaySoundEffect(somDeAndar, volumeSomDeAndar);

                if(casaAtual.tipoDaCasa == EstadoMinigame.ParadaObrigatoria && lastMinigame != EstadoMinigame.ParadaObrigatoria){

                    Debug.Log("Parada Obrigatória!");
                    minigameManager.ComecarParadaObrigatoria();
                    lastMinigame = EstadoMinigame.ParadaObrigatoria;
                    qtdCasasAndar = -2;
                    theGM.ChangePlayer(); // Tirar depois que pronto

                }else if(casaAtual.tipoDaCasa == EstadoMinigame.PedagioOnca && lastMinigame != EstadoMinigame.PedagioOnca){

                    Debug.Log("Pedágio da onça!");
                    minigameManager.ComecarPedagioOnca();
                    lastMinigame = EstadoMinigame.PedagioOnca;
                    qtdCasasAndar = -2;
                    theGM.ChangePlayer(); // Tirar depois que pronto

                }else{

                    if(casaAtual.proxima != null){
                        casaAtual = casaAtual.proxima;
                        qtdCasasAndar -= 1;
                    }else{
                        qtdCasasAndar = -3;
                    }

                }

            }

        }else{

            if(qtdCasasAndar == -1){
                casaAtual = casaAtual.anterior;
                TipoDaCasa();
            }else if(qtdCasasAndar == -3){
                terminou = true;
                theGM.Chegada(); // Salvar posição que chegou no vetor do GameManager
            }

            andar = false;
            animator.SetBool("Andar", andar);
            
            if(terminou == true){
                theGM.ChangePlayer();
            }
            
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
            case EstadoMinigame.PerdaTurno:
                Debug.Log("Perda de turno");
                // Só deve funcionar para jogos com mais de um player
                // Deve desativar quando só restar um player jogando
                minigameManager.ComecarPerdaTurno();
                lastMinigame = EstadoMinigame.PerdaTurno;
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

                if(paraFrente == true){
                    casaAtual = casaAtual.proxima;
                }else{
                    casaAtual = casaAtual.anterior;
                }
                qtdCasasAndar -= 1;
                
            }

        }else{

            if(paraFrente == true){
                if(qtdCasasAndar == -1){
                    casaAtual = casaAtual.anterior;
                }else if(qtdCasasAndar == -3){
                    terminou = true;
                    theGM.Chegada(); // Salvar posição que chegou no vetor do GameManager
                }
            }else{
                casaAtual = casaAtual.proxima;
            }
            

            //casaAtual = casaAtual.anterior;
            bonus = false;
            animator.SetBool("Andar", bonus);
            theGM.ChangePlayer();

        }
            
    }

}
