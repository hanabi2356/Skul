using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform groundCheckObject;

    private float checkRadius = 0.2f;
    void Awake()
    {
       
    }

    void Update()
    {
      
    }
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckObject.position, checkRadius, groundLayer);
    }
}
