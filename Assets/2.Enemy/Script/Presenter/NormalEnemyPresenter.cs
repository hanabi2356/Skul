using UnityEngine;
using Zenject;

public class NormalEnemyPresenter : MonoBehaviour
{
	[SerializeField] private string _enemyName = "Archer";

	private INormalEnemyStatModel _normalEnemyStatModel;
	private INormalEnemyView _view;
	private IFSMMachine _fsm;

	[Inject]
	public void Initialize(INormalEnemyStatModel normalEnemyModel,
		INormalEnemyView view,
		IFSMMachine fsm	)
	{
		_normalEnemyStatModel = normalEnemyModel;
		_view = view;
		_fsm = fsm;

		

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
