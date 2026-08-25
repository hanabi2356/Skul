using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
	[SerializeField] private PlayerView _playerView;
	[SerializeField] private PlayerHudView _playerHudView;
	public override void InstallBindings()
	{
		Container.Bind<IPlayerStatModel>().To<PlayerStatModel>().AsSingle();
		Container.Bind<IPlayerView>().FromInstance(_playerView).AsSingle();
		Container.Bind<IPlayerHudView>().FromInstance(_playerHudView).AsSingle();

		Container.Bind<SkulDataLoader>().AsSingle();

		Container.Bind<PlayerMoveController>().AsSingle();
		Container.Bind<PlayerAttackController>().AsSingle();
		Container.Bind<PlayerAnimController>().AsSingle();

		// IFSMMachine을 SceneContext에서 Player/Enemy가 동시에 Bind하면 충돌한다.
		// Presenter는 구체 FSM만 주입받는다.
		Container.Bind<PlayerFSMMachine>().AsSingle();

		Container.QueueForInject(FindAnyObjectByType<PlayerPresenter>());
	}
}
