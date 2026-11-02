using UnityEngine;

public class MagicSpellLaser : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _sprite;
	private float _lifeTime;
	private float _elapsed;
	private bool _isActive;

	private void Awake()
	{
		if (_sprite == null)
		{
			_sprite = GetComponentInChildren<SpriteRenderer>();
		}
	}

	public void Initialize(int damage, Vector2 direction, Vector2 origin, float length, float thickness, LayerMask targetLayer, float lifeTime)
	{
		_lifeTime = lifeTime;
		_elapsed = 0f;
		_isActive = true;
		Aim(origin, direction, length);
		ApplyDamage(damage, direction, origin, length, thickness, targetLayer);
	}

	private void Aim(Vector2 origin, Vector2 direction, float length)
	{
		_sprite.transform.localRotation = Quaternion.identity;
		_sprite.transform.localPosition = Vector3.zero;

		float spriteLength = Mathf.Max(0.01f, _sprite.sprite.bounds.size.y);
		float parentScaleY = 1.0f;
		float parentScaleX = 1.0f;
		if (transform.parent != null)
		{
			Vector3 parentScale = transform.parent.lossyScale;
			if (Mathf.Abs(parentScale.y) > 0.0001f)
			{
				parentScaleY = parentScale.y;
			}
			if (Mathf.Abs(parentScale.x) > 0.0001f)
			{
				parentScaleX = parentScale.x;
			}
		}

		transform.position = origin + direction * (length * 0.5f);
		transform.up = direction;

		Vector3 scale = transform.localScale;
		scale.y = (length / spriteLength) / parentScaleY;
		scale.x = 1f / parentScaleX;
		transform.localScale = scale;

	}

	private void ApplyDamage(int damage, Vector2 direction, Vector2 origin, float length, float thickness, LayerMask targetLayer)
	{
		Vector2 size = new Vector2(length, thickness);
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Vector2 boxOrigin = origin + direction * (length * 0.5f);
		Collider2D[] hits = Physics2D.OverlapBoxAll(boxOrigin, size, angle, targetLayer);

		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i] == null) continue;

			PlayerPresenter player = hits[i].GetComponentInChildren<PlayerPresenter>();

			if (player == null) continue;

			player.ApplyDamage(damage);
			return;
		}

	}
	private void Update()
	{
		if (_isActive == false) return;

		_elapsed += Time.deltaTime;
		if (_elapsed < _lifeTime) return;

		_isActive = false;
		Destroy(gameObject);
	}

}
