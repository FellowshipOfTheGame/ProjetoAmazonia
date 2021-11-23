using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SorteReves : MonoBehaviour
{
    public SorteRevesScriptableObject[] sorteRevesScriptableObjects;
    [SerializeField] private TMP_Text textoSorteado;

    //private Players[] players = new Players[4];

    private void Awake()
    {
        //players = FindObjectsOfType<Player>();
    }

    private void OnEnable()
    {
        int numSorteado = Random.Range(0, sorteRevesScriptableObjects.Length);
        textoSorteado.text = sorteRevesScriptableObjects[numSorteado].texto;
    }

    public void BotaoVoltarClick()
    {
        //players.AndarCasas(sorteRevesScriptableObjects[numSorteado].casasAndadas);
        gameObject.SetActive(false);
    }
}
