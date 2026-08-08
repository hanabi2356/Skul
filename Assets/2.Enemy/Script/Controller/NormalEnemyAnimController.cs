using UnityEngine;

public class NormalEnemyAnimController
{
	private readonly INormalEnemyView _view;

	public NormalEnemyAnimController(INormalEnemyView view)
	{
		_view = view;
	}

	public void UpdateAnimation(ENormalEnemyState state)
	{
		if (_view.Animator == null) return;
		_view.Animator.SetInteger("State", (int)state);
	}
}
