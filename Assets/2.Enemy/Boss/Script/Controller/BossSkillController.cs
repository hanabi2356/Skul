using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스 스킬 선택·실행·취소. Attack State는 이 컨트롤러만 호출한다.
/// </summary>
public class BossSkillController
{
	private readonly IBossEnemyView _view;
	private readonly IEnemyStatModel _statModel;
	private readonly List<ISkillBase> _skills;
	private ISkillBase _current;

	public bool IsUsingSkill => _current != null && _current.IsRunning;
	public bool IsSkillCompleted => _current != null && _current.IsFinished && _current.IsRunning == false;
	public bool HasSelectedSkill => _current != null;

	public bool CanUseAnySkill
	{
		get
		{
			if (IsUsingSkill) return false;
			if (IsInAttackRange() == false) return false;

			for (int i = 0; i < _skills.Count; i++)
			{
				if (_skills[i].CanExecute())
				{
					return true;
				}
			}

			return false;
		}
	}

	public bool IsOnCoolDown
	{
		get
		{
			for (int i = 0; i < _skills.Count; i++)
			{
				if (_skills[i].CanExecute())
				{
					return false;
				}
			}

			return true;
		}
	}

	public BossSkillController(
		IBossEnemyView view,
		IEnemyStatModel statModel,
		List<ISkillBase> skills)
	{
		_view = view;
		_statModel = statModel;
		_skills = skills ?? new List<ISkillBase>();
	}

	public void TryBeginSkill()
	{
		if (IsUsingSkill) return;

		ISkillBase skill = SelectSkill();
		if (skill == null) return;

		_current = skill;
		_current.Begin();
	}

	public void Tick()
	{
		_current?.Tick();
	}

	public void Cancel()
	{
		if (_current == null) return;

		_current.Cancel();
	}

	public bool IsInAttackRange()
	{
		if (_view?.BossEnemyTransform == null) return false;

		Vector2 self = _view.BossEnemyTransform.position;
		Vector2 target = _view.TargetPosition;
		float range = _statModel.FinalAttackRange;
		return (target - self).sqrMagnitude <= range * range;
	}

	private ISkillBase SelectSkill()
	{
		for (int i = 0; i < _skills.Count; i++)
		{
			if (_skills[i].CanExecute())
			{
				return _skills[i];
			}
		}

		return null;
	}
}
