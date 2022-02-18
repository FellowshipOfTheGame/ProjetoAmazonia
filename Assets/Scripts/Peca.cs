using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Peca : MonoBehaviour
{
    private bool _isDragging;
    private RectTransform _pecaRectTransform;
    private RectTransform _gridLayoutGroupRectTransform;
    public int indice;
    public bool posicaoCorreta;
    
    private Transform _gridLayoutGroupTransform;
    private Camera _mainCamera;
    public event Action<Peca> OnChanged;
    public event Func<Vector3, int> OnRelease;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _pecaRectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _gridLayoutGroupTransform = transform.parent;
        _gridLayoutGroupRectTransform = _gridLayoutGroupTransform.gameObject.GetComponent<RectTransform>();
        indice = transform.GetSiblingIndex();
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
        
        _pecaRectTransform.Rotate(0f, 0f, 90f);

        OnChanged?.Invoke(this);
    }

    public void ClickDrag()
    {
        _isDragging = true;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        
        if (_mainCamera != null)
        {
            Vector3 objPosition = _mainCamera.ScreenToWorldPoint(mousePos);
            objPosition.z = 0f;
            _pecaRectTransform.position = objPosition;
        }

        OnChanged?.Invoke(this);
    }

    public void EndDrag()
    {
        _isDragging = false;
        int index = OnRelease(_pecaRectTransform.position);
        print("cuuuu " + index);
        transform.SetSiblingIndex(index);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_gridLayoutGroupRectTransform);
        //OnRelease?.Invoke(this);
    }

    public void EndOfTime()
    {
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = event
        //eventTrigger.triggers.Add()
    }
}
