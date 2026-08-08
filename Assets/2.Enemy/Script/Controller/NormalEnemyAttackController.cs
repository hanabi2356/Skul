using UnityEngine;

public class NormalEnemyAttackController
{
	private readonly INormalEnemyStatModel _statModel;
	private readonly INormalEnemyView _view;
	private float _lastAttackEndTime = -999.0f;
	private float _attackStartTime;
	private bool _damageApplied;

	public bool IsAttacking { get; private set; }

	public bool IsOnCoolDown =>
		Time.time - _lastAttackEndTime < _statModel.FinalAttackCoolTime;

	public bool CanAttack => IsAttacking == false && IsOnCoolDown == false;

	private float MaxAttackDuration => _statModel.FinalAttackSpeed > 0.0f ? _statModel.FinalAttackSpeed : 1.0f;

	public NormalEnemyAttackController(INormalEnemyView view, INormalEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
	}

	public void TryAttack()
	{
		if (CanAttack == false) return;

		IsAttacking = true;
		_damageApplied = false;
		_view.SetIsAttacking(true);
		_attackStartTime = Time.time;
	}

	/// <summary>
	/// 애니 이벤트가 없어도 공격이 끝나도록 안전장치
	/// </summary>
	public void Tick()
	{
		if (IsAttacking && Time.time - _attackStartTime >= MaxAttackDuration)
		{
			OnAttackEnd();
		}
	}

	public void OnAttackStart()
	{
		if (IsAttacking == false || _damageApplied) return;

		var player = PlayerTransformProvider.PlayerTransform;
		if(player == null) return;

		var playerPresenter = player.GetComponentInParent<PlayerPresenter>();
		if (playerPresenter == null) return;


		_damageApplied = true;
	}

	public void OnAttackEnd()
	{
		if (IsAttacking == false) return;

		IsAttacking = false;
		_view.SetIsAttacking(false);
		_lastAttackEndTime = Time.time;
	}

	public void CancelAttack()
	{
		IsAttacking = false;
		//피격 시 취소 로직 추가
		_view.SetIsAttacking(false);
	}
}
