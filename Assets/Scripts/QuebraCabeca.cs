using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class QuebraCabeca : MonoBehaviour
{
    private struct PecaPosicaoIndice
    {
        public Vector3 posicao;
        public int indice;

        public PecaPosicaoIndice(Vector3 position, int index)
        {
            posicao = position;
            indice = index;
        }
    }
    
    private Peca[] _pecas;
    private int pecasCorretas = 0;
    public static bool ganhou = false;
    private GameObject[] pecasGameObjects;
    private RectTransform[] pecasRectTransforms;
    //private Vector3[] pecasIni

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float tempoEmSegundosParaCronometro = 10;
    private float tempoRestante;
    private bool pararTempo = false;
    private int player = 0;

    private PecaPosicaoIndice[] _pecaPosicaoIndices;
    // Start is called before the first frame update
    void Awake()
    {
        _pecas = FindObjectsOfType<Peca>();
        int pecasLength = _pecas.Length;
        pecasGameObjects = new GameObject[pecasLength];
        pecasRectTransforms = new RectTransform[pecasLength];
        _pecaPosicaoIndices = new PecaPosicaoIndice[pecasLength];

        for (int i = 0; i < pecasLength; i++)
        {
            pecasGameObjects[i] = _pecas[i].gameObject;
            pecasRectTransforms[i] = pecasGameObjects[i].GetComponent<RectTransform>();
        }
        
        foreach (Peca peca in _pecas)
        {
            peca.OnChanged += VerificarAcerto;
            peca.OnRelease += VerifyDistance;
        }

        //GameObject.FindGameObjectsWithTag("Peca");
    }

    private void OnEnable()
    {
        tempoRestante = tempoEmSegundosParaCronometro;
        FisherYatesShuffle(pecasGameObjects);
    }

    private void Start()
    {
        print(transform.GetChild(0));
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(0).GetComponent<RectTransform>());
        for (int i = 0; i < _pecas.Length; i++)
        {
            _pecaPosicaoIndices[i] = new PecaPosicaoIndice(pecasRectTransforms[i].position, i);
            print(_pecaPosicaoIndices[i].posicao.ToString());
        }
    }

    private void Update()
    {
        if (tempoEmSegundosParaCronometro > 0)
        {
            if (pararTempo) return;
            
            tempoRestante -= Time.deltaTime;
            MostrarTempo(tempoRestante);
        }
        else
        {
            Debug.Log("Player perdeu", this);
        }
    }

    private void OnDestroy()
    {
        foreach (Peca peca in _pecas)
        {
            peca.OnChanged -= VerificarAcerto;
            peca.OnRelease -= VerifyDistance;
        }
    }

    private void VerificarAcerto(Peca peca)
    {
        if (peca.posicaoCorreta)
        {
            pecasCorretas++;
        }
        else
        {
            pecasCorretas = pecasCorretas <= 0 ? 0 : pecasCorretas--;
        }

        if (pecasCorretas != _pecas.Length) return;

        pararTempo = true;
        ganhou = true;
        Debug.Log($"Player { player.ToString() } ganhou!", this);
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
        timerText.text = string.Format("{0:00}:{1:00}", minutos.ToString(), segundos.ToString());
    }

    private void FisherYatesShuffle(GameObject[] array)
    {
        int tamanho = array.Length;
        int randomRotationIndex;
        float[] rotations = { 0f, 90f, 180f, -90f };

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);
            randomRotationIndex = Random.Range(0, rotations.Length);
            
            array[r].transform.SetSiblingIndex(i);
            array[i].transform.SetSiblingIndex(r);
            array[i].transform.Rotate(0f, 0f, rotations[randomRotationIndex]);
        }

        randomRotationIndex = Random.Range(0, rotations.Length);
        array[tamanho - 1].transform.Rotate(0f, 0f, rotations[randomRotationIndex]);
    }

    private void Swap(GameObject[] gameObjectsArray, int indexA, int indexB)
    {
        gameObjectsArray[indexA].transform.SetSiblingIndex(indexB);
        gameObjectsArray[indexB].transform.SetSiblingIndex(indexA);
    }

    private int VerifyDistance(Vector3 position)
    {
        //float[] distances = new float[_pecaPosicaoIndices.Length];
        float smallest = 10000;
        int index = 0;
        
        for (int i = 0; i < _pecaPosicaoIndices.Length; i++)
        {
            float distance = Vector2.Distance(position, _pecaPosicaoIndices[i].posicao);

            if (!(smallest > distance)) continue;
            
            smallest = distance;
            index = i;
        }
        
        //return _pecaPosicaoIndices[index].indice;
        return index;
    }
}
