using UnityEngine;
using Zenject;
public class NormalEnemyInstaller : MonoInstaller
{
	[SerializeField] private NormalEnemyView _view;

	public override void InstallBindings()
	{
		Container.Bind<INormalEnemyView>().FromInstance(_view).AsSingle();
		Container.Bind<INormalEnemyStatModel>().To<NormalEnemyStatModel>().AsSingle();
		// IFSMMachine 공용 바인딩은 PlayerInstaller와 충돌하므로 구체 타입으로 바인딩
		Container.Bind<NormalEnemyFSMMachine>().AsSingle();
		Container.Bind<NormalEnemyDataLoader>().AsSingle();
		Container.QueueForInject(FindAnyObjectByType<NormalEnemyPresenter>());
	}
}
