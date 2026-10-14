using UnityEngine;
using Zenject;

public class BossEnemyPresenter : MonoBehaviour
{
	[SerializeField] private string _enemyID = "301";

	private IEnemyStatModel _statModel;
	private IBossEnemyView _view;
	private EnemyDataLoader _dataLoader;
	private BossEnemyFSMMachine _fsm;
	private ICurrencyModel _currencyModel;
	private bool _isInitialized;
	private BossEnemyAnimController _animController;
	private BossEnemySkillController _skillController;

	[Inject]
	public void Initialized(IEnemyStatModel statModel,
		IBossEnemyView view,
		EnemyDataLoader dataLoader,
		BossEnemyFSMMachine fsm,
		ICurrencyModel currencyModel,
		BossEnemyAnimController animController,
		BossEnemySkillController skillController)
	{
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;
		_fsm = fsm;
		_currencyModel = currencyModel;
		_animController = animController;
		_skillController = skillController;
		if (_view == null)
		{
			Debug.LogError("BossEnemyView가 주입되지 않았습니다.");
			return;
		}
		_dataLoader.EnemyStatDataToCache();
		var data = _dataLoader.Get(_enemyID);
		if (data == null)
		{
			Debug.LogError($"EnemyStat 로드 실패 [ID: {_enemyID}]");
			return;
		}
		_statModel.UpdateFinalStat(data);
		_fsm.BootUp();
		SubscribeEvent();
		_isInitialized = true;
	}
	void Update()
	{
		if (!_isInitialized || _view == null) return;
		if (_fsm.CurrentStateEnum == EBossEnemyState.Dead) return;
		var player = PlayerTransformProvider.PlayerTransform;
		if (player == null) return;
		_view.UpdateTargetPosition(player.position);
		_fsm.CurrentState?.Execute();

		if(_fsm.CurrentStateEnum == EBossEnemyState.Idle)
		{
			_skillController.TryUseSkill();
		}

		_skillController.Tick();

		_animController?.UpdateAnimation(_fsm.CurrentStateEnum);
	}
	public void ApplyDamage(int damage)
	{
		if (!_isInitialized || _statModel.IsDead) return;
		_statModel.TakeDamage(damage);
	}
	private void SubscribeEvent()
	{
		_statModel.OnHPChanged += OnHPChanged;
		if (_view.AnimEventListener != null)
		{
			_view.AnimEventListener.OnAttackHit += _skillController.OnAttackHit;
			_view.AnimEventListener.OnAttackEnd += _skillController.OnAttackEnd;
		}
	}
	private void OnHPChanged(int currentHP)
	{
		if (currentHP <= 0)
		{
			_fsm.ChangeState(_fsm.DeadState, EBossEnemyState.Dead);
			_currencyModel?.CurrencyAdd(ECurrencyType.Gold, _statModel.FinalDropGold);
			_skillController.Cancel();
			return;
		}
		// 보스 전용 분기 예시:
		// if (페이즈 조건) → PhaseChange
		// if (그로기 조건) → Groggy
		// else Hit (Idle/Attack 중일 때만)
		if (_fsm.CurrentStateEnum != EBossEnemyState.Hit
			&& _fsm.CurrentStateEnum != EBossEnemyState.Dead
			&& _fsm.CurrentStateEnum != EBossEnemyState.PhaseChange
			&& _fsm.CurrentStateEnum != EBossEnemyState.groggy)
		{
			_fsm.ChangeState(_fsm.HitState, EBossEnemyState.Hit);
		}
	}
	private void OnDisable()
	{
		if (!_isInitialized) return;
		if (_statModel != null)
		{
			_statModel.OnHPChanged -= OnHPChanged;
		}
		if (_view?.AnimEventListener != null && _skillController != null)
		{
			_view.AnimEventListener.OnAttackHit -= _skillController.OnAttackHit;
			_view.AnimEventListener.OnAttackEnd -= _skillController.OnAttackEnd;
		}

	}
}
