using UnityEngine;
using UnityEngine.UI;

public class PedagioOnca : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Awake()
    {
        backButton.onClick.AddListener(BackButtonClick);
    }
    
    /*
    private void OnEnable()
    {
        PEGAR UMA FRASE ALEATORIA
    }
    */

    private void BackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
