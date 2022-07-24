using UnityEngine;

[CreateAssetMenu(fileName = "NewBGMData", menuName = "ScriptableObjects/BGMData")]
public class BGMData : ScriptableObject
{
    public AudioClip clip;
    public BGMData nextBGM;

}