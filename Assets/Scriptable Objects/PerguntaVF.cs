using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerguntaVF", menuName = "ScriptableObjects/PerguntaVF")]
public class PerguntaVF : ScriptableObject
{
    [TextArea] public string pergunta;
    public bool respostaCorreta;
}
