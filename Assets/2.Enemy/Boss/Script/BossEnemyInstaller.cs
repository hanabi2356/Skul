using UnityEngine;
using Zenject;
public class BossEnemyInstaller : MonoInstaller
{
	[SerializeField] private BossEnemyView _view;
	[SerializeField] private BossEnemyPresenter _presenter;
	public override void InstallBindings()
	{
		if (_view == null)
		{ 
			_view = GetComponent<BossEnemyView>();
		}

		if (_presenter == null)
		{
			_presenter = GetComponent<BossEnemyPresenter>();
		}

		Container.Bind<IBossEnemyView>().FromInstance(_view).AsSingle();
		Container.Bind<IEnemyStatModel>().To<EnemyStatModel>().AsSingle();
		Container.Bind<BossEnemyFSMMachine>().AsSingle();

		Container.Bind<BossSkillMeleeAttack>().AsSingle();
		Container.Bind<BossEnemySkillController>().AsSingle();
		Container.Bind<BossEnemyAnimController>().AsSingle();


		Container.QueueForInject(_presenter);
	}
}
