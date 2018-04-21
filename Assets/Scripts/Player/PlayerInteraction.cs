using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	[SerializeField] private PlayerInput input;
    [SerializeField] private BoxCollider playerCollider;

    private void Awake()
    {
        input.OnAction += TryToInteract;
    }

    private void TryToInteract()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one);
        
        foreach(Collider c in colliders)
        {
            Interactable interactable = c.GetComponent<Interactable>();
            
            if(interactable != null)
            {
                Vector3 origin = transform.position + playerCollider.center;

                Vector3 dir = (c.transform.position - origin).normalized;

                Debug.DrawRay(origin, dir, Color.red, 1f);

                interactable.Interact(dir);
            }
        }
    }
}
