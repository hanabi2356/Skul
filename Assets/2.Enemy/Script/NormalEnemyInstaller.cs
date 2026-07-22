using UnityEngine;
using Zenject;
public class NormalEnemyInstaller : MonoInstaller
{
	[SerializeField] private NormalEnemyView _view;

	public override void InstallBindings()
	{
		Container.Bind<INormalEnemyView>().FromInstance(_view).AsSingle();
		Container.Bind<INormalEnemyStatModel>().To<NormalEnemyStatModel>().AsSingle();
		Container.Bind<IFSMMachine>().To<NormalEnemyFSMMachine>().AsSingle();

		Container.QueueForInject(FindAnyObjectByType<NormalEnemyPresenter>());
	}
}
