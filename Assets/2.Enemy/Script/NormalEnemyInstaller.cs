using UnityEngine;
using Zenject;

/// <summary>
/// NormalEnemy Prefab의 GameObjectContext에 등록.
/// Prefab 인스턴스마다 독립된 View / Model / FSM / Controller를 가진다.
/// </summary>
public class NormalEnemyInstaller : MonoInstaller
{
	[SerializeField] private NormalEnemyView _view;
	[SerializeField] private NormalEnemyPresenter _presenter;

	public override void InstallBindings()
	{
		if (_view == null)
		{
			_view = GetComponent<NormalEnemyView>();
		}

		if (_presenter == null)
		{
			_presenter = GetComponent<NormalEnemyPresenter>();
		}

		Container.Bind<INormalEnemyView>().FromInstance(_view).AsSingle();
		Container.Bind<INormalEnemyStatModel>().To<NormalEnemyStatModel>().AsSingle();
		Container.Bind<NormalEnemyFSMMachine>().AsSingle();

		Container.Bind<NormalEnemyMoveController>().AsSingle();
		Container.Bind<NormalEnemyAnimController>().AsSingle();
		Container.Bind<MeleeAttackAction>().AsSingle();
		Container.Bind<RangeAttackAction>().AsSingle();
		Container.Bind<NormalEnemyAttackController>().AsSingle();
		Container.Bind<NormalEnemyRangeDetectionController>().AsSingle();

		// DataLoader는 SceneContext(EnemySharedInstaller)에서 상속받아 Resolve
		Container.QueueForInject(_presenter);
	}
}
