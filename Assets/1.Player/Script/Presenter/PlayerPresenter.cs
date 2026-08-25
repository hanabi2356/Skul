using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerPresenter : MonoBehaviour
{
	private IPlayerStatModel _statModel;
	private IPlayerView _view;
	private SkulDataLoader _dataLoader;
	private PlayerMoveController _moveController;
	private PlayerAttackController _attackController;
	private PlayerAnimController _animController;
	private PlayerFSMMachine _fsm;
	private IPlayerHudView _hudView;
	[SerializeField] private DefaultStatData _defaultStatData;

	private bool _isInitialized = false;
	[Inject]
	public void Initialize(IPlayerStatModel statModel, 
		IPlayerView view, 
		SkulDataLoader dataLoader,
		PlayerMoveController moveController,
		PlayerAttackController attackController,
		PlayerAnimController animController,
		PlayerFSMMachine fsm,
		IPlayerHudView hudView)
	{
		
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;
		_moveController = moveController;
		_attackController = attackController;
		_animController = animController;
		_fsm = fsm;
		_hudView = hudView;


		SkulStatData loadData = _dataLoader.SkulStatDataLoad("LittleBorn");
		SubscribeEvent();
		_statModel.UpdateFinalStat(_defaultStatData, loadData);

		_fsm.BootUp();

		_isInitialized = true;
		PlayerTransformProvider.Resgister(_view.PlayerTransform);
	}
	private void Awake()
	{
	}

	private void Start()
	{
		_hudView.Initialize();

		_hudView.SetHP(_statModel.CurrentHP, _statModel.MaxHP);
	}
	private void SubscribeEvent()
	{
		if (_view != null && _moveController != null && _attackController != null)
		{
			_view.OnMove += _moveController.SetMoveInput;
			_view.OnJump += _moveController.TryJump;
			_view.OnPlatformIgnore += _moveController.TryPlatformIgnore;
			_view.OnDash += _moveController.TryDash;
			_view.OnAttack += _attackController.TryAttack;
			_statModel.OnChangeHp += OnHPChanged;
			_statModel.OnStatCaculated += OnStatCaculated;

			SubscribeAttackEvent();
			
		}
		
	}
	
	private void SubscribeAttackEvent()
	{
		if (_view.PlayerAnimEventListener != null)
		{
			_view.PlayerAnimEventListener.OnAttackStart += _attackController.OnAttackStart;
			_view.PlayerAnimEventListener.OnAttackStart += _moveController.OnAttackDash;
			_view.PlayerAnimEventListener.OnAttackEnd += _attackController.OnAttackEnd;

		}
		else
		{
			Debug.Log("PlayerAnimEventLitner null");
		}
	}
	public void ApplyDamage(int damage)
	{
		if (_isInitialized == false || _statModel.CurrentHP <= 0) return;
		_statModel.TakeDamage(damage);
	}

	private void OnHPChanged(int currentHP)
	{
		_hudView.SetHP(currentHP, _statModel.MaxHP);

		if (currentHP <= 0)
		{
			_fsm.ChangeState(_fsm.DeadState, EPlayerState.Dead);
			return;
		}

		if (_fsm.CurrentStateEnum != EPlayerState.Hit && _fsm.CurrentStateEnum != EPlayerState.Dead)
		{
			_fsm.ChangeState(_fsm.HitState, EPlayerState.Hit);
		}
	}

	private void OnStatCaculated()
	{
		_hudView.SetHP(_statModel.CurrentHP, _statModel.MaxHP);
	}
	private void FixedUpdate()
	{
		if (_isInitialized == false) return;
		_moveController.FixedTick();
		
	}

	private void Update()
	{
		if (_isInitialized == false) return;

		_fsm.CurrentState?.Execute();
		_attackController.ComboCoolDown();
		_animController.ChangeAnim(_fsm.CurrentStateEnum, _attackController.AttackCount);
	}

	private void OnDisable()
	{
		if (_isInitialized == false) return;
		if (_view == null || _moveController == null || _attackController == null) return;

		_view.OnMove -= _moveController.SetMoveInput;
		_view.OnJump -= _moveController.TryJump;
		_view.OnPlatformIgnore -= _moveController.TryPlatformIgnore;
		_view.OnDash -= _moveController.TryDash;
		_view.OnAttack -= _attackController.TryAttack;
		_statModel.OnChangeHp -= OnHPChanged;
		_statModel.OnStatCaculated -= OnStatCaculated;

		if (_view.PlayerAnimEventListener != null)
		{
			_view.PlayerAnimEventListener.OnAttackStart -= _attackController.OnAttackStart;
			_view.PlayerAnimEventListener.OnAttackStart -= _moveController.OnAttackDash;
			_view.PlayerAnimEventListener.OnAttackEnd -= _attackController.OnAttackEnd;
		}

		PlayerTransformProvider.Unregister();
	}

	
}

