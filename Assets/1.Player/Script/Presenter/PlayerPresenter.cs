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
	private async void Awake()
	{
		
	}
	private void SubscribeEvent()
	{
		if (_view != null && _moveController != null && _attackController != null)
		{
			_view.OnMove += _moveController.SetMoveInput;
			_view.OnJump += _moveController.TryJump;
			_view.OnDash += _moveController.TryDash;
			_view.OnAttack += _attackController.TryAttack;
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
		if(_fsm is PlayerFSMMachine playerFSM)
		{
			_animController.ChangeAnim(playerFSM.CurrentStateEnum, _attackController.AttackCount);
		}	
	}
	private void OnDestroy()
	{
		if (_view != null && _moveController != null && _attackController != null)
		{
			_view.OnMove -= _moveController.SetMoveInput;
			_view.OnJump -= _moveController.TryJump;
			_view.OnDash -= _moveController.TryDash;
			_view.OnAttack -= _attackController.TryAttack;

		}
	}
}

