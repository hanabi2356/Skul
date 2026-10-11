using UnityEngine;

public abstract class SkillBase : ISkillBase
{
	protected readonly IBossEnemyView _view;
	protected readonly IEnemyStatModel _statModel;

	private float _startedAt;
	private float _lastEndTime = -999.0f;
	private bool _isRunning;
	private bool _isFinished;

	public bool IsFinished => _isFinished;
	public bool IsRunning => _isRunning;

	protected virtual float Duration =>
		_statModel.FinalAttackSpeed > 0.0f ? _statModel.FinalAttackSpeed : 1.0f;

	protected virtual float CoolTime => _statModel.FinalAttackCoolTime;

	protected SkillBase(IBossEnemyView view, IEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
	}

	public virtual bool CanExecute()
	{
		if (_isRunning) return false;
		return Time.time - _lastEndTime >= CoolTime;
	}

	public virtual void Begin()
	{
		if (_isRunning) return;

		_isRunning = true;
		_isFinished = false;
		_startedAt = Time.time;
		// UseSkill 잠금 전에 바라볼 방향을 먼저 맞춘다.
		OnBegin();
		_view?.SetUseSkill(true);
	}

	public virtual void Tick()
	{
		if (_isRunning == false) return;

		OnTick();

		if (Time.time - _startedAt >= Duration)
		{
			End();
		}
	}

	public virtual void End()
	{
		if (_isRunning == false) return;

		OnEnd();
		FinishInternal();
	}

	public virtual void Cancel()
	{
		if (_isRunning == false)
		{
			_isFinished = true;
			return;
		}

		OnCancel();
		FinishInternal();
	}

	private void FinishInternal()
	{
		_isRunning = false;
		_isFinished = true;
		_lastEndTime = Time.time;
		_view?.SetUseSkill(false);
	}

	protected abstract void OnBegin();
	protected virtual void OnTick() { }
	protected virtual void OnEnd() { }
	protected virtual void OnCancel() { }

	protected void FaceTarget()
	{
		if (_view?.BossEnemyTransform == null) return;

		bool lookRight = _view.TargetPosition.x >= _view.BossEnemyTransform.position.x;
		_view.SetRotation(lookRight);
	}
}
