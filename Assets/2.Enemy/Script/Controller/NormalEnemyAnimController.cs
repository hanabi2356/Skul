using UnityEngine;

public class NormalEnemyAnimController 
{
	private INormalEnemyView _view;
	public NormalEnemyAnimController(INormalEnemyView view)
	{
		_view = view;
	}
    public void UpdateAnimation(ENormalEnemyState state)
	{
		_view.Animator.SetInteger("State", (int)state);
	}
}
