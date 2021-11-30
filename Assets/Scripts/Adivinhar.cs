using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Adivinhar : MonoBehaviour
{
    [SerializeField] private AdivinharAnimalPlantaScriptableObject[] adivinharAnimalPlantaScriptableObjects;

    private TMP_InputField inputField;
    private Image animalPlantaImage;
    private int randomNumber;

    private void Start()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        animalPlantaImage = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        randomNumber = Random.Range(0, adivinharAnimalPlantaScriptableObjects.Length);

        animalPlantaImage.sprite = adivinharAnimalPlantaScriptableObjects[randomNumber].spriteAnimalPlanta;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResponderButtonClick();
        }
    }

    private void ResponderButtonClick()
    {
        // se alguem nao lembrar de colocar as respostas em lowercase, colocar o adivinhar para .ToLower() tambem
        if (inputField.text.ToLower() == adivinharAnimalPlantaScriptableObjects[randomNumber].resposta)
        {
            // Acertou
        }
        else
        {
            // Errou
        }
    }
}
