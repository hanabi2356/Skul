using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController 
{
	

    private IPlayerStatModel _statModel;
	private IPlayerView _view;

	private float _lastAttackTime = 0.0f;

	public int AttackCount { get; private set; } = 0;
    public bool IsAttacking { get; private set; } =  false;
    public bool IsReset {  get; private set; } = false;

	private Queue<float> _inputBuffer = new Queue<float>();

	public PlayerAttackController(IPlayerStatModel statModel, 
		IPlayerView view)
	{
		_statModel = statModel;
		_view = view;
	}

	public void TryAttack()
	{
		_inputBuffer.Enqueue(Time.time);

		if(IsAttacking == false)
		{
			ProcessInputBuffer();
		}
		
		
	}
	private void ProcessInputBuffer()
	{
		while(_inputBuffer.Count > 0)
		{
			float inputTime = _inputBuffer.Peek();

			if( Time.time - inputTime > 0.2f)
			{
				_inputBuffer.Dequeue();
				continue;
			}

			if(AttackCount < _statModel.FinalDashMaxCount)
			{
				_inputBuffer.Dequeue();
				StartNewAttack();
				return;
			}
			else
			{
				_inputBuffer.Dequeue();
			}
		}
	}
	public void OnAttackStart()
	{
		
		
	}
	
	public void OnAttackEnd()
	{
		IsAttacking = false;
		_view.SetIsAttacking(IsAttacking);

		ProcessInputBuffer();

	}
	private void StartNewAttack()
	{
		IsReset = false;
		IsAttacking = true;
		AttackCount++;

		_lastAttackTime = Time.time;
		_view.SetIsAttacking(IsAttacking);

	}


	public void ComboCoolDown()
	{
		if(IsAttacking == false && AttackCount>0)
		{
			if(Time.time-_lastAttackTime > _statModel.FinalAttackCountResetDelay)
			{
				ResetCombo();
			}
		}
	}

	public void ResetCombo()
	{
		AttackCount = 0;
		IsReset = true;
		_inputBuffer.Clear();
	}

    

}
