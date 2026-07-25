using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NormalEnemyPresenter : MonoBehaviour
{
	
	
	private INormalEnemyStatModel _normalEnemyStatModel;
	private INormalEnemyView _view;
	private NormalEnemyDataLoader _dataLoader;
	private IFSMMachine _fsm;

	[Inject]
	public void Initialize(INormalEnemyStatModel normalEnemyModel,
		INormalEnemyView view,
		NormalEnemyDataLoader dataLoader,
		IFSMMachine fsm	)
	{
		_normalEnemyStatModel = normalEnemyModel;
		_view = view;
		_dataLoader = dataLoader;
		_fsm = fsm;

		_dataLoader.EnemyStatDataToCache();
		
		if (_fsm is NormalEnemyFSMMachine normalEnemyFSM)
		{
			normalEnemyFSM.BootUp();
		}

		SubscribeEvent();
	}

	void Update()
	{
		if (PlayerTransformProvider.PlayerTransform == null) return;

		_view.UpdateTargetPosition(PlayerTransformProvider.PlayerTransform.position);
	}

	private void SubscribeEvent()
	{
		
	}
}
