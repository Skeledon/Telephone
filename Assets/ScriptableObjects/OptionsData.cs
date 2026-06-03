using UnityEngine;

[CreateAssetMenu(fileName = "OptionsData", menuName = "ScriptableObjects/OptionsData", order = 7)]
public class OptionsData : ScriptableObject
{
    public Vector2Int Resolution;
    public bool FullScreen;
    public float Volume;
}
