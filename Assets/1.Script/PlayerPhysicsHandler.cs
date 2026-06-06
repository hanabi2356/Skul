using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhysicsHandler : MonoBehaviour
{
    private PlayerBase playerBase;

    [SerializeField, Label("¹Ù´Ú Layer")] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckObject;
    

    private float rayDistance = 1.0f;
    private float checkRadius = 0.2f;

    private int oneWayPlatformLayer;
    private int playerLayer;

  




    void Awake()
    {
       playerBase = GetComponent<PlayerBase>();
    }

    private void Start()
    {
        oneWayPlatformLayer = LayerMask.NameToLayer("OneWayPlatform");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        IgnoreOneWayPlatform();
    }
    public bool IsWallCheck()
    {
        float lookDir = playerBase.moveController.gazeVector.x >= 0.0f ? 1.0f : -1.0f;
        Vector2 rayDir = new Vector2(lookDir, 0.0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayDistance, groundLayer );
        //Debug.DrawRay(transform.position, rayDir*rayDistance, Color.yellow);
        return hit.collider != null;
    }
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckObject.position, checkRadius, groundLayer);
    }

    private void IgnoreOneWayPlatform()
    {
        if (playerBase.body.linearVelocity.y > 0.1f)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, oneWayPlatformLayer, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, oneWayPlatformLayer, false);
        }
    }
}
