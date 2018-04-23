using UnityEngine;
using UnityEditor;

public class CreateScriptableObjects
{
    [MenuItem("Assets/Create/Item")]
    public static void CreateItem()
    {
        ScriptableObjectUtility.CreateAsset<ItemData>();
    }

    [MenuItem("Assets/Create/LockPickingDifficulty")]
    public static void CreateDifficulty()
    {
        ScriptableObjectUtility.CreateAsset<LockPickingPreset>();
    }
}