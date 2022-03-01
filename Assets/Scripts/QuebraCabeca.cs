using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class QuebraCabeca : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private QuebraCabecaScriptableObject[] quebraCabecaScriptableObjects;
    [SerializeField] private float tempoEmSegundosParaCronometro = 10;
    
    public static bool Ganhou;
    
    private Peca[] _pecas;
    private GameObject[] _pecasGameObjects;
    private Image[] _pecasImages;
    private RectTransform[] _pecasRectTransforms;
    
    private int _pecasCorretas;
    private float _tempoRestante;
    private bool _pararTempo;
    private int _player;

    void Awake()
    {
        _pecas = FindObjectsOfType<Peca>();
        int pecasLength = _pecas.Length;
        _pecasGameObjects = new GameObject[pecasLength];
        _pecasRectTransforms = new RectTransform[pecasLength];
        _pecasImages = new Image[pecasLength];

        for (int i = 0; i < pecasLength; i++)
        {
            _pecasGameObjects[i] = _pecas[i].gameObject;
            _pecasRectTransforms[i] = _pecasGameObjects[i].GetComponent<RectTransform>();
            _pecasImages[i] = _pecasGameObjects[i].GetComponent<Image>();
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
        int randomNumber = Random.Range(0, quebraCabecaScriptableObjects.Length);
        Ganhou = false;
        _pecasCorretas  = 0;

        for (int i = 0 ; i < _pecas.Length; i++)
        {
            _pecasImages[i].sprite = quebraCabecaScriptableObjects[randomNumber].sprites[_pecas.Length - i - 1];
        }
        
        _tempoRestante = tempoEmSegundosParaCronometro;
        FisherYatesShuffle(_pecasGameObjects);
    }

    private void Update()
    {
        if (_tempoRestante > 0)
        {
            if (_pararTempo) return;
            
            _tempoRestante -= Time.deltaTime;
            MostrarTempo(_tempoRestante);
        }
        /*else if (perdeu)
        {
            Debug.Log("Player perdeu", this);
        }*/
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
        //print($"{peca.name}: Indice Atual: {peca.indiceAtual.ToString()} Indice Correto: {peca.indiceCorreto.ToString()}");

        //print($"{peca.indiceCorreto == peca.indiceAtual} && { Mathf.Abs(peca.gameObject.transform.rotation.z) <= Mathf.Epsilon }");
        //print($"abs: {Mathf.Abs(peca.gameObject.transform.rotation.eulerAngles.z)} sem abs: {peca.gameObject.transform.rotation.eulerAngles.z}");
        
        if (peca.indiceCorreto == peca.indiceAtual &&
            Mathf.Abs(peca.gameObject.transform.rotation.eulerAngles.z) <= 0.01f)
        {
            if (!peca.posicaoCorreta)
            {
                _pecasCorretas++;
                peca.posicaoCorreta = true;
            }
        }
        else
        {
            if (peca.posicaoCorreta)
            {
                _pecasCorretas--;
                peca.posicaoCorreta = false;
            }
        }

        //print(_pecasCorretas.ToString());
        
        if (_pecasCorretas != _pecas.Length) return;

        _pararTempo = true;
        Ganhou = true;
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
        timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
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
            _pecasRectTransforms[i].rotation = Quaternion.Euler(0f, 0f, rotations[randomRotationIndex]);
        }

        randomRotationIndex = Random.Range(0, rotations.Length);
        _pecasRectTransforms[tamanho - 1].rotation = Quaternion.Euler(0f, 0f, rotations[randomRotationIndex]);
    }

    private void Swap(int indexA, int indexB)
    {
        Transform transformChild = transform.GetChild(0);
        Transform transformB = transformChild.GetChild(indexB);
        Transform transformA = transformChild.GetChild(indexA);
        Peca pecaA = transformA.GetComponent<Peca>();
        Peca pecaB = transformB.GetComponent<Peca>();

        pecaA.indiceAtual = indexB;
        pecaB.indiceAtual = indexA;
        
        transformA.SetSiblingIndex(indexB);
        transformB.SetSiblingIndex(indexA);
        
        VerificarAcerto(pecaA);
        VerificarAcerto(pecaB);
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
