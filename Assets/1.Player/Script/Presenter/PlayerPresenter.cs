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

	private IFSMMachine _fsm;
	[SerializeField] private DefaultStatData _defaultStatData;
	[Inject]
	public void Initialize(IPlayerStatModel statModel, 
		IPlayerView view, 
		SkulDataLoader dataLoader,
		PlayerMoveController moveController,
		PlayerAttackController attackController,
		IFSMMachine fsm)
	{
		
		_statModel = statModel;
		_view = view;
		_dataLoader = dataLoader;
		_moveController = moveController;
		_attackController = attackController;
		_fsm = fsm;
		
	}
	private async void Awake()
	{
		SkulStatData loadData = _dataLoader.SkulStatDataLoad("LittleBorn");
		SubscribeEvent();
		_statModel.UpdateFinalStat(_defaultStatData, loadData);
	}
	private void SubscribeEvent()
	{
		_view.OnMove += _moveController.SetMoveInput;
		_view.OnJump += _moveController.TryJump;
		_view.OnDash += _moveController.TryDash;
		_view.OnAttack += _attackController.TryAttack;
		
	}
	private void FixedUpdate()
	{
		_moveController.FixedTick();
		Debug.Log(_fsm.CurrentState.ToString());
		_fsm.CurrentState?.Execute();
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

