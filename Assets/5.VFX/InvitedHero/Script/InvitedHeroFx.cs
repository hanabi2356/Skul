using System;
using UnityEngine;

public enum InvitedHeroFxKind
{
	SlashDown,
	SlashUp,
	Flurry,
	DashSlash,
	SwordWave,
	SwordStorm,
	SwordLine,
	Slam,
	Lightning,
	Orb,
	SmokeLow,
	SmokeWrap,
	SmokeHigh,
}

/// <summary>
/// 초대 용사 1페이즈 이펙트를 위치에 재생한다.
/// 애니메이션 이벤트에서는 PlayKind에 enum 번호를 넘긴다.
/// </summary>
public class InvitedHeroFxPlayer : MonoBehaviour
{
	[Serializable]
	public struct Entry
	{
		public InvitedHeroFxKind kind;
		public GameObject prefab;
	}

	[SerializeField] private Entry[] _entries;

	public void PlayKind(int kind)
	{
		var faceRight = transform.lossyScale.x >= 0f;
		Play((InvitedHeroFxKind)kind, transform.position, faceRight);
	}

	public GameObject Play(InvitedHeroFxKind kind, Vector2 position, bool faceRight)
	{
		var prefab = Find(kind);
		if (prefab == null)
		{
			return null;
		}

		var go = Instantiate(prefab, position, Quaternion.identity);
		var scale = go.transform.localScale;
		scale.x = Mathf.Abs(scale.x) * (faceRight ? 1f : -1f);
		go.transform.localScale = scale;
		return go;
	}

	private GameObject Find(InvitedHeroFxKind kind)
	{
		if (_entries == null)
		{
			return null;
		}

		for (var i = 0; i < _entries.Length; i++)
		{
			if (_entries[i].kind == kind)
			{
				return _entries[i].prefab;
			}
		}

		return null;
	}
}
