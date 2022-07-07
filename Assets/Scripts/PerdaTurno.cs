using UnityEngine;

public class PerdaTurno : MonoBehaviour
{   

    private Dado dado;
    private GameManager theGM;
    int player;

    [SerializeField] private AudioClip somPederTurno;
    [SerializeField] private float volumeSomResposta = 1.0f;

    private void Awake()
    {
        dado = FindObjectOfType<Dado>();
        theGM = FindObjectOfType<GameManager>();
    }

    private void OnEnable(){
        player = dado ? dado.jogador : 0;
        AudioManager.Instance.PlaySoundEffect(somPederTurno, volumeSomResposta);
    }

    public void FecharMensagem(){

        theGM.jogadores[player].perdeTurno = 1;
        theGM.ChangePlayer();
        gameObject.SetActive(false);

    }

}
