using UnityEngine;

public class NormalEnemyRangeDetectionController
{
	private INormalEnemyView _view;
	private INormalEnemyStatModel _statModel;
	private const float _sameYThreshold = 0.5f;
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

	private bool IsInSameYLevel()
	{
		float selfY = _view.NormalEnemyTransform.position.y;
		float targetY = _view.TargetPosition.y;
		return Mathf.Abs(targetY - selfY) <= _sameYThreshold;
	}
	public bool IsInAttackRange()
	{
		if (_view.UseHorizontialRangeOnly && IsInSameYLevel() == false) return false;

		float range = _statModel.FinalAttackRange;
		return _sqrDistanceToTarget <= (range * range);
	}
	public bool IsInTraceRange()
	{
		if (_view.UseHorizontialRangeOnly && IsInSameYLevel() == false) return false;

		float range = _statModel.FinalTraceRange;
		return _sqrDistanceToTarget <= (range * range);
	}
}
