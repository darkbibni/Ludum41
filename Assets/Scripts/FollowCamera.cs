using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Vector3[] limits = { Vector3.zero, Vector3.zero };

    [SerializeField] private float camSpeed = 2f;

    private void LateUpdate()
    {
        Vector3 clampedPosition = player.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, limits[0].x, limits[1].x);

        clampedPosition.y = transform.position.y;
        clampedPosition.z = 0f;
        
        transform.position = Vector3.Lerp(transform.position, clampedPosition, Time.deltaTime * camSpeed);
    }
}
