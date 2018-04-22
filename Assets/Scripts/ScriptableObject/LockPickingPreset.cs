using UnityEngine;

[System.Serializable]
public class LockPickingPreset : ScriptableObject
{
    [Tooltip("Speed of the micro game.")]
    public float speed = 1f;

    [Tooltip("Fragment when the player can unlock the vitrin.")]
    [Range(0f, 1f)]
    public float size = 0.2f;
}