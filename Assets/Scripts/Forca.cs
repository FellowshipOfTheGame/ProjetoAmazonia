using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Forca : MonoBehaviour
{
    [SerializeField] private GameObject letraPrefab;
    [SerializeField] private TMP_Text palavraAleatoriaText;
    [SerializeField] private ForcaScriptableObject[] forcaScriptableObjects;

    private const int alfabetoTamanho = 26;
    private Button[] letrasButton;
    private GridLayoutGroup gridLayoutGroup;
    //private string palavra = "PALAVRA";
    private int erros = 0;
    private char[] charArray;
    private char[] palavra;
    private bool acertou;
    private int jogador = 0;
    private int[] ordemJogada = new int[4] { 0, 1, 2, 3 };

    void Start()
    {
        gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
        letrasButton = new Button[alfabetoTamanho];

        for (int i = 'A'; i < alfabetoTamanho + 'A'; i++)
        {
            int indice = i - 'A';
            GameObject letraGameObject =  Instantiate(letraPrefab, gridLayoutGroup.transform, false);
            letrasButton[indice] = letraGameObject.GetComponent<Button>();
            letraGameObject.GetComponentInChildren<TMP_Text>().text = char.ConvertFromUtf32(i);
            letrasButton[indice].onClick.AddListener(delegate { LetraClick(indice); });
        }
    }

    private void OnEnable()
    {
        int randomNumber = Random.Range(0, forcaScriptableObjects.Length);
        palavra = new char[forcaScriptableObjects[randomNumber].animal.Length];
        charArray = forcaScriptableObjects[0].animal.ToCharArray();

        for (int i = 0; i < palavra.Length; i++)
        {
            palavra[i] = '-';
        }

        jogador = 0;
        FisherYatesShuffle(ordemJogada);
        palavraAleatoriaText.text = new string(palavra);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < letrasButton.Length; i++)
        {
            letrasButton[i].onClick.RemoveAllListeners();
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
                palavra[i] = (char) numero;
                acertou = true;
            }
        }
        if (acertou)
        {
            print("Acertou");
            palavraAleatoriaText.text = new string(palavra);
            
            if (!palavra.Contains('-'))
            {
                print($"O jogador {jogador} acertou");
            }
        }
        else
        {
            print("Errou");
            erros++;
            // trocar a imagem da forca

            if (erros >= 6)
            {
                print("Todos perdem");

                return;
            }

            for (int i = 0; i < ordemJogada.Length; i++)
            {
                if (ordemJogada[i] == jogador)
                {
                    jogador = ordemJogada[(i + 1) % ordemJogada.Length];
                    break;
                }
            }
        }

        letrasButton[indice].interactable = false;
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
