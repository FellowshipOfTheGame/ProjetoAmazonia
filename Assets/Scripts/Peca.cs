using System;
using UnityEngine;
using UnityEngine.UI;

public class Peca : MonoBehaviour
{
    public event Action<Peca> OnChanged;
    public event Func<Vector3, int> OnRelease;
    public event Action<int, int> SwapPieces;
    
    public int indiceAtual;
    public int indiceCorreto;
    public bool posicaoCorreta;
    
    private Transform _gridLayoutGroupTransform;
    private GridLayoutGroup _gridLayoutGroup;
    private Camera _mainCamera;
    private Vector3 _mousePos;
    private RectTransform _pecaRectTransform;
    //private RectTransform _gridLayoutGroupRectTransform;
    private bool _isDragging;

    private void Awake()
    {
        indiceCorreto  = transform.GetSiblingIndex();
        _mainCamera = Camera.main;
        _pecaRectTransform = GetComponent<RectTransform>();
        _gridLayoutGroupTransform = transform.parent;
        _gridLayoutGroup = _gridLayoutGroupTransform.GetComponent<GridLayoutGroup>();
        //_gridLayoutGroupRectTransform = _gridLayoutGroupTransform.gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        indiceAtual = transform.GetSiblingIndex();
        OnChanged?.Invoke(this);
    }

    public void ClickDown()
    {
        if (_isDragging) return;
        if (QuebraCabeca.Ganhou) return;

        //print(transform.GetSiblingIndex());
        _pecaRectTransform.Rotate(0f, 0f, 90f);

        OnChanged?.Invoke(this);
    }

    public void ClickDrag()
    {
        if (!_isDragging)
        {
            _isDragging = true;
            indiceAtual = transform.GetSiblingIndex();
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

        //OnChanged?.Invoke(this);
    }

    public void EndDrag()
    {
        _isDragging = false;

        if (OnRelease != null)
        {
            transform.SetSiblingIndex(indiceAtual);
            //print($"variavel indice end drag {_indice}");
            //print($"endereco de origem: {transform.GetSiblingIndex().ToString() }");
            int indexDestiny = OnRelease.Invoke(_mousePos);
                    
            //print($"endereco de destino: { indexDestiny.ToString() }");

            if (indexDestiny != -1)
            {
                SwapPieces?.Invoke(indiceAtual, indexDestiny);
                //OnChanged?.Invoke(this);
            }
            else
            {
                transform.SetSiblingIndex(indiceAtual);
            }
            
            //LayoutRebuilder.ForceRebuildLayoutImmediate(_gridLayoutGroupRectTransform);
        }
        else
        {
            Debug.LogWarning("OnRelease is NULL", this);
        }
        
        _gridLayoutGroup.enabled = true;
    }
}
