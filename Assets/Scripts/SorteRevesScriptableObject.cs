using UnityEngine;

[CreateAssetMenu(fileName = "SorteOuReves", menuName = "ScriptableObjects/SorteOuReves")]
public class SorteRevesScriptableObject : ScriptableObject
{
    [TextArea] public string texto;
    public int casasAndadas;
}
