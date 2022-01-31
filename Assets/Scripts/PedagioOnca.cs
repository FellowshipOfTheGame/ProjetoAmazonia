using UnityEngine;
using UnityEngine.UI;

public class PedagioOnca : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(delegate { BackButtonClick(); });
    }

    private void OnEnable()
    {
        // PEGAR UMA FRASE ALEATORIA
    }

    private void BackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
