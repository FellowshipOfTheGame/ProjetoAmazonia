using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dado : MonoBehaviour
{

    public Sprite[] dado;
    private SpriteRenderer rend;
    public int jogador = 0;
    private bool coroutineAllowed = true;
    [SerializeField] private Button map;
    public GameObject telaJogadorVez;
    public TMP_Text mensagemTurno;

    [SerializeField] private AudioClip somDado;
    [SerializeField] private float volumeDado = 1.0f;

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = dado[0];
    }

    private void OnMouseDown() {
        StartCoroutine("JogarDado");
    }

    private IEnumerator JogarDado(){
        
        AudioManager.Instance.PlaySoundEffect(somDado, volumeDado);

        coroutineAllowed = false;
        int faceDado = 0;
        for(int i = 0; i <= 20; i++){
            faceDado = Random.Range(0,6);
            rend.sprite = dado[faceDado];
            yield return new WaitForSeconds(0.05f);
        }

        GameManager.dado = faceDado + 1;
        yield return new WaitForSeconds(2f);
        telaJogadorVez.SetActive(false);
        gameObject.SetActive(false);

        // Desabilitar botões
        map.interactable = false;

        switch (jogador)
        {

            case 0:
                GameManager.MoverJogador(jogador + 1, new Vector3(0,0,0));
                break;
            case 1:
                GameManager.MoverJogador(jogador + 1, new Vector3(0.7f,0,0));
                break;
            case 2:
                GameManager.MoverJogador(jogador + 1, new Vector3(-0.7f,0,0));
                break;

        }

    }

    /*
    [SerializeField]
    private Button dice;

    [SerializeField]
    private Button map;

    public int jogador = 0;
    private int numeroDado = 0;

    public void JogarDado()
    {

        numeroDado = Random.Range(1, 7);
        Debug.Log(numeroDado.ToString());

        GameManager.dado = numeroDado;

        // Desabilitar botões
        dice.interactable = false;
        map.interactable = false;

        switch (jogador)
        {

            case 0:
                GameManager.MoverJogador(jogador + 1);
                break;
            case 1:
                GameManager.MoverJogador(jogador + 1);
                break;
            case 2:
                GameManager.MoverJogador(jogador + 1);
                break;

        }

    }*/

}
