using UnityEngine;

public class NormalEnemyRangeDetectionController
{
	private INormalEnemyView _view;
	private INormalEnemyStatModel _statModel;

    public NormalEnemyRangeDetectionController(INormalEnemyView view,
		INormalEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
	}
	public bool IsInAttackRange(Vector2 currentPosition, Vector2 targetPosition, float attackRange)
	{
		float sqrDistance = (targetPosition - currentPosition).sqrMagnitude;

		return sqrDistance <= (attackRange * attackRange);
	}
	public bool IsInDetectedRange(Vector2 currentPosition, Vector2 targetPosition, float detectedRange)
	{
		float sqrDistance = (targetPosition - currentPosition).sqrMagnitude;

		return sqrDistance <= (detectedRange * detectedRange);
	}

}
