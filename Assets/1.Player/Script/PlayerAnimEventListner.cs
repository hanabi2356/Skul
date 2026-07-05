using System;
using UnityEngine;

/// <summary>
/// MonoBehavior는 AnimEvent를 호출하기 위해 선언함
/// </summary>
public class PlayerAnimEventListner : MonoBehaviour, IAnimEventListener
{

	public event Action OnAttackStart;
	public event Action OnAttackEnd;

	public void AnimEventAttackEnd()
	{
		OnAttackStart?.Invoke();
	}

	public void AnimEventAttackStart()
	{
		OnAttackEnd?.Invoke();
	}


}
