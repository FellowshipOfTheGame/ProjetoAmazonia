using UnityEngine;

[CreateAssetMenu(fileName = "Pergunta", menuName = "ScriptableObjects/Pergunta")]
public class PerguntasScriptableObject : ScriptableObject
{
    [TextArea] public string pergunta;
    [TextArea] public string[] respostas;
    public int respostaCorreta;
}
