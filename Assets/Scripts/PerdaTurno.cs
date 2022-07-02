using UnityEngine;
using UnityEngine.UI;

public class PerdaTurno : MonoBehaviour
{   

    private Dado dado;
    private GameManager theGM;
    int player;

    private void Awake()
    {
        dado = FindObjectOfType<Dado>();
        theGM = FindObjectOfType<GameManager>();
    }

    private void OnEnable(){
        player = dado ? dado.jogador : 0;
    }

    public void FecharMensagem(){

        theGM.jogadores[player].perdeTurno = 1;
        theGM.ChangePlayer();
        gameObject.SetActive(false);

    }

}
