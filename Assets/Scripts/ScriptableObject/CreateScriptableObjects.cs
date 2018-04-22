using UnityEngine;
using UnityEditor;

public class CreateScriptableObjects
{
    [MenuItem("Assets/Create/Item")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ItemData>();
    }
}