using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resultados : MonoBehaviour
{
    [SerializeField] private TMP_Text resultadosText;
    [SerializeField] private Image animalImage;
    
    public Button backButton;

    private readonly Color _invisibleImageColor = new Color(0, 0, 0, 0);
    private readonly Vector3 _textPositionWithImage = new Vector3(0f, 206f, 0f);
    private RectTransform _resultadosTextRectTransform;
    private Color visibleImageColor = Color.white;

    private void Awake()
    {
        _resultadosTextRectTransform = resultadosText.GetComponent<RectTransform>();
        backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        animalImage.color = _invisibleImageColor;
    }

    private void OnDisable()
    {
        animalImage.color = _invisibleImageColor;
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }
    
    public void SetText(string text, bool hasImage = false)
    {
        _resultadosTextRectTransform.anchoredPosition = hasImage ? _textPositionWithImage : Vector3.zero;
        resultadosText.text = text;
    }

    public void SetImage(Sprite sprite)
    {
        animalImage.sprite = sprite;
        animalImage.color = visibleImageColor;
    }
}
