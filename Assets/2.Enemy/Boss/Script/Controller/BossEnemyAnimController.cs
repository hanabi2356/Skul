using UnityEngine;

public class BossEnemyAnimController 
{
	private readonly IBossEnemyView _view;
	public BossEnemyAnimController(IBossEnemyView view)
	{
		_view = view;
	}

	public void UpdateAnimation(EBossEnemyState state, int ? skillNumber=null, int phase = 1)
	{
		if (_view.Animator == null) return;
		_view.Animator.SetInteger("State", (int)state);
		_view.Animator.SetInteger("SkillNumber", skillNumber ?? 0);
		_view.Animator.SetInteger("Phase", phase);
	}
}
