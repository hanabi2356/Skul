using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhysicsHandler : MonoBehaviour
{
	
    [SerializeField, Label("¹Ù´Ú Layer")] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckObject;
    

    private float rayDistance = 0.3f;
    private float checkRadius = 0.2f;

    private int oneWayPlatformLayer;
    private int playerLayer;


    void Awake()
    {
    }

    private void Start()
    {
        oneWayPlatformLayer = LayerMask.NameToLayer("OneWayPlatform");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    public bool IsWallCheck(bool lookRight)
    {
		float lookDir = lookRight ? 1.0f : -1.0f;
		Vector2 rayDir = new Vector2(lookDir, 0.0f);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayDistance, groundLayer);

		return hit.collider != null;
	}
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckObject.position, checkRadius, groundLayer);
    }
	public void SetOneWayPlatformIgnore(bool ignore)
	{
		Physics2D.IgnoreLayerCollision(playerLayer, oneWayPlatformLayer, ignore);
	}
	
}
