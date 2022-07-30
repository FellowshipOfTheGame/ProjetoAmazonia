using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{

    public Sprite[] dado;
    private SpriteRenderer rend;
    public int jogador = 0;
    private bool coroutineAllowed = true;
    [SerializeField]
    private Button map;

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = dado[0];
    }

    private void OnMouseDown() {
        StartCoroutine("JogarDado");
    }

    private IEnumerator JogarDado(){
        
        coroutineAllowed = false;
        int faceDado = 0;
        for(int i = 0; i <= 20; i++){
            faceDado = Random.Range(0,6);
            rend.sprite = dado[faceDado];
            yield return new WaitForSeconds(0.05f);
        }

        GameManager.dado = faceDado + 1;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

        // Desabilitar botões
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
