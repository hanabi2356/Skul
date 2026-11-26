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
	private readonly List<ISkill> _bag = new List<ISkill>();

	public bool IsUsingSkill => _current != null && _current.IsRunning;
	public int? CurrentSkillNumber => _current?.SkillNumber;

	public BossEnemySkillController(
		IBossEnemyView view,
		List<ISkill> skills)
	{
		_view = view;
		_skills = skills ?? new List<ISkill>();
	}

	private void RefillBag()
	{
		_bag.Clear();
		_bag.AddRange(_skills);
		for(int i=_bag.Count-1; i >0; i--)
		{
			int j = Random.Range(0, i + 1);
			(_bag[i], _bag[j]) = (_bag[j], _bag[i]);
		}
	}
	public void TryUseSkill()
	{
		if (IsUsingSkill) return;
		if (_view.BossEnemyTransform == null) return;
		if(_bag.Count == 0) RefillBag();

		float dist = Vector2.Distance(_view.BossEnemyTransform.position, _view.TargetPosition);
		ISkill skill = _bag.FirstOrDefault(s => s.CanUse && dist <= s.MaxUseRange);

		if (skill != null)
		{
			_bag.Remove(skill);
		}
		else
		{
			// 백에 쓸 수 있는 스킬이 없으면 전체 목록에서 고름 (백은 그대로 유지)
			List<ISkill> fallback = _skills
				.Where(s => s.CanUse && dist <= s.MaxUseRange)
				.ToList();
			if (fallback.Count == 0) return;
			skill = fallback[Random.Range(0, fallback.Count)];
		}

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
