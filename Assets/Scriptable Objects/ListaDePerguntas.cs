using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListaDePerguntas", menuName = "ScriptableObjects/ListaDePerguntas")]
public class ListaDePerguntas : ScriptableObject
{
    public List<PerguntasScriptableObject> perguntasAlternativas;
    public List<PerguntaVF> perguntasVF;

}
