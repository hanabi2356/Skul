using System;
using UnityEngine;

public class NormalEnemyView : MonoBehaviour ,INormalEnemyView
{
	private Vector2 _targetPosition;
	public Vector2 TargetPosition => _targetPosition;

	public event Action OnAttack;

	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}

	
}
