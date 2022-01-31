using UnityEngine;

[CreateAssetMenu(fileName = "AdinharAnimalPlanta", menuName = "ScriptableObjects/AdivinharAnimalPlanta")]
public class AdivinharAnimalPlantaScriptableObject : ScriptableObject
{
    public Sprite spriteAnimalPlanta;

    [TextArea] public string[] respostas;
    public int respostaCorreta;
}
