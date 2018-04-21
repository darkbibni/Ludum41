using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField] private Orientation playerOrientation;

    public Delegates.SimpleDelegate OnInteract;

    public virtual void Interact(Vector3 dir)
    {
        print("INTERACT with dir : " + dir);

        if (playerOrientation != Orientation.None && dir != Vector3.zero)
        {
            // Check the player orientation when interact.

            print(playerOrientation + " " + OrientationUtility.GetOrientation(dir));

            if(playerOrientation != OrientationUtility.GetOrientation(dir)) {
                return;
            }
        }

        if (OnInteract != null)
        {
            OnInteract();
        }
    }

    public virtual void OnHoverEnd()
    {
        
    }

    public virtual void OnHoverStart()
    {

    }
}
