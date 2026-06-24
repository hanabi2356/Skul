using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<IPlayerStatModel>().To<PlayerStatModel>().AsSingle();

		Container.Bind<SkulDataLoader>().AsSingle();

		Container.Bind<IPlayerView>().To<PlayerView>().AsSingle();
	}
}
