using System;
using UnityEngine;

public class BossEnemyAnimEventListener : MonoBehaviour, IAnimEventListener
{
	public event Action OnAttackStart;
	public event Action OnAttackEnd;
	public event Action OnAttackHit;
	public void AnimEventAttackStart()
	{
		OnAttackStart?.Invoke();
	}
	public void AnimEventAttackEnd()
	{
		OnAttackEnd?.Invoke();
	}
	public void AnimEventAttackHit()
	{
		OnAttackHit?.Invoke();
	}

}
