using UnityEngine;
using Zenject;
public class BossEnemyInstaller : MonoInstaller
{
	[SerializeField] private BossEnemyView _view;
	[SerializeField] private BossSkillView _skillView;
	[SerializeField] private BossEnemyPresenter _presenter;
	public override void InstallBindings()
	{
		if (_view == null)
		{ 
			_view = GetComponent<BossEnemyView>();
		}
		if (_skillView == null)
		{
			_skillView = GetComponent<BossSkillView>();
		}
		if (_presenter == null)
		{
			_presenter = GetComponent<BossEnemyPresenter>();
		}

		Container.Bind<IBossEnemyView>().FromInstance(_view).AsSingle();
		Container.Bind<BossSkillView>().FromInstance(_skillView).AsSingle();
		Container.Bind<IEnemyStatModel>().To<EnemyStatModel>().AsSingle();
		Container.Bind<BossEnemyFSMMachine>().AsSingle();

		Container.Bind<ISkill>().To<BossSkillMeleeAttack>().AsCached();
		Container.Bind<ISkill>().To<BossSkillJumpAttack>().AsCached();
		Container.Bind<ISkill>().To<BossSkillDashAttack>().AsCached();
		Container.Bind<ISkill>().To<BossSkillProjectileAttack>().AsCached();

		Container.Bind<BossEnemySkillController>().AsSingle();
		Container.Bind<BossEnemyAnimController>().AsSingle();


		Container.QueueForInject(_presenter);
	}
}
