using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class QuebraCabeca : MonoBehaviour
{
    private Peca[] _pecas;
    private int pecasCorretas;
    public static bool ganhou = false;
    private GameObject[] _pecasGameObjects;
    private RectTransform[] _pecasRectTransforms;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float tempoEmSegundosParaCronometro = 10;
    private float _tempoRestante;
    private bool _pararTempo;
    private int _player;

    void Awake()
    {
        _pecas = FindObjectsOfType<Peca>();
        int pecasLength = _pecas.Length;
        _pecasGameObjects = new GameObject[pecasLength];
        _pecasRectTransforms = new RectTransform[pecasLength];

        for (int i = 0; i < pecasLength; i++)
        {
            _pecasGameObjects[i] = _pecas[i].gameObject;
            _pecasRectTransforms[i] = _pecasGameObjects[i].GetComponent<RectTransform>();
        }
        
        foreach (Peca peca in _pecas)
        {
            peca.OnChanged += VerificarAcerto;
            peca.OnRelease += VerifyPieceOverMouseDrag;
            peca.SwapPieces += Swap;
        }
    }

    private void OnEnable()
    {
        _tempoRestante = tempoEmSegundosParaCronometro;
        FisherYatesShuffle(_pecasGameObjects);
    }

    private void Update()
    {
        if (tempoEmSegundosParaCronometro > 0)
        {
            if (_pararTempo) return;
            
            _tempoRestante -= Time.deltaTime;
            MostrarTempo(_tempoRestante);
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
            peca.OnRelease -= VerifyPieceOverMouseDrag;
            peca.SwapPieces -= Swap;
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

        _pararTempo = true;
        //ganhou = true;
        Debug.Log($"Player { _player.ToString() } ganhou!", this);
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
            
            Transform transformI = _pecasGameObjects[i].transform;
            
            array[r].transform.SetSiblingIndex(i);
            transformI.SetSiblingIndex(r);
            transformI.Rotate(0f, 0f, rotations[randomRotationIndex]);
        }

        randomRotationIndex = Random.Range(0, rotations.Length);
        array[tamanho - 1].transform.Rotate(0f, 0f, rotations[randomRotationIndex]);
    }

    private void Swap(int indexA, int indexB)
    {
        Transform transformB = transform.GetChild(0).GetChild(indexB);

        //print($"Swap {indexA} e {indexB}");
        transform.GetChild(0).GetChild(indexA).SetSiblingIndex(indexB);
        transformB.SetSiblingIndex(indexA);
    }

    private int VerifyPieceOverMouseDrag(Vector3 pecaPosition)
    {
        EventSystem currentEventSystem = EventSystem.current;
        PointerEventData pointerEventData = new PointerEventData(currentEventSystem) { position = pecaPosition };
        List<RaycastResult> raycasts = new List<RaycastResult>();
        currentEventSystem.RaycastAll(pointerEventData, raycasts);
        
        //print($"Antes: { raycasts.Count.ToString() }");
        
        for (int i = 0; i < raycasts.Count; i++)
        {
            if (!raycasts[i].gameObject.CompareTag("Peca"))
            {
                raycasts.Remove(raycasts[i]);
                i--;
            }
        }

        //print($"Depois: { raycasts.Count.ToString() }");

        return raycasts.Count > 1 ? raycasts[1].gameObject.transform.GetSiblingIndex() : -1;
    }
}
