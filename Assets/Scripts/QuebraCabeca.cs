using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuebraCabeca : MonoBehaviour
{
    private Peca[] pecas;
    private int pecasCorretas = 0;
    public static bool ganhou = false;
    GameObject[] pecasGameObjects;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float tempoEmSegundos = 10;
    private float tempo;

    // Start is called before the first frame update
    void Awake()
    {
        pecas = FindObjectsOfType<Peca>();
        pecasGameObjects = GameObject.FindGameObjectsWithTag("Peca");
    }

    private void OnEnable()
    {
        tempo = tempoEmSegundos;
        FisherYatesShuffle(pecasGameObjects);
    }

    private void Update()
    {
        if (tempoEmSegundos > 0)
        {
            tempo -= Time.deltaTime;
            MostrarTempo(tempo);
        }
        else
        {
            Debug.Log("Player perdeu");
        }
    }

    private void MostrarTempo(float tempoParaMostrar)
    {
        if (tempoParaMostrar < 0)
        {
            tempoParaMostrar = 0f;
        }

        float minutos = Mathf.FloorToInt(tempoParaMostrar / 60);
        float segundos = Mathf.CeilToInt(tempoParaMostrar % 60);
        // Se for usar milisegundos, trocar a linha acima para Mathf.FloorToInt()
        //float milisegundos = tempoParaMostrar % 1 * 1000;

        //timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milisegundos);
        timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    private void FixedUpdate()
    {
        foreach(Peca peca in pecas)
        {
            if (peca.transform.rotation.z == 0f)
            {
                pecasCorretas++;
            }
            else
            {
                pecasCorretas--;
            }
        }
    }

    private void FisherYatesShuffle(GameObject[] array)
    {
        //System.Random random = new System.Random();
        int tamanho = array.Length;
        int randomRotationIndex;
        float[] rotations = { 0f, 90f, 180f, -90f };

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);
            randomRotationIndex = Random.Range(0, rotations.Length);

            GameObject t = array[r];
            array[r] = array[i];
            array[r].transform.SetSiblingIndex(i);
            array[i].transform.SetSiblingIndex(r);
            array[i].transform.Rotate(0f, 0f, rotations[randomRotationIndex]);
            array[i] = t;
        }

        randomRotationIndex = Random.Range(0, rotations.Length);
        array[tamanho - 1].transform.Rotate(0f, 0f, rotations[randomRotationIndex]);
    }
}
