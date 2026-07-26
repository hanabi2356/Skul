using UnityEngine;

public class NormalEnemyPhysicsHandler : MonoBehaviour
{
	[SerializeField, Label("¹Ù´Ú Layer")] private LayerMask _groundLayer;
	[SerializeField] private Transform _groundChekcObejct;

	private float _rayDistance = 0.3f;
	private float _checkRadius = 0.2f;

	public bool IsGround()
	{
		return Physics2D.OverlapCircle(_groundChekcObejct.position, _checkRadius, _groundLayer);
	}
	public bool IsWallChekc(bool lookRight)
	{
		float lookDir = lookRight ? 1.0f : -1.0f;
		Vector2 ratDir = new Vector2(lookDir, 0.0f);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, ratDir, _rayDistance, _groundLayer);
		return hit.collider != null;
	}
}
