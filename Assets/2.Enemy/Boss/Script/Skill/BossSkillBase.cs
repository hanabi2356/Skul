using UnityEngine;

public abstract class BossSkillBase : ISkill
{
	protected readonly IBossEnemyView _view;
	protected readonly IEnemyStatModel _statModel;

	private float _lastEndTime = -999.0f;
	private bool _hitApplied;

	protected BossSkillBase(IBossEnemyView view, IEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
	}

	public abstract int SkillNumber { get; }
	protected abstract float CoolTime { get; }

	public bool IsRunning {get; private set;}
	public virtual bool CanUse => IsRunning == false && Time.time - _lastEndTime >= CoolTime;

	

	public void Enter()
	{
		IsRunning = true;
		_hitApplied = false;
		OnEnter();
	}

	public void Exit()
	{
		if (IsRunning == false) return;
		OnExit();
		Finish();
	}

	public void OnHit()
	{
		if (IsRunning == false || _hitApplied) return;
		if(OnApplyHit())
		{
			_hitApplied = true;
		}
	}

	public void Tick()
	{
		if (IsRunning == false) return;
		OnTick();
	}

	public void Cancle()
	{
		if (IsRunning == false) return;
		OnCancel();
		Finish();
	}

	private void Finish()
	{
		IsRunning = false;
		_lastEndTime = Time.time;
		_hitApplied = false;
	}
	protected virtual void OnEnter() { }
	protected virtual void OnTick() { }
	protected abstract bool OnApplyHit(); // 실제 데미지/스폰
	protected virtual void OnExit() { }
	protected virtual void OnCancel() { }
}
