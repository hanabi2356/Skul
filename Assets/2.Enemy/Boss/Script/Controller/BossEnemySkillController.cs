using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossEnemySkillController
{
	private readonly IBossEnemyView _view;
	private readonly List<ISkill> _skills;
	private ISkill _current;
	private float _skillStartTime;
	private const float _maxSkillDuration = 5.0f;

	public bool IsUsingSkill => _current != null && _current.IsRunning;
	public int? CurrentSkillNumber => _current?.SkillNumber;

	public BossEnemySkillController(
		IBossEnemyView view,
		List<ISkill> skills)
	{
		_view = view;
		_skills = skills ?? new List<ISkill>();
	}
	public void TryUseSkill()
	{
		if (IsUsingSkill) return;
		if (_view.BossEnemyTransform == null) return;

		float dist = Vector2.Distance(_view.BossEnemyTransform.position, _view.TargetPosition);
		ISkill skill = _skills
			.Where(s => s.CanUse)
			.Where(s => dist <= s.MaxUseRange)
			.FirstOrDefault();

		if (skill == null) return;

		_current = skill;
		_current.Enter();         
		_view.SetUseSkill(true);  
		_skillStartTime = Time.time;
	}

	public void Tick()
	{
		if (_current == null) return;

		_current.Tick();

		if(_current != null && _current.IsEndRequested)
		{
			OnAttackEnd();
		}
		else if (IsUsingSkill && Time.time - _skillStartTime >= _maxSkillDuration)
		{
			OnAttackEnd();
		}
		
	}

	public void OnAttackHit()
	{
		_current?.OnHit();
	}

	public void OnAttackEnd()
	{
		if (_current == null) return;
		_current.Exit();
		_current = null;
		_view.SetUseSkill(false);
	}

	public void Cancel()
	{
		if (_current == null) return;
		_current.Cancle();
		_current = null;
		_view.SetUseSkill(false);
	}
}
