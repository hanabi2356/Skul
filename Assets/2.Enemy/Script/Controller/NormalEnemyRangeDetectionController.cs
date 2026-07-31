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
	private float SqrDistanceToTarget => (_view.TargetPosition - 
		(Vector2)_view.NormalEnemyTransform.position).sqrMagnitude;
	public bool IsInAttackRange()
	{
		float range = _statModel.FinalAttackRange;


		return SqrDistanceToTarget <= (range * range);
	}
	public bool IsInTraceRange()
	{
		float range = _statModel.FinalTraceRange;
		return SqrDistanceToTarget <= (range * range);
	}
	public bool CanMove => _statModel.FinalMoveSpeed > 0.0f;
}
