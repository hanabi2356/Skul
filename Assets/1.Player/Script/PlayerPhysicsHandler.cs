using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhysicsHandler : MonoBehaviour
{
	
    [SerializeField, Label("바닥 Layer")] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckObject;
    

    private float _rayDistance = 0.3f;
    private float _checkRadius = 0.2f;

    private int _oneWayPlatformLayer;
    private int _playerLayer;


    void Awake()
    {
    }

    private void Start()
    {
        _oneWayPlatformLayer = LayerMask.NameToLayer("OneWayPlatform");
        _playerLayer = LayerMask.NameToLayer("Player");
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
		RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, _rayDistance, _groundLayer);

		return hit.collider != null;
	}
    public bool IsGround()
    {
        return Physics2D.OverlapCircle(_groundCheckObject.position, _checkRadius, _groundLayer);
    }
	public void SetOneWayPlatformIgnore(bool ignore)
	{
		//parameter: 충돌을 제어할 오브젝트의 레이어, 무시할 오브젝트, 무시 여부에 대한 boolean값
		Physics2D.IgnoreLayerCollision(_playerLayer, _oneWayPlatformLayer, ignore);
	}
	

}
