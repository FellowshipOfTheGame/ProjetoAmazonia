using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resultados : MonoBehaviour
{
    public TMP_Text resultadosText;
    public Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }
}
