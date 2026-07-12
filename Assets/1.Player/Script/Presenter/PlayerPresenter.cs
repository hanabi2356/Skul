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

	private IFSMMachine _fsm;
	[SerializeField] private DefaultStatData _defaultStatData;

	private bool _isInitialized = false;
	[Inject]
	public void Initialize(IPlayerStatModel statModel, 
		IPlayerView view, 
		SkulDataLoader dataLoader,
		PlayerMoveController moveController,
		PlayerAttackController attackController,
		PlayerAnimController animController,
		IFSMMachine fsm)
	{
		
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;
		_moveController = moveController;
		_attackController = attackController;
		_animController = animController;
		_fsm = fsm;

		SkulStatData loadData = _dataLoader.SkulStatDataLoad("LittleBorn");
		SubscribeEvent();
		_statModel.UpdateFinalStat(_defaultStatData, loadData);

		if(_fsm is PlayerFSMMachine playerFSM)
		{
			playerFSM.BootUp();
		}

		_isInitialized = true;
	}
	private void Awake()
	{
		
	}
	/// <summary>
	/// 이벤트 구독 함수(공격 제외)
	/// </summary>
	private void SubscribeEvent()
	{
		if (_view != null && _moveController != null && _attackController != null)
		{
			_view.OnMove += _moveController.SetMoveInput;
			_view.OnJump += _moveController.TryJump;
			_view.OnPlatformIgnore += _moveController.TryPlatformIgnore;
			_view.OnDash += _moveController.TryDash;
			_view.OnAttack += _attackController.TryAttack;

			SubscribeAttackEvent();
		}
		
	}
	/// <summary>
	/// 앞으로 공격에 관한 이벤트가 추가 될 수 있어서 Attack 관련 이벤트 등록 분리
	/// </summary>
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

	private void FixedUpdate()
	{
		if (_isInitialized == false) return;
		_moveController.FixedTick();
		
	}

	private void Update()
	{
		Debug.Log(_fsm.CurrentState.ToString());
		_fsm.CurrentState?.Execute();
		_attackController.ComboCoolDown();
		if(_fsm is PlayerFSMMachine playerFSM)
		{
			_animController.ChangeAnim(playerFSM.CurrentStateEnum, _attackController.AttackCount);
		}
	}

	private void OnDisable()
	{
		if (_view != null && _moveController != null && _attackController != null)
		{
			_view.OnMove -= _moveController.SetMoveInput;
			_view.OnJump -= _moveController.TryJump;
			_view.OnPlatformIgnore -= _moveController.TryPlatformIgnore;
			_view.OnDash -= _moveController.TryDash;
			_view.OnAttack -= _attackController.TryAttack;

			_view.PlayerAnimEventListener.OnAttackStart -= _attackController.OnAttackStart;
			_view.PlayerAnimEventListener.OnAttackEnd -= _attackController.OnAttackEnd;
		}
	}

	
}

