using UnityEngine;

[CreateAssetMenu(fileName = "Curiosidade", menuName = "ScriptableObjects/Curiosidade")]
public class CuriosidadeScriptableObject : ScriptableObject
{
    [TextArea] public string curiosidade;
}
