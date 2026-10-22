using UnityEngine;

public abstract class BossSkillBase : ISkill
{
	protected readonly IBossEnemyView _view;
	protected readonly IEnemyStatModel _statModel;
	protected LayerMask _targetLayer;
	private float _lastEndTime = -999.0f;
	private bool _hitApplied;

	protected BossSkillBase(IBossEnemyView view, IEnemyStatModel statModel)
	{
		_view = view;
		_statModel = statModel;
		_targetLayer = _view.TargetLayer;
	}

	public abstract int SkillNumber { get; }
	protected abstract float CoolTime { get; }

	public bool IsRunning {get; private set;}
	public virtual bool CanUse => IsRunning == false && Time.time - _lastEndTime >= CoolTime;

	public virtual float MaxUseRange => 0.0f;

	public bool IsEndRequested { get; private set; }

	public void Enter()
	{
		IsRunning = true;
		IsEndRequested = false;
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
		IsEndRequested = false;
		_lastEndTime = Time.time;
		_hitApplied = false;
		
	}
	

	protected virtual void OnEnter()
	{ 
		FaceTarget(); 
	}

	protected virtual void OnTick() { }
	/// <summary>
	/// 데미지를 입히는 로직이 들어가는 함수
	/// 원거리 탄환 소환도 이 함수에서 실행 
	/// </summary>
	/// <returns></returns>
	protected abstract bool OnApplyHit(); 
	protected virtual void OnExit() { }
	protected virtual void OnCancel() { }

	protected void FaceTarget()
	{
		if (_view.BossEnemyTransform == null) return;
		bool lookRight = _view.TargetPosition.x >= _view.BossEnemyTransform.position.x;
		_view.SetRotation(lookRight);
	}

	protected bool TryApplyDamage(Collider2D[] hits)
	{
		if(hits == null || hits.Length == 0) return false;

		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i]==null) continue;

			var playerPresenter = hits[i].GetComponentInChildren<PlayerPresenter>();
			if (playerPresenter == null) continue;

			playerPresenter.ApplyDamage(_statModel.FinalDamage);
			return true;
		}

		return false;
	}

	protected void RequestedEnd()
	{
		IsEndRequested = true;
	}

	
}
