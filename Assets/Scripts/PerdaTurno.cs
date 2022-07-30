using UnityEngine;

public class PerdaTurno : MonoBehaviour
{   

    private Dado dado;
    private GameManager theGM;
    int player;

    [SerializeField] private AudioClip somPerdaTurno;
    [SerializeField] private float volumePerdaTurno = 1.0f;

    private void Awake()
    {
        dado = FindObjectOfType<Dado>();
        theGM = FindObjectOfType<GameManager>();
    }

    private void OnEnable(){
        player = GameManager.player;
        Debug.Log(player);
        AudioManager.Instance.PlaySoundEffect(somPerdaTurno, volumePerdaTurno);
    }

    public void FecharMensagem(){

        Debug.Log(player);
        theGM.jogadores[player].perdeTurno = 1;
        theGM.ChangePlayer();
        gameObject.SetActive(false);

    }

}
