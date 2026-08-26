using UnityEngine;

public class NormalEnemyRangeDetectionController
{
	private INormalEnemyView _view;
	private INormalEnemyStatModel _statModel;
	public bool CanMove => _statModel.FinalMoveSpeed > 0.0f;
	private float _sqrDistanceToTarget
	{
		get
		{
			Vector2 self = _view.NormalEnemyTransform.position;
			Vector2 target = _view.TargetPosition;

			if(_view.UseHorizontialRangeOnly)
			{
				float dx = target.x - self.x;
				return dx * dx;
			}

			return (target - self).sqrMagnitude;
		}
	}

	public NormalEnemyRangeDetectionController(INormalEnemyView view,
		INormalEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
	}

	public bool IsInAttackRange()
	{
		float range = _statModel.FinalAttackRange;


		return _sqrDistanceToTarget <= (range * range);
	}
	public bool IsInTraceRange()
	{
		float range = _statModel.FinalTraceRange;
		return _sqrDistanceToTarget <= (range * range);
	}
}
