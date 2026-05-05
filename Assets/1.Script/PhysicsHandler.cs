using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform groundCheckObject;

    void Awake()
    {
       
    }

    void Update()
    {
        
    }
    public bool IsGround()
    {
        return Physics2D.Raycast(groundCheckObject.position, Vector2.down, 0.2f, groundLayer);
    }
}
