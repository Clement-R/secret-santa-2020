using UnityEngine;

[CreateAssetMenu(fileName = "SFX", menuName = "Game/SFX", order = 1)]
public class SFX : ScriptableObject
{
    public AudioClip Clip;
    public float Volume = 1f;
}