using UnityEngine;

/// <summary>
/// 원샷 이펙트 루트를 수명이 끝나면 없앤다.
/// </summary>
public class InvitedHeroFxLifetime : MonoBehaviour
{
	[SerializeField] private float _life = 1.2f;
	private float _elapsed;

	private void Update()
	{
		_elapsed += Time.deltaTime;
		if (_elapsed >= _life)
		{
			Destroy(gameObject);
		}
	}
}
