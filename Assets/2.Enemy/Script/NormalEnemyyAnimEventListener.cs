using System;
using UnityEngine;

public class NormalEnemyyAnimEventListener : MonoBehaviour, IAnimEventListener
{
	public event Action OnAttackStart;
	public event Action OnAttackEnd;


	public void AnimEventAttackStart()
	{
		OnAttackStart?.Invoke();
	}
	public void AnimEventAttackEnd()
	{
		OnAttackEnd?.Invoke();
	}




}
