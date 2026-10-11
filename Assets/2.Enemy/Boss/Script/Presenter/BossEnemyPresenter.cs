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
	private BossEnemyAnimController _animController;
	private bool _isInitialized;

	[Inject]
	public void Initialize(
		IEnemyStatModel statModel,
		IBossEnemyView view,
		EnemyDataLoader dataLoader,
		BossEnemyFSMMachine fsm,
		ICurrencyModel currencyModel,
		BossEnemyAnimController animController)
	{
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;
		_fsm = fsm;
		_currencyModel = currencyModel;
		_animController = animController;

		if (_view == null)
		{
			Debug.LogError("BossEnemyView가 주입되지 않았습니다.");
			return;
		}

		_dataLoader.EnemyStatDataToCache();

		EnemyStatDataDTO data = _dataLoader.Get(_enemyID);
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
		if (_isInitialized == false || _view == null) return;
		if (_fsm.CurrentStateEnum == EBossEnemyState.Dead) return;

		var playerTransform = PlayerTransformProvider.PlayerTransform;
		if (playerTransform == null) return;

		_view.UpdateTargetPosition(playerTransform.position);
		_fsm.CurrentState?.Execute();
		_animController?.UpdateAnimation(_fsm.CurrentStateEnum);
	}

	public void ApplyDamage(int damage)
	{
		if (_isInitialized == false || _statModel.IsDead) return;
		_statModel.TakeDamage(damage);
	}

	private void SubscribeEvent()
	{
		_statModel.OnHPChanged += OnHPChanged;
	}

	private void OnHPChanged(int currentHP)
	{
		if (currentHP <= 0)
		{
			_fsm.ChangeState(_fsm.DeadState, EBossEnemyState.Dead);
			_currencyModel?.CurrencyAdd(ECurrencyType.Gold, _statModel.FinalDropGold);
			return;
		}

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
		if (_isInitialized == false) return;

		if (_statModel != null)
		{
			_statModel.OnHPChanged -= OnHPChanged;
		}
	}
}
