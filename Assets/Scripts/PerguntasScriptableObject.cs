using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pergunta", menuName = "ScriptableObjects/Pergunta")]
public class PerguntasScriptableObject : ScriptableObject
{
    public string pergunta;
    public string[] respostas;
    public int respostaCorreta;
}
