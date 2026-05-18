using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    private PlayerBase playerBase;

    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private Transform groundCheckObject;
   

    private float rayDistance = 1.0f;

    private float checkRadius = 0.2f;
    void Awake()
    {
       playerBase = GetComponent<PlayerBase>();
    }

    void Update()
    {
      
    }
    public bool IsWallCheck()
    {
        float lookDir = playerBase.moveController.gazeVector.x >= 0.0f ? 1.0f : -1.0f;
        Vector2 rayDir = new Vector2(lookDir, 0.0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayDistance, environmentLayer);
        //Debug.DrawRay(transform.position, rayDir*rayDistance, Color.yellow);
        return hit.collider != null;
    }
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckObject.position, checkRadius, environmentLayer);
    }
}
