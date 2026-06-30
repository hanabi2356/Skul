using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
	[SerializeField] private PlayerView _playerView;
	public override void InstallBindings()
	{
		Container.Bind<IPlayerStatModel>().To<PlayerStatModel>().AsSingle();
		Container.Bind<IPlayerView>().FromInstance(_playerView).AsSingle();

		Container.Bind<SkulDataLoader>().AsSingle();

		Container.Bind<PlayerMoveController>().AsSingle();
		Container.Bind<PlayerAttackController>().AsSingle();
		Container.Bind<PlayerAnimController>().AsSingle();

		Container.Bind<IFSMMachine>().To<PlayerFSMMachine>().AsSingle();

		Container.QueueForInject(FindAnyObjectByType<PlayerPresenter>());
	}
}
