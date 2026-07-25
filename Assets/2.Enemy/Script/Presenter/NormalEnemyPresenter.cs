using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NormalEnemyPresenter : MonoBehaviour
{
	private INormalEnemyStatModel _normalEnemyStatModel;
	private INormalEnemyView _view;
	private NormalEnemyDataLoader _dataLoader;
	private NormalEnemyFSMMachine _fsm;
	private bool _isInitialized;

	[Inject]
	public void Initialize(INormalEnemyStatModel normalEnemyModel,
		INormalEnemyView view,
		NormalEnemyDataLoader dataLoader,
		NormalEnemyFSMMachine fsm)
	{
		_normalEnemyStatModel = normalEnemyModel;
		_view = view;
		_dataLoader = dataLoader;
		_fsm = fsm;

		if (_view == null)
		{
			Debug.LogError("NormalEnemyView가 주입되지 않았습니다. NormalEnemyInstaller._view를 Prefab View에 연결하세요.");
			return;
		}

		_dataLoader.EnemyStatDataToCache();
		_fsm.BootUp();

		SubscribeEvent();
		_isInitialized = true;
	}

	void Update()
	{
		if (_isInitialized == false || _view == null) return;
		if (PlayerTransformProvider.PlayerTransform == null) return;

		_view.UpdateTargetPosition(PlayerTransformProvider.PlayerTransform.position);
	}

	private void SubscribeEvent()
	{
		
	}
}
