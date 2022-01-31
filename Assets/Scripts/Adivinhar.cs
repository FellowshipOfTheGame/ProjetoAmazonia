using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Adivinhar : MonoBehaviour
{
    [SerializeField] private AdivinharAnimalPlantaScriptableObject[] adivinharAnimalPlantaScriptableObjects;

    private Image animalPlantaImage;
    private int randomNumber;
    private int jogador = 0;
    private int[] ordemJogada = new int[4] { 0, 1, 2, 3 };
    private Button[] buttons;

    private void Awake()
    {
        animalPlantaImage = GetComponentsInChildren<Image>()[1];
        buttons = GetComponentsInChildren<Button>();
    }

    private void OnEnable()
    {
        randomNumber = Random.Range(0, adivinharAnimalPlantaScriptableObjects.Length);
        animalPlantaImage.sprite = adivinharAnimalPlantaScriptableObjects[randomNumber].spriteAnimalPlanta;
        jogador = 0;
        Debug.LogWarning("Lembrar de pegar o jogador que começa o minigame de outro script");
        FisherYatesShuffle(ordemJogada);
    }

    public void ResponderButtonClick(int indice)
    {
        buttons[indice].interactable = false;
    }

    private void FisherYatesShuffle(int[] array)
    {
        int tamanho = array.Length;

        for (int i = 0; i < tamanho - 1; i++)
        {
            int r = i + Random.Range(0, tamanho - i);

            int t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}
