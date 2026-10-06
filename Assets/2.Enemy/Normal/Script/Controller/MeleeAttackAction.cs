using UnityEngine;

public class MeleeAttackAction : INormalEnemyAttackAction
{

	private readonly INormalEnemyStatModel _statModel;
	private readonly NormalEnemyRangeDetectionController _rangeController;

	private bool _damageApplied;

	public MeleeAttackAction(INormalEnemyStatModel statModel, 
		NormalEnemyRangeDetectionController rangeController)
	{
		_statModel = statModel;
		_rangeController = rangeController;
	}

	public void Execute()
	{
		if (_damageApplied) return;
		if ( _rangeController == null ||_rangeController.IsInAttackRange() == false) return;

		var player = PlayerTransformProvider.PlayerTransform;
		if (player == null) return;

		var playerPresenter = player.GetComponentInChildren<PlayerPresenter>();
		if (playerPresenter == null) return;

		playerPresenter.ApplyDamage(_statModel.FinalDamage);
		_damageApplied = true;

	}

	public void Reset()
	{
		_damageApplied = false;
	}


}
