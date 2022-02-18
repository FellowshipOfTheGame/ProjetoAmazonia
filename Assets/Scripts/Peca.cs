using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Peca : MonoBehaviour
{
    private bool _isDragging;
    private RectTransform _pecaRectTransform;
    private RectTransform _gridLayoutGroupRectTransform;
    private int _indice;
    public int indiceCorreto;
    public bool posicaoCorreta;
    
    private Transform _gridLayoutGroupTransform;
    private GridLayoutGroup _gridLayoutGroup;
    private Camera _mainCamera;
    private Vector3 _mousePos;
    public event Action<Peca> OnChanged;
    public event Func<Vector3, int> OnRelease;
    public event Action<int, int> SwapPieces;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _pecaRectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _gridLayoutGroupTransform = transform.parent;
        _gridLayoutGroupRectTransform = _gridLayoutGroupTransform.gameObject.GetComponent<RectTransform>();
        _gridLayoutGroup = _gridLayoutGroupTransform.GetComponent<GridLayoutGroup>();
        OnChanged?.Invoke(this);
    }

    private void Update()
    {
        posicaoCorreta = _pecaRectTransform.rotation.z == 0f;
    }

    public void ClickDown()
    {
        if (_isDragging) return;
        if (QuebraCabeca.ganhou) return;

        //print(transform.GetSiblingIndex());
        _pecaRectTransform.Rotate(0f, 0f, 90f);

        OnChanged?.Invoke(this);
    }

    public void ClickDrag()
    {
        if (!_isDragging)
        {
            _isDragging = true;
            _indice = transform.GetSiblingIndex();
            _gridLayoutGroup.enabled = false;
            //print($"variavel indice click drag {_indice}");
            transform.SetSiblingIndex(8);
        }
        _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        
        if (_mainCamera != null)
        {
            Vector3 objPosition = _mainCamera.ScreenToWorldPoint(_mousePos);
            objPosition.z = 0f;
            _pecaRectTransform.position = objPosition;
        }

        OnChanged?.Invoke(this);
    }

    public void EndDrag()
    {
        _isDragging = false;

        if (OnRelease != null)
        {
            transform.SetSiblingIndex(_indice);
            //print($"variavel indice end drag {_indice}");
            //print($"endereco de origem: {transform.GetSiblingIndex().ToString() }");
            int indexDestiny = OnRelease.Invoke(_mousePos);
                    
            //print($"endereco de destino: { indexDestiny.ToString() }");

            if (indexDestiny != -1)
            {
                SwapPieces?.Invoke(_indice, indexDestiny);
                _indice = indexDestiny;
            }
            else
            {
                transform.SetSiblingIndex(_indice);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_gridLayoutGroupRectTransform);
        }
        else
        {
            Debug.LogWarning("OnRelease is NULL", this);
        }
        
        _gridLayoutGroup.enabled = true;
    }
}
