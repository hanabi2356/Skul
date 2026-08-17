using UnityEngine;
using Zenject;

public class NormalEnemyPresenter : MonoBehaviour
{
	[SerializeField] private string _enemyID = "101";

	private INormalEnemyStatModel _statModel;
	private INormalEnemyView _view;
	private NormalEnemyDataLoader _dataLoader;
	private NormalEnemyFSMMachine _fsm;
	private NormalEnemyAnimController _animController;
	private NormalEnemyAttackController _attackController;
	private NormalEnemyRegistry _enemyRegistry;
	private bool _isInitialized;

	[Inject]
	public void Initialize(INormalEnemyStatModel statModel,
		INormalEnemyView view,
		NormalEnemyDataLoader dataLoader,
		NormalEnemyFSMMachine fsm,
		NormalEnemyAnimController animController,
		NormalEnemyAttackController attackController,
		NormalEnemyRegistry enemyRegistry)
	{
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;
		_fsm = fsm;
		_animController = animController;
		_attackController = attackController;
		_enemyRegistry = enemyRegistry;

		if (_view == null)
		{
			Debug.LogError("NormalEnemyView가 주입되지 않았습니다.");
			return;
		}

		_dataLoader.EnemyStatDataToCache();

		EnemyStatDataDTO data = _dataLoader.Get(_enemyID);
		if (data != null)
		{
			_statModel.UpdateFinalStat(data);
		}
		else
		{
			Debug.LogError($"EnemyStat 로드 실패 [ID: {_enemyID}]");
			return;
		}

		_fsm.BootUp();
		SubscribeEvent();
		_isInitialized = true;
		
		if(_enemyRegistry != null)
		{
			_enemyRegistry.Registry(this);
		}
	}

	void Update()
	{
		if (_isInitialized == false || _view == null) return;
		if (_fsm.CurrentStateEnum == ENormalEnemyState.Dead) return;

		var playerTransform = PlayerTransformProvider.PlayerTransform;
		if (playerTransform == null) return;
		

		_view.UpdateTargetPosition(playerTransform.position);
		_fsm.CurrentState?.Execute();
		_animController?.UpdateAnimation(_fsm.CurrentStateEnum);
	}

	
	private void SubscribeEvent()
	{
		_statModel.OnHPChanged += OnHPChanged;

		if (_view.NormalEnemyAnimEventListener != null)
		{
			_view.NormalEnemyAnimEventListener.OnAttackStart += _attackController.OnAttackStart;
			_view.NormalEnemyAnimEventListener.OnAttackEnd += _attackController.OnAttackEnd;
		}
		else
		{
			Debug.LogWarning("NormalEnemyAnimEventListener가 없습니다. 공격 애니 이벤트를 연결하세요.", this);
		}
	}

	/// <summary>
	/// 외부(플레이어 공격 등)에서 호출
	/// </summary>
	public void ApplyDamage(int damage)
	{
		if (_isInitialized == false || _statModel.IsDead) return;
		_statModel.TakeDamage(damage);
	}

	private void OnHPChanged(int currentHP)
	{
		if (currentHP <= 0)
		{
			_fsm.ChangeState(_fsm.DeadState, ENormalEnemyState.Dead);
			return;
		}

		if (_fsm.CurrentStateEnum != ENormalEnemyState.Hit
			&& _fsm.CurrentStateEnum != ENormalEnemyState.Dead)
		{
			_fsm.ChangeState(_fsm.HitState, ENormalEnemyState.Hit);
		}
	}

	private void OnDisable()
	{
		if (_isInitialized == false) return;

		if (_statModel != null)
		{
			_statModel.OnHPChanged -= OnHPChanged;
		}

		if (_view?.NormalEnemyAnimEventListener != null && _attackController != null)
		{
			_view.NormalEnemyAnimEventListener.OnAttackStart -= _attackController.OnAttackStart;
			_view.NormalEnemyAnimEventListener.OnAttackEnd -= _attackController.OnAttackEnd;
		}

		if (_enemyRegistry != null)
		{
			_enemyRegistry.Unregistry(this);
		}
	}
}
