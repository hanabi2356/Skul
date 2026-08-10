using UnityEngine;
using UnityEngine.Serialization;

public class NormalEnemyPhysicsHandler : MonoBehaviour
{
	[SerializeField] private LayerMask _groundLayer;
	[FormerlySerializedAs("_groundChekcObejct")] //변수명이 변해도 인스펙터에 할당한 객체의 해제를 막아주는 Attribute
	[SerializeField] private Transform _groundCheckObject;

	private float _rayDistance = 0.3f;
	private float _checkRadius = 0.2f;

	public bool IsGround()
	{
		if (_groundCheckObject == null) return false;
		return Physics2D.OverlapCircle(_groundCheckObject.position, _checkRadius, _groundLayer);
	}

	public bool IsWallCheck(bool lookRight)
	{
		float lookDir = lookRight ? 1.0f : -1.0f;
		Vector2 rayDir = new Vector2(lookDir, 0.0f);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, _rayDistance, _groundLayer);
		return hit.collider != null;
	}
	public bool IsCliffCheck(bool lookRight)
	{
		if(_groundCheckObject == null) return false;

		float lookDir = lookRight ? 1.0f : -1.0f;
		Vector2 origin = (Vector2)_groundCheckObject.transform.position + new Vector2(lookDir * _checkRadius, 0.0f);
		RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, _rayDistance + _checkRadius, _groundLayer);
		return hit.collider == null;
	}
}
