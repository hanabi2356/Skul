using UnityEngine;

public class NormalEnemyMoveController
{
	private readonly INormalEnemyView _view;
	private readonly INormalEnemyStatModel _statModel;
	private bool _lookRight = true;

	public bool LookRight => _lookRight;

	public NormalEnemyMoveController(INormalEnemyView view, INormalEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
	}

	public void MoveToPatrol()
	{
		var physics = _view.PhysicsHandler;
		if (physics != null)
		{
			bool hitWall = physics.IsWallCheck(_lookRight);
			bool noGroundAhead = physics.IsCliffCheck(_lookRight);

			if (hitWall || noGroundAhead)
			{
				_lookRight = !_lookRight;
			}
		}

		ApplyHorizontalMove(_lookRight);
	}

	public void MoveToTrace(Vector2 targetPos)
	{
		_lookRight = targetPos.x >= _view.NormalEnemyTransform.position.x;
		ApplyHorizontalMove(_lookRight);
	}

	public void Stop()
	{
		_view.SetVelocityX(0.0f);
	}

	public void FaceTarget(Vector2 targetPos)
	{
		_lookRight = targetPos.x >= _view.NormalEnemyTransform.position.x;
		_view.SetRotation(_lookRight);
	}

	private void ApplyHorizontalMove(bool lookRight)
	{
		_view.SetRotation(lookRight);
		float dir = lookRight ? 1.0f : -1.0f;
		_view.SetVelocityX(dir * _statModel.FinalMoveSpeed);
	}
}
