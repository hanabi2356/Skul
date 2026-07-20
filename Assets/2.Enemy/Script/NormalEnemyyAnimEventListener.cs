using System;
using UnityEngine;

public class NormalEnemyyAnimEventListener : MonoBehaviour ,IAnimEventListener
{
	public event Action OnAttackStart;
	public event Action OnAttackEnd;

	public void AnimEventAttackEnd()
	{
		throw new NotImplementedException();
	}

	public void AnimEventAttackStart()
	{
		throw new NotImplementedException();
	}

	
}
