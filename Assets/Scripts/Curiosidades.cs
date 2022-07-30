using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Curiosidades : MonoBehaviour
{
    [SerializeField] private List<CuriosidadeScriptableObject> _curiosidades;
    [SerializeField] private TMP_Text curiosidadeText;

    private void OnEnable()
    {
        curiosidadeText.text = _curiosidades[Random.Range(0, _curiosidades.Count)].curiosidade;
    }
    
    public void BackButton()
    {
        gameObject.SetActive(false);
    }
}
