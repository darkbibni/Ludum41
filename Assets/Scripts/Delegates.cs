using UnityEngine;

public class Delegates : MonoBehaviour {

    public delegate void SimpleDelegate();
    public delegate void OnLootItem(Item item);
    public delegate void CallbackDelegate(System.Action callback);
}
