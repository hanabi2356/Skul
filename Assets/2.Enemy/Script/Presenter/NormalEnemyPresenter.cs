using UnityEngine;
using Zenject;

public class NormalEnemyPresenter : MonoBehaviour
{
	[SerializeField] private string _enemyID = "101";

	private INormalEnemyStatModel _normalEnemyStatModel;
	private INormalEnemyView _view;
	private NormalEnemyDataLoader _dataLoader;
	private NormalEnemyFSMMachine _fsm;
	private NormalEnemyAnimController _animController;
	private bool _isInitialized;

	[Inject]
	public void Initialize(INormalEnemyStatModel normalEnemyModel,
		INormalEnemyView view,
		NormalEnemyDataLoader dataLoader,
		NormalEnemyFSMMachine fsm,
		NormalEnemyAnimController animController)
	{
		_normalEnemyStatModel = normalEnemyModel;
		_view = view;
		_dataLoader = dataLoader;
		_fsm = fsm;
		_animController = animController;

		if (_view == null)
		{
			Debug.LogError("NormalEnemyView가 주입되지 않았습니다.");
			return;
		}

		_dataLoader.EnemyStatDataToCache();

		EnemyStatDataDTO data = _dataLoader.Get(_enemyID);
		if (data != null)
		{
			_normalEnemyStatModel.UpdateFinalStat(data);
		}

		_fsm.BootUp();
		SubscribeEvent();
		_isInitialized = true;
	}

	void Update()
	{
		if (_isInitialized == false || _view == null) return;

		if (PlayerTransformProvider.PlayerTransform != null)
		{
			_view.UpdateTargetPosition(PlayerTransformProvider.PlayerTransform.position);
		}

		_fsm.CurrentState?.Execute();
		_animController?.UpdateAnimation(_fsm.CurrentStateEnum);
	}

	private void SubscribeEvent()
	{
	}
}
