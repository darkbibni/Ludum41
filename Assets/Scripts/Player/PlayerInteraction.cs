using UnityEngine;

/// <summary>
/// Permit to interact with all elements around the player.
/// </summary>
public class PlayerInteraction : MonoBehaviour {

	[SerializeField] private PlayerInput input;
    [SerializeField] private CapsuleCollider playerCollider;

    [SerializeField] LayerMask interactableMask;

    public Delegates.SimpleDelegate OnInteract;

    private void Awake()
    {
        input.OnAction += TryToInteract;
        input.OnDiscreteAction += TryToInteractWithDiscretion;
    }

    private void TryToInteract()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, interactableMask);
        
        foreach(Collider c in colliders)
        {
            Interactable interactable = c.GetComponent<Interactable>();
            
            if(interactable != null)
            {
                Vector3 origin = transform.position + playerCollider.center;
                Vector3 firstDir = (c.transform.position - origin).normalized;
                
                // Correct dir. 
                Vector3 correctedDir = GetUnitVector(firstDir);

                // Debug dir.
                Debug.DrawRay(origin, firstDir, Color.red, 1f);
                Debug.DrawRay(origin, correctedDir, Color.green, 1f);

                // Interact with the element.
                interactable.Interact(correctedDir);

                Interact(correctedDir);
            }
        }
    }

    private void TryToInteractWithDiscretion()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, interactableMask);
        
        foreach (Collider c in colliders)
        {
            Interactable interactable = c.GetComponent<Interactable>();

            if (interactable != null)
            {
                if(!interactable.HasDiscretInteraction)
                {
                    continue;
                }

                Vector3 origin = transform.position + playerCollider.center;
                Vector3 firstDir = (c.transform.position - origin).normalized;

                // Correct dir. 
                Vector3 correctedDir = GetUnitVector(firstDir);
                
                if(correctedDir != Vector3.one)
                {
                    // Debug dir.
                    Debug.DrawRay(origin, firstDir, Color.red, 1f);
                    Debug.DrawRay(origin, correctedDir, Color.green, 1f);

                    // Interact with the element.
                    interactable.DiscreteInteract(correctedDir);

                    Interact(correctedDir);
                }
            }
        }
    }

    private void Interact(Vector3 dir)
    {
        if (OnInteract != null)
        {
            OnInteract();
        }
    }

    // Check player placement.
    private Vector3 GetUnitVector(Vector3 dir)
    {
        Vector3 unitDir = Vector3.one;

        float absX = Mathf.Abs(dir.x);
        float absZ = Mathf.Abs(dir.z);

        // Player is on the cell.
        if(absX == absZ && absX == 0f)
        {
            return Vector3.zero;
        }

        // Take largest component of the vector then take his sign.
        if (absX > absZ)
        {
            unitDir = Mathf.Sign(dir.x) * Vector3.right;
        }

        else if(absX < absZ)
        {
            unitDir = Mathf.Sign(dir.z) * Vector3.forward;
        }
        
        return unitDir;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1.5f);
    }
}
