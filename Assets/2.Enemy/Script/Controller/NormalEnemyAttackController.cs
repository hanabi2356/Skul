using UnityEngine;

public class NormalEnemyAttackController
{
	private readonly INormalEnemyStatModel _statModel;
	private readonly INormalEnemyView _view;
	private readonly MeleeAttackAction _meleeAttackAction;
	private readonly RangeAttackAction _rangeAttackAction;
	private float _lastAttackEndTime = -999.0f;
	private float _attackStartTime;
	public bool IsAttacking { get; private set; }

	public bool IsOnCoolDown =>
		Time.time - _lastAttackEndTime < _statModel.FinalAttackCoolTime;

	public bool CanAttack => IsAttacking == false && IsOnCoolDown == false;

	private float MaxAttackDuration => _statModel.FinalAttackSpeed > 0.0f ? _statModel.FinalAttackSpeed : 1.0f;
	private INormalEnemyAttackAction _currentAction => 
		_statModel.FinalAttackType == AttackType.Melee ? _meleeAttackAction : _rangeAttackAction;

	public NormalEnemyAttackController(INormalEnemyView view, 
		INormalEnemyStatModel statModel,
		MeleeAttackAction meleeAttackAction,
		RangeAttackAction rangeAttackAction)
	{
		_view = view;
		_statModel = statModel;
		_meleeAttackAction = meleeAttackAction;
		_rangeAttackAction = rangeAttackAction;
	}

	public void TryAttack()
	{
		if (CanAttack == false) return;

		IsAttacking = true;
		_currentAction.Reset();
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
		if (IsAttacking == false ) return;
		_currentAction.Execute();
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
