using UnityEngine;

public class Laser : Trigerrable {

    [SerializeField] private LineRenderer laserRenderer;

    [SerializeField] private float laserLength = 25f;

    [SerializeField] private LayerMask blockableLayer;

    private float hitDistance;
    private GameObject touchedGameobject;

    private bool isActive;

    private void Awake()
    {
        isActive = true;
    }

    public override void Activate()
    {
        isActive = false;

        // Disable the laser.
        laserRenderer.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        HandleRaycast();

        // Resize laser depending what is touched.
        laserRenderer.SetPosition(1, Vector3.forward * hitDistance);

        HandlePlayer();
    }

    private void HandleRaycast()
    {
        Vector3 end = laserRenderer.transform.forward * laserLength;
        Ray r = new Ray(transform.position, end);

        RaycastHit hit;

        hitDistance = laserLength;
        
        if (Physics.Raycast(r, out hit, laserLength, blockableLayer))
        {
            hitDistance = hit.distance;

            touchedGameobject = hit.collider.gameObject;
        }
    }

    private void HandlePlayer()
    {
        if(touchedGameobject.tag == "Player")
        {
            PlayerManager player = touchedGameobject.GetComponent<PlayerManager>();
            player.DetectPlayer();
        }
    }
}
