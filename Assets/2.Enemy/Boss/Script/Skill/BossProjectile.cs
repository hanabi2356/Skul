using UnityEngine;

public class BossProjectile : MonoBehaviour, IEnemyProjectile
{
	[SerializeField, Label("발사체 속도")] private float _projectileVelocity = 1.0f;
	[SerializeField, Label("수명")] private float _lifeTime = 5.0f;

	private int _damage;
	private Vector2 _direction;
	private bool _isActive;
	private float _elpased;
	private IEnemyProjectilePool _pool;

	public GameObject Root => gameObject;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_isActive == false) return;
		if (other.CompareTag("Player") == false) return;
		var playerPresenter = other.GetComponentInChildren<PlayerPresenter>();
		playerPresenter.ApplyDamage(_damage);
		Despawn();
	}

	public void BindPool(IEnemyProjectilePool pool)
	{
		_pool = pool;
	}

	public void Initialize(int damage, Vector2 direction, Vector2 spawnPosition)
	{
		_damage = damage;
		_direction = direction;
		_elpased = 0.0f;
		transform.position = spawnPosition;
		_isActive = true;
		transform.right = _direction;
	}

	private void Despawn()
	{
		_isActive = false;
		_pool?.Release(this);
	}

	private void Update()
	{
		if(_isActive == false) return;
		transform.Translate(_direction * _projectileVelocity * Time.deltaTime, Space.World);

		_elpased += Time.deltaTime;
		if(_elpased >= _lifeTime)
		{
			Despawn();
		}
	}
}
