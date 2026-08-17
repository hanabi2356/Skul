using Zenject;

/// <summary>
/// SceneContext에 등록. 모든 NormalEnemy가 공유하는 데이터만 바인딩한다.
/// </summary>
public class EnemySharedInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<NormalEnemyDataLoader>().AsSingle();
		Container.Bind<NormalEnemyRegistry>().AsSingle();
		Container.Bind<NormalEnemyProjectilePool>().AsSingle();
	}
}
