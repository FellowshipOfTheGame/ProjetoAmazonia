using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Forca : MonoBehaviour
{
    [SerializeField] private GameObject letraPrefab;
    [SerializeField] private TMP_Text palavraAleatoriaText;

    private const int alfabetoTamanho = 26;
    private Button[] letrasButton;
    private GridLayoutGroup gridLayoutGroup;
    private string palavra = "PALAVRA";
    private int erros = 0;
    char[] charArray;
    char[] array;
    bool acertou;

    void Start()
    {
        array = new char[palavra.Length];
        charArray = palavra.ToCharArray();

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = '-';
        }

        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
        letrasButton = new Button[alfabetoTamanho];
        palavraAleatoriaText.text = new string(array);

        for (int i = 'A'; i < alfabetoTamanho + 'A'; i++)
        {
            int indice = i - 'A';
            GameObject letraGameObject =  Instantiate(letraPrefab, gridLayoutGroup.transform, false);
            letrasButton[indice] = letraGameObject.GetComponent<Button>();
            letraGameObject.GetComponentInChildren<TMP_Text>().text = char.ConvertFromUtf32(i);
            letrasButton[indice].onClick.AddListener(delegate { LetraClick(indice); }); 
        }
    }

    private void LetraClick(int indice)
    {
        acertou = false;

        for (int i = 0; i < charArray.Length; i++)
        {
            if (charArray[i] == indice + 'A')
            {
                int numero = indice + 'A';
                array[i] = (char) numero;
                acertou = true;
            }
        }

        if (acertou)
        {
            print("Acertou");
            palavraAleatoriaText.text = new string(array);
        }
        else
        {
            print("Errou");
            erros++;
            // trocar a imagem da forca
        }

        letrasButton[indice].interactable = false;
    }
}
