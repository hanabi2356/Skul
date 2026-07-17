using UnityEngine;

public class NormalEnemyView : INormalEnemyView
{
	private Vector2 _targetPosition;
	public Vector2 TargetPosition => _targetPosition;

	public void UpdateTargetPosition(Vector2 targetPosition)
	{
		_targetPosition = targetPosition;
	}

	
}
