using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carta", menuName = "ScriptableObjects/Carta")]
public class CartasScriptableObject : ScriptableObject
{
    public int indice;
    public Sprite frente;
    public Sprite verso;
}
